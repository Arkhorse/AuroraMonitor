using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraMonitor.Data
{
    public class MainConfig
    {
        public bool BaseLoaded { get; set; }
        public bool SandboxLoaded { get; set; }
        public bool DLC01Loaded { get; set; }
        public string? BaseSceneName { get; set; }
        public string? SandboxSceneName { get; set; }
        public string? DLC01SceneName { get; set; }
        public bool ModInitiliated { get; set; }
    }
}
