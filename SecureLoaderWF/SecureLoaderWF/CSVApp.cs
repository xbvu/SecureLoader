using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

// thanks msdn 
// https://social.technet.microsoft.com/wiki/contents/articles/50922.c-importing-csv-content-and-populating-datagridview-in-windows-form-app-with-the-content.aspx

namespace CSVApp
{
    public class ReadCSV
    {
        public DataTable readCSV;

        public ReadCSV(string fileName, DataGridViewColumnCollection columns, bool firstRowContainsFieldNames = false)
        {
            readCSV = GenerateDataTable(fileName, columns, firstRowContainsFieldNames);
        }

        private static DataTable GenerateDataTable(string fileName, DataGridViewColumnCollection columns, bool firstRowContainsFieldNames = false)
        {
            DataTable result = new DataTable();

            if (fileName == "")
            {
                return result;
            }

            string delimiters = ",";
            string extension = Path.GetExtension(fileName);

            if (extension.ToLower() == "txt")
                delimiters = "\t";
            else if (extension.ToLower() == "csv")
                delimiters = ",";

            using (TextFieldParser tfp = new TextFieldParser(fileName))
            {
                tfp.SetDelimiters(delimiters);

                // Get The Column Names
                if (!tfp.EndOfData)
                {
                    //string[] fields = tfp.ReadFields();

                    //for (int i = 0; i < fields.Count(); i++)
                    //{
                    //    if (firstRowContainsFieldNames)
                    //       result.Columns.Add(fields[i]);
                    //   else
                    //        result.Columns.Add("Col" + i);
                    //}

                    //result.Columns.Add("TimeColumn");
                    //result.Columns.Add("TypeColumn");
                    //result.Columns.Add("MsgColumn");

                    foreach (DataGridViewColumn col in columns)
                    {
                        result.Columns.Add(col.Name);
                        col.DataPropertyName = col.Name;
                    }

                    // If first line is data then add it
                    //if (!firstRowContainsFieldNames)
                    //    result.Rows.Add(fields);
                }

                // Get Remaining Rows from the CSV
                while (!tfp.EndOfData)
                    result.Rows.Add(tfp.ReadFields());
            }

            return result;
        }
    }
}