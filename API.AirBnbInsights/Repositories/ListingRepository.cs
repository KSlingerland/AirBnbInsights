using System;
using API.AirBnbInsights.Models;
using API.AirBnbInsights.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ZiggyCreatures.Caching.Fusion;

namespace API.AirBnbInsights.Repositories
{
	public class ListingRepository : IListingRepository
	{
        private readonly InsightsDbContext _context;
        private readonly IFusionCache _cache;

        public ListingRepository(InsightsDbContext context, IFusionCache cache)
		{
            _context = context;
            _cache = cache;
		}

        public async Task<GeoJsonResponse?> GetAll()
        {
            if (_context.Listings == null)
            {
                return null;
            }

            var features = _cache.GetOrSet<List<Feature>>("Listings", await _context.Listings.AsNoTracking().Select(x => new Feature
            {
                Type = "Feature",
                Geometry = new Geometry
                {
                    Type = "Point",
                    Coordinates = new List<double>
                    {
                        decimal.ToDouble(x.Longitude) * 0.000001,
                        decimal.ToDouble(x.Latitude) * 0.000001
                    }
                },
                Properties = new Properties
                {
                    Id = x.Id,
                    Neighbourhood = x.NeighbourhoodCleansed
                }
            }).ToListAsync(), options => options
                    .SetPriority(CacheItemPriority.High)
                    .SetFailSafe(true, TimeSpan.FromHours(2))
                    .SetFactoryTimeouts(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(2))
                );

            return new GeoJsonResponse
            {
                Type = "FeatureCollection",
                Features = features
            };
        }

        public async Task<Listing?> GetById(long Id)
        {
            if (_context.Listings == null)
            {
                return null;
            }
            var listing = await _cache.GetOrSetAsync($"listing:{Id}", await _context.Listings.FindAsync(Id));

            if (listing == null)
            {
                return null;
            }

            return listing;
        }
    }
}

