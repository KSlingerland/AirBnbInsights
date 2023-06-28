using Microsoft.AspNetCore.Mvc;
using API.AirBnbInsights.Models;
using API.AirBnbInsights.Repositories.Interfaces;

namespace API.AirBnbInsights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IListingRepository _listingRepository;

        public ListingsController(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        // GET: api/Listings
        [HttpGet]
        public async Task<ActionResult<GeoJsonResponse>> GetListings()
        {
            var listings = await _listingRepository.GetAll();

            if (listings != null)
            {
                return Ok(listings);
            }

            return NotFound();

        }

        // GET: api/Listings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(long id)
        {
            var listing = await _listingRepository.GetById(id);

            if(listing != null)
            {
                return listing;
            }

            return NotFound();
        }
    }
}
