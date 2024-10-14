using System.Text.Json.Serialization;

namespace AuroraMonitor.Data
{
	public class WeatherMonitorData
	{
		public WeatherMonitorData() { }

		[JsonInclude]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		[JsonPropertyName("Previous Weather Stage")]
		public WeatherStage Prev { get; set; }

		[JsonInclude]
		[JsonPropertyName("Weather Information")]
		public List<WeatherInformation>? m_WeatherInformation { get; set; }
	}

	public class WeatherInformation
	{
		public WeatherInformation() { }

		[JsonInclude]
		[JsonPropertyName("Day Information")]
		public DayInformation? m_DayInformation { get; set; }

		[JsonInclude]
		[JsonPropertyName("Weather Stage")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public WeatherStage m_WeatherStage { get; set; }
		[JsonInclude]
		public float WindSpeed { get; set; }
		[JsonInclude]
		public float WindAngle { get; set; }
		[JsonInclude]
		public float WindPlayerMult { get; set; }
		[JsonInclude]
		public float Temperature { get; set; }
	}

	public class DayInformation
	{
		public DayInformation() { }

		[JsonInclude]
		public int Day { get; set; }
		[JsonInclude]
		public int Hour { get; set; }
		[JsonInclude]
		public int Minute { get; set; }
	}


}
