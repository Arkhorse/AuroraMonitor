using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Il2CppInterop.Runtime;
using System.Text.Json.Serialization;

namespace AuroraMonitor.Data
{
	public class WeatherDataTracking
	{
		[JsonInclude]
		public SortedDictionary<string, WeatherDataTrackingIndex> Weather = new();
	}

	public class WeatherDataTrackingIndex
	{
		[JsonInclude]
		[JsonPropertyName("Stage")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public WeatherStage m_WeatherType;
		[JsonInclude]
		[JsonPropertyName("Duration")]
		public Vector2 m_DurationMinMax;
		[JsonInclude]
		[JsonPropertyName("Transition")]
		public Vector2 m_TransitionTimeMinMax;
		[JsonInclude]
		[JsonPropertyName("Current Duration")]
		public float m_CurrentDuration;
		[JsonInclude]
		[JsonPropertyName("Current Transition")]
		public float m_CurrentTransitionTime;
		[JsonInclude]
		[JsonPropertyName("Elapsed Time")]
		public float m_ElapsedTime;
		[JsonInclude]
		public double Temperature;
	}
}
