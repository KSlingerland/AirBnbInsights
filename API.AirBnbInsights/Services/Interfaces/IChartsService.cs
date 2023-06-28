using System;
using API.AirBnbInsights.Models;

namespace API.AirBnbInsights.Services.Interfaces
{
	public interface IChartsService
	{
		Task<List<ChartSeries>> GetLitingsPerNeighbourhood();
    }
}

