using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


/* This lists the history from the text file
 * User can double click on a link and this will be displayed in the main window
 */


namespace WindowsFormsApplication1
{
    public partial class ListHistory : Form
    {
        private BrowserWindow browserWindow1;

        public ListHistory(BrowserWindow browserWindow)
        {
            InitializeComponent();
            browserWindow1 = browserWindow;

            // load history to the listBox
            Shown += ListHistory_Shown;
        }

        private void ListHistory_Shown(object sender, EventArgs e)
        {
            listHistory();
        }

        // Populate listBox with history from text file
        private void listHistory()
        {
            StreamReader tw = new StreamReader("history.txt");
           
            while (!tw.EndOfStream)
            {
                string text = tw.ReadLine();
                listBox1.Items.Add(text);
            }

                tw.Close();
            
        }

        // Double click 
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            // get current selected item
            string curItem = listBox1.SelectedItem.ToString();
   
            // load the page into the browser window
            browserWindow1.startUrlActions(curItem);

            this.Close();
        }

        
    }
}
