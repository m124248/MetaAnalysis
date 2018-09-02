using MetaAnalysis.Models;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace MetaAnalysis.Services
{
    public class StudyDataService
    {
        static List<Study> AllStudies = new List<Study>();
        static bool IsDataLoaded = false;

        public static List<Study> FindAll()
        {
            LoadData();
            return new List<Study>(AllStudies);
        }

        public static List<object> FindAll(string column)
        {
            LoadData();
            List<object> values = new List<object>();

            var StudyColumn = typeof(Study).GetProperties();

            foreach (Study study in AllStudies)
            {
                object aValue = StudyColumn.Where(t => t.PropertyType.Name==column).Select(t => t.GetValue(study));
                if (!values.Contains(aValue))
                {
                    values.Add(aValue);
                }
            }
            return values;
        }

        //THIS IS WHERE I LEFT OFF
        public static List<Study> FindByValue(string value)
        {
            LoadData();
            List<Study> studies = new List<Study>();

            foreach (Study row in AllStudies)
            {
                foreach (string key in row.Keys)
                {
                    string aValue = row[key];

                    if (aValue.ToLower().Contains(value.ToLower()))
                    {
                        studies.Add(row);
                        break;
                    }
                }
            }
            return studies;
        }

        public static List<Study> FindByColumnAndValue(string column, string value)
        {
            LoadData();

            List<Study> studies = new List<Study>();

            foreach (Study row in AllStudies)
            {
                string aValue = row[column];

                if (aValue.ToLower().Contains(value.ToLower()))
                {
                    studies.Add(row);
                }
            }
            return studies;
        }

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

        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

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
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
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