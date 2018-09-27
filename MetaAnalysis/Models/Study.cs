using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaAnalysis.Models
{
    public class Study
    {
        public int ID { get; set; }
        private static int nextId = 1;

        public string Id { get; set; }
        public PublicationYear PublicationYear { get; set; }
        public CorrelationCoefficient CorrelationCoefficient { get; set; }
        public SampleSize SampleSize { get; set; }
        public VariablesControlled VariablesControlled { get; set; }
        public StudyDesign StudyDesign { get; set; }
        public AdherenceMeasure AdherenceMeasure { get; set; }
        public ConscientiousnessMeasure ConscientiousnessMeasure { get; set; }
        public MeanAge MeanAge { get; set; }
        public MethodologicalQuality MethodologicalQuality { get; set; }

        public Study()
        {
            ID = nextId;
            nextId++;
        }
    }
}

