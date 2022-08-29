using CSVApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureLoaderWF
{
    public partial class LogViewer : Form
    {
        public LogViewer()
        {
            InitializeComponent();
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AllowUserToAddRows = false;
            LoadCSVOnDataGridView("secure_loader_logs.csv");
            var lineCount = File.ReadLines("secure_loader_logs.csv").Count();
            long length = new System.IO.FileInfo("secure_loader_logs.csv").Length;
            double kb_lenght = Math.Round((double)length / 1024, 1);
            toolStripStatusLabel1.Text = "File size: " + kb_lenght.ToString() + "KB";
            toolStripStatusLabel2.Text = "Line count: " + lineCount.ToString();
            
            
        }

        private void LoadCSVOnDataGridView(string fileName)
        {
            try
            {
                DataGridViewColumnCollection columns = GetColumns();
                ReadCSV csv = new ReadCSV(fileName, columns);

                try
                {
                    dataGridView1.DataSource = csv.readCSV;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                        if (row.Cells[1].Value.ToString() == ("INFO"))
                        {
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                        else if (row.Cells[1].Value.ToString() == ("WARNING"))
                        {
                            row.DefaultCellStyle.BackColor = Color.Orange;
                        }
                        else if (row.Cells[1].Value.ToString() == ("ERROR"))
                        {
                            row.DefaultCellStyle.BackColor = Color.OrangeRed;
                        }

                        }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataGridViewColumnCollection GetColumns()
        {
            return dataGridView1.Columns;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadCSVOnDataGridView("secure_loader_logs.csv");
            var lineCount = File.ReadLines("secure_loader_logs.csv").Count();
            long length = new System.IO.FileInfo("secure_loader_logs.csv").Length;
            double kb_lenght = Math.Round((double)length / 1024, 1);
            toolStripStatusLabel2.Text = "Line count: " + lineCount.ToString();
            toolStripStatusLabel1.Text = "File size: " + kb_lenght.ToString() + "KB";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("TimeColumn LIKE '%{0}%' OR TypeColumn LIKE '%{0}%' OR MsgColumn LIKE '%{0}%'", textBox1.Text);
            toolStripStatusLabel3.Text = "Items found: " + (dataGridView1.DataSource as DataTable).DefaultView.Count.ToString();
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (row.Cells[1].Value.ToString() == ("INFO"))
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
                else if (row.Cells[1].Value.ToString() == ("WARNING"))
                {
                    row.DefaultCellStyle.BackColor = Color.Orange;
                }
                else if (row.Cells[1].Value.ToString() == ("ERROR"))
                {
                    row.DefaultCellStyle.BackColor = Color.OrangeRed;
                }
        
        
    }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
