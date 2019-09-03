using System;
using System.Collections.Generic;
using System.Text;

namespace AppSoftware.FileDistribution
{
    public class Distribution
    {
        public string SourceDirectory { get; set; }

        public IList<string> DestinationDirectories { get; set; }
    }
}
