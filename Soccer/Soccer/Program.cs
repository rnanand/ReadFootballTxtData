using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Soccer
{

    class Program
    {
        static readonly string textFile = @"F:\ReadFootballTxtData\soccer.txt";

        static void Main(string[] args)
        {
            Console.WriteLine(ConvertToDataTable(textFile));
            Console.ReadLine();
        }


        public static string ConvertToDataTable(string filePath)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add(new DataColumn("S.No"));
            tbl.Columns.Add(new DataColumn("Team"));
            tbl.Columns.Add(new DataColumn("P"));
            tbl.Columns.Add(new DataColumn("W"));
            tbl.Columns.Add(new DataColumn("L"));
            tbl.Columns.Add(new DataColumn("D"));
            tbl.Columns.Add(new DataColumn("F"));
            tbl.Columns.Add(new DataColumn("A"));
            tbl.Columns.Add(new DataColumn("Pts"));


            string[] lines = System.IO.File.ReadAllLines(filePath);

            for (int j = 2; j < lines.Length; j++)
            {
                if (!string.IsNullOrEmpty(lines[j]))
                {
                    var cols = lines[j].Split(' ');
                    cols = cols.Where(x => !string.IsNullOrEmpty(x) && x != "-").ToArray();

                    DataRow dr = tbl.NewRow();
                    for (int i = 0; i < cols.Length; i++)
                    {
                        dr[i] = cols[i];
                    }
                    tbl.Rows.Add(dr);

                }
            }

            Dictionary<string, int> doc = new Dictionary<string, int>();
            foreach (DataRow row in tbl.Rows)
            {
                foreach (DataColumn column in tbl.Columns)
                {
                    if (column.ToString() == "Team")
                    {
                        string Name = row["Team"].ToString();
                        if (Name != "")
                        {
                            int diff = Math.Abs(Convert.ToInt32(row["F"]) - Convert.ToInt32(row["A"]));
                            doc.Add(Name, diff);
                        }
                    }
                }
            }
            var item = doc.Where(e => e.Value == doc.Min(e2 => e2.Value)).First();
            return "Team name: " + item.Key + "\nMin Score: " + item.Value;
        }
    }
}