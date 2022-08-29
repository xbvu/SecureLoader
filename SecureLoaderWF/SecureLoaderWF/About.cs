using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureLoaderWF
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            label2.Text = label2.Text + " " + "2019-05";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://pngimage.net/sl-logo-png-1/");
        }
    }
}
