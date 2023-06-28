using System;
namespace API.AirBnbInsights.Models
{
	public class ChartResponse
	{
		public string Label { get; set; }
		public List<ChartSeries> ChartSeries { get; set; }
	}

	public class ChartSeries
	{
		public string Label { get; set; }
		public List<ChartData> Data { get; set;}
	}

	public class ChartData 
	{
		public dynamic Primary { get; set; }
		public dynamic Secondary { get; set; }
	}
}

