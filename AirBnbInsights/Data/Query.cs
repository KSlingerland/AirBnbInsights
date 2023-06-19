using AirBnbInsights.Modules.Models;

namespace AirBnbInsights.Data
{
    public class Query
    {
        public IQueryable<Listing> GetListings =>
            new List<Listing>().AsQueryable();
    }
}
