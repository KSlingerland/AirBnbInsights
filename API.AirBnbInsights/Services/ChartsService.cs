using System;
using API.AirBnbInsights.Models;
using API.AirBnbInsights.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.AirBnbInsights.Services
{
	public class ChartsService: IChartsService
    {
        private readonly InsightsDbContext _context;

        public ChartsService(InsightsDbContext context)
		{
            _context = context;
        }

        public async Task<List<ChartSeries>> GetLitingsPerNeighbourhood()
        {
            var chartData = await _context.Listings
                .GroupBy(l => l.NeighbourhoodCleansed)
                .Select(g => new ChartData
                {
                    Primary = g.Key,
                    Secondary = g.Count()
                })
                .ToListAsync();

            var chartSeries = new ChartSeries
            {
                Label = "Listings",
                Data = chartData
            };

            return new List<ChartSeries> { chartSeries };
        }
    }
}

