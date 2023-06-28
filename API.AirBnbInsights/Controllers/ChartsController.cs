using Microsoft.AspNetCore.Mvc;
using API.AirBnbInsights.Models;
using API.AirBnbInsights.Services.Interfaces;

namespace API.AirBnbInsights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly IChartsService _chartsService;
        public ChartsController(IChartsService chartsService)
        {
            _chartsService = chartsService;
        }

        //// GET: api/Charts/listings
        //[HttpGet("listings")]
        //public async Task<ActionResult<List<ChartSeries>>> GetListingsChart()
        //{
        //    if (_context.Listings == null)
        //    {
        //        return NotFound();
        //    }

        //    var ListingCount = new ChartData
        //    {
        //        Primary = "Amount Of Listings",
        //        Secondary = _context.Listings.Count()
        //    };

        //    var HostCount = new ChartData
        //    {
        //        Primary = "Amount of Hosts",
        //        Secondary = _context.Listings.AsNoTracking().Select(x => x.HostId).Distinct().Count()
        //    };

        //    var test = new ChartData
        //    {
        //        Primary = "Neibourhoods",
        //        Secondary = _context.Listings.AsNoTracking().Select(x => x.NeighbourhoodCleansed).Distinct().Count()
        //    };

        //}



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

        //GET: api/charts/neighbourhoods
        [HttpGet("neighbourhoods")]
        public async Task<ActionResult<List<ChartSeries>>> GetLitingsPerNeighbourhood()
        {
            var chartData = await _chartsService.GetLitingsPerNeighbourhood();

            if(chartData is not null)
            {
                return chartData;
            }

            return NotFound();
        }
    }
}
