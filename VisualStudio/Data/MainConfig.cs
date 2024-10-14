using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuroraMonitor.Data
{
	public class MainConfig
	{
		public MainConfig() { }

		[JsonInclude]
		public bool BaseLoaded { get; set; }
		[JsonInclude]
		public bool SandboxLoaded { get; set; }
		[JsonInclude]
		public bool DLC01Loaded { get; set; }
		[JsonInclude]
		public string? BaseSceneName { get; set; }
		[JsonInclude]
		public string? SandboxSceneName { get; set; }
		[JsonInclude]
		public string? DLC01SceneName { get; set; }
		[JsonInclude]
		public bool ModInitiliated { get; set; }
		[JsonInclude]
		public List<float>? WeatherWeights { get; set; } = new();
	}
}
