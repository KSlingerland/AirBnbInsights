using System;
using API.AirBnbInsights.Models;

namespace API.AirBnbInsights.Repositories.Interfaces
{
	public interface IListingRepository
	{
        Task<GeoJsonResponse?> GetAll();
        Task<Listing?> GetById(long Id);
    }
}

