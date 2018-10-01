using System.IO;
using System.Collections.Generic;
using System.Text;
using MetaAnalysis.Models;
using System.Data;

namespace MetaAnalysis.Data
{
    public class StudyDataImporter
    {
        private static bool IsDataLoaded = false;

        /**
         * Load and parse data from dat_mes.csv
         */

        internal static void LoadData(StudyData studyData)
        {
            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("Data/dat_mes.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArray = CSVRowToStringArray(line);
                    if (rowArray.Length > 0)
                    {
                        rows.Add(rowArray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            /**
             * Parse each row array into a Study object.
             * Assumes CSV column ordering:
             *     id,PublicationYear,n,r,VariablesControlled,StudyDesign,
             *     AdherenceMeasure,ConscientiousnessMeasure ,MeanAge,MethodologicalQuality
             */
            foreach (string[] row in rows)
            {
                PublicationYear publicationYear = studyData.PublicationYears.AddUnique(row[1]);
                CorrelationCoefficient correlationCoefficient = studyData.CorrelationCoefficients.AddUnique(row[2]);
                SampleSize sampleSize = studyData.SampleSizes.AddUnique(row[3]);
                VariablesControlled variablesControlled = studyData.VariablesControlled.AddUnique(row[4]);
                StudyDesign studyDesign = studyData.StudyDesigns.AddUnique(row[5]);
                AdherenceMeasure adherenceMeasure = studyData.AdherenceMeasures.AddUnique(row[6]);
                ConscientiousnessMeasure conscientiousnessMeasure = studyData.ConscientiousnessMeasures.AddUnique(row[7]);
                MeanAge meanAge = studyData.MeanAges.AddUnique(row[8]);
                MethodologicalQuality methodologicalQuality = studyData.MethodologicalQualities.AddUnique(row[9]);

                Study newStudy = new Study
                {
                    Id = row[0],
                    PublicationYear = publicationYear,
                    CorrelationCoefficient = correlationCoefficient,
                    SampleSize = sampleSize,
                    VariablesControlled = variablesControlled,
                    StudyDesign = studyDesign,
                    AdherenceMeasure = adherenceMeasure,
                    ConscientiousnessMeasure = conscientiousnessMeasure,
                    MeanAge = meanAge,
                    MethodologicalQuality = methodologicalQuality
                };
                studyData.Studies.Add(newStudy);
            }

            IsDataLoaded = true;
        }

        /**
         * Parse a single line of a CSV file into a string array
         */

        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            //Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();

        }

        public static DataTable ConvertCSVtoDataTable()
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = File.OpenText("Data/dat_mes.csv"))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
