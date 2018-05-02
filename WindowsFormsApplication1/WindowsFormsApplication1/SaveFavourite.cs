using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class SaveFavourite : Form
    {
        private BrowserWindow f;
        private string name, link;
        private object lockFavourite;

        public SaveFavourite(BrowserWindow f1)
        {
            InitializeComponent();
            lockFavourite = new object();
            f = f1;
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                name = textBox1.Text;
                link = label1.Text;

                TextWriter tw = new StreamWriter("favourites.txt", true);

                lock (lockFavourite)
                {
                    tw.WriteLine(name + "\t" + link);
                    tw.Close();
                }

                this.Close();
            }
        }

        public void setLabel(string text)
        {
            label1.Text = text;
        }

    }
}
