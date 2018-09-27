using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaAnalysis.Models;

namespace MetaAnalysis.Data
{
    public class StudyFieldData<TField> where TField : StudyField, new()
    {
        private List<TField> allFields = new List<TField>();

        private void Add(TField field)
        {
            allFields.Add(field);
        }

        public List<TField> ToList()
        {
            return allFields;
        }

        public TField Find(int id)
        {
            var results = from field in allFields
                          where field.ID == id
                          select field;

            return results.Single();
        }

        internal TField AddUnique(string fieldValue)
        {

            var results = from field in allFields
                          where field.Value.Equals(fieldValue)
                          select field;

            TField theField;

            if (!results.Any())
            {
                theField = new TField();
                theField.Value = fieldValue;

                Add(theField);
            }
            else
            {
                theField = results.Single();
            }

            return theField;

        }

    }
}
