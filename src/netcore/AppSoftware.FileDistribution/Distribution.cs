using System.Collections.Generic;

namespace AppSoftware.FileDistribution
{
    public class Distribution
    {
        public string SourceDirectory { get; set; }

        public string FileNameMaskRegex { get; set; }

        public IList<string> DestinationDirectories { get; set; }
    }
}
