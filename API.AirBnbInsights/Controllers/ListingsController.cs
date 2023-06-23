using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.AirBnbInsights.Models;
using Newtonsoft.Json;
using ZiggyCreatures.Caching.Fusion;
using Microsoft.Extensions.Caching.Memory;

namespace API.AirBnbInsights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly InsightsDbContext _context;
        private readonly IFusionCache _cache;

        public ListingsController(InsightsDbContext context, IFusionCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/Listings
        [HttpGet]
        public async Task<IActionResult> GetListings()
        {
            if (_context.Listings == null)
            {
                return NotFound();
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
                    Name = x.Name,
                    HostName = x.HostName,
                    RoomType = x.RoomType,
                    Accomodates = x.Accommodates,
                    Price = x.Price
                }
            }).ToListAsync(), options => options
                    .SetPriority(CacheItemPriority.High)
                    .SetFailSafe(true, TimeSpan.FromHours(2))
                    .SetFactoryTimeouts(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(2))
                );

            var response = new GeoJsonResponse
            {
                Type = "FeatureCollection",
                Features = features
            };

            string json = JsonConvert.SerializeObject(response);

            return Content(json, "application/json");
        }

        // GET: api/Listings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(long id)
        {
          if (_context.Listings == null)
          {
              return NotFound();
          }
            var listing = await _cache.GetOrSetAsync<Listing>(
                $"listing:{id}",
                await _context.Listings.FindAsync(id),
                options => options
                    .SetPriority(CacheItemPriority.High)
                    .SetFailSafe(true, TimeSpan.FromHours(2))
                    .SetFactoryTimeouts(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(2))
                );

            if (listing == null)
            {
                return NotFound();
            }

            return listing;
        }
    }
}
