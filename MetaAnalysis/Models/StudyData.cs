using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MetaAnalysis.Models
{
    public class StudyData
    {
        static List<Dictionary<string, string>> AllStudies = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;

        public static List<Dictionary<string, string>> FindAll()
        {
            LoadData();

            // Bonus mission: return a copy
            return new List<Dictionary<string, string>>(AllStudies);
        }

        /*
         * Returns a list of all values contained in a given column,
         * without duplicates. 
         */
        public static List<string> FindAll(string column)
        {
            LoadData();

            List<string> values = new List<string>();

            foreach (Dictionary<string, string> study in AllStudies)
            {
                string aValue = study[column];

                if (!values.Contains(aValue))
                {
                    values.Add(aValue);
                }
            }

            // Bonus mission: sort results alphabetically
            values.Sort();
            return values;
        }

        /**
         * Search all columns for the given term
         */
        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> studies = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> row in AllStudies)
            {

                foreach (string key in row.Keys)
                {
                    string aValue = row[key];

                    if (aValue.ToLower().Contains(value.ToLower()))
                    {
                        studies.Add(row);

                        // Finding one field in a study that matches is sufficient
                        break;
                    }
                }
            }

            return studies;
        }

        /**
         * Returns results of search the studiess data by key/value, using
         * inclusion of the search term.
         *
         * FROM TECHJOBS: For example, searching for employer "Enterprise" will include results
         * with "Enterprise Holdings, Inc".
         */
        public static List<Dictionary<string, string>> FindByColumnAndValue(string column, string value)
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> studies = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> row in AllStudies)
            {
                string aValue = row[column];

                if (aValue.ToLower().Contains(value.ToLower()))
                {
                    studies.Add(row);
                }
            }

            return studies;
        }

        /*
         * Load and parse data from dat_mes.csv
         */
        private static void LoadData()
        {

            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("Models/dat_mes.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            // Parse each row array into a more friendly Dictionary
            foreach (string[] row in rows)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllStudies.Add(rowDict);
            }

            IsDataLoaded = true;
        }

        /*
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
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
    }
}

