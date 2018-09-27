using System;
using System.Collections.Generic;
using System.Linq;
using MetaAnalysis.Models;

namespace MetaAnalysis.Data
{
    public class StudyData
    {
        // A data store for Study objects

        public List<Study> Studies { get; set; } = new List<Study>();
        public StudyFieldData<PublicationYear> PublicationYears { get; set; } = new StudyFieldData<PublicationYear>();
        public StudyFieldData<CorrelationCoefficient> CorrelationCoefficients { get; set; } = new StudyFieldData<CorrelationCoefficient>();
        public StudyFieldData<SampleSize> SampleSizes { get; set; } = new StudyFieldData<SampleSize>();
        public StudyFieldData<VariablesControlled> VariablesControlled { get; set; } = new StudyFieldData<VariablesControlled>();
        public StudyFieldData<StudyDesign> StudyDesigns { get; set; } = new StudyFieldData<StudyDesign>();
        public StudyFieldData<AdherenceMeasure> AdherenceMeasures { get; set; } = new StudyFieldData<AdherenceMeasure>();
        public StudyFieldData<ConscientiousnessMeasure> ConscientiousnessMeasures { get; set; } = new StudyFieldData<ConscientiousnessMeasure>();
        public StudyFieldData<MeanAge> MeanAges { get; set; } = new StudyFieldData<MeanAge>();
        public StudyFieldData<MethodologicalQuality> MethodologicalQualities { get; set; } = new StudyFieldData<MethodologicalQuality>();

        private StudyData()
        {
            StudyDataImporter.LoadData(this);
        }

        private static StudyData instance;
        public static StudyData GetInstance()
        {
            if (instance == null)
            {
                instance = new StudyData();
            }

            return instance;
        }

        /**
         * Return all Study objects in the data store 
         * with a field containing the given term 
         */

        public List<Study> FindByValue(string value)
        {
            var results = from j in Studies
                          where j.PublicationYear.Contains(value)
                          || j.CorrelationCoefficient.Contains(value)
                          || j.Id.ToLower().Contains(value)
                          || j.SampleSize.Contains(value)
                          || j.VariablesControlled.Contains(value)
                          || j.StudyDesign.Contains(value)
                          || j.AdherenceMeasure.Contains(value)
                          || j.ConscientiousnessMeasure.Contains(value)
                          || j.MeanAge.Contains(value)
                          || j.MethodologicalQuality.Contains(value)
                          select j;

            return results.ToList();
        }
        
        /**
         * Returns results of search the studies data by key/value, using
         * inclusion of the search term
         */
         public List<Study> FindByColumnAndValue(StudyFieldType column, string value)
        {
            var results = from j in Studies
                          where GetFieldByType(j, column).Contains(value)
                          select j;

            return results.ToList();
        }

        /**
         * Returns the StudyField of the given type from the Study object
         * for all types other than StudyFIeldType.All. In this case,
         * null is returned
         */
         public static StudyField GetFieldByType(Study study, StudyFieldType type)
        {
            switch (type)
            {
                case StudyFieldType.PublicationYear:
                    return study.PublicationYear;
                case StudyFieldType.CorrelationCoefficient:
                    return study.CorrelationCoefficient;
                case StudyFieldType.SampleSize:
                    return study.SampleSize;
                case StudyFieldType.VariablesControlled:
                    return study.VariablesControlled;
                case StudyFieldType.MeanAge:
                    return study.MeanAge;
                case StudyFieldType.StudyDesign:
                    return study.StudyDesign;
                case StudyFieldType.AdherenceMeasure:
                    return study.AdherenceMeasure;
                case StudyFieldType.ConscientiousnessMeasure:
                    return study.ConscientiousnessMeasure;
                case StudyFieldType.MethodologicalQuality:
                    return study.MethodologicalQuality;
            }

            throw new ArgumentException("Cannot get field of type: " + type);
        }

        /**
         * Returns the Study with the given ID,
         * if it exists in the store
         */
         public Study Find(int id)
        {
            var results = from j in Studies
                          where j.ID == id
                          select j;
            return results.Single();
        }
    }
}

