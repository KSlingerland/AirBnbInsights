using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.AirBnbInsights.Models;

namespace API.AirBnbInsights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly InsightsDbContext _context;

        public ChartsController(InsightsDbContext context)
        {
            _context = context;
        }

        // GET: api/Charts/listings
        [HttpGet("listings")]
        public async Task<ActionResult<List<ChartSeries>>> GetListingsChart()
        {
          if (_context.Listings == null)
          {
              return NotFound();
          }

            var ListingCount = new ChartData
            {
                Primary = "Amount Of Listings",
                Secondary = _context.Listings.Count()
            };

            var HostCount = new ChartData
            {
                Primary = "Amount of Hosts",
                Secondary = _context.Listings.AsNoTracking().Select(x => x.HostId).Distinct().Count()
            };

            var test = new ChartData
            {
                Primary = "Neibourhoods",
                Secondary = _context.Listings.AsNoTracking().Select(x => x.NeighbourhoodCleansed).Distinct().Count()
            };

            return new List<ChartSeries>
                {

                    new ChartSeries {
                        Label = "Neibourhoods",
                        ChartData = new List<ChartData>
                        {
                            test
                        }

                    },

                    new ChartSeries{
                        Label = "Hosts",
                        ChartData = new List<ChartData>{
                            HostCount
                        }
                    }
                };

        }

        //// GET: api/Charts/listings/hosts
        //[HttpGet("/listings/hosts")]
        //public async Task<ActionResult<List<ChartSeries>>>GetHostsChart()
        //{
        //    if (_context.Listings == null)
        //    {
        //        return NotFound();
        //    }

        //    var chartData = 

        //    return new List<ChartSeries>
        //    {
        //        new ChartSeries
        //        {
        //            Label = "Hosts",
        //            ChartData = 
        //        }
        //    };
        //}

        // GET: api/Charts/listings/neighbourhoods
        [HttpGet("neighbourhoods")]
        public async Task<ActionResult<List<ChartData>>> GetListingsPerNeighbourhood()
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }

            var chartData = _context.Listings
            .GroupBy(l => l.NeighbourhoodCleansed)
            .Select(g => new ChartData
            {
                Primary = g.Key,
                Secondary = g.Count() // You can change this based on your requirements
            })
            .ToList();

            return chartData;
        }
    }
}
