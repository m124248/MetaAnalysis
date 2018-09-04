using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaAnalysis.Models
{
    public class Study
    {
        public string Id { get; set; }
        public string PublicationYear { get; set; }
        public string N { get; set; }
        public string R { get; set; }
        public string VariablesControlled { get; set; }
        public string StudyDesign { get; set; }
        public string AdherenceMeasure { get; set; }
        public string ConscientiousnessMeasure { get; set; }
        public string MeanAge { get; set; }
        public string MethodologicalQuality { get; set; }

    }

}
