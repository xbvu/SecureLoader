using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureLoaderWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            IntPtr le_int_ptr = Marshal.StringToHGlobalAnsi("127.0.0.1");
            Program.pSession = Program.CreateSession(le_int_ptr, 5000);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 50; // specify interval time as you want
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        public String GetValue()
        {
            StringBuilder str = new StringBuilder(512);

            Program.__GetValue__(str, 512);

            return str.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr username = Marshal.StringToHGlobalAnsi(textBox1.Text);
            IntPtr hash = Marshal.StringToHGlobalAnsi(SHA256HexHashString(textBox2.Text));
            Program.CallSessionLogin(Program.pSession, username, hash);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = GetValue();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
        private static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }

        private static string SHA256HexHashString(string StringIn)
        {
            string hashString;
            using (var sha256 = SHA256Managed.Create())
            {
                var hash = sha256.ComputeHash(Encoding.Default.GetBytes(StringIn));
                hashString = ToHex(hash, false);
            }

            return hashString;
        }

        private void showLogsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LogViewer logviewer = new LogViewer();
            logviewer.Show();
        }
    }
}
