using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaAnalysis.Models
{
    public class Study
    {
        public string Id { get; set; }
        public int PublicationYear { get; set; }
        public int N { get; set; }
        public float R { get; set; }
        public string VariablesControlled { get; set; }
        public string StudyDesign { get; set; }
        public string AdherenceMeasure { get; set; }
        public string ConscientiousnessMeasure { get; set; }
        public float MeanAge { get; set; }
        public int MethodologicalQuality { get; set; }

    }

}
