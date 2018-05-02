using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    /* This class lists all the favourites from the text file
     * User can double click on a favourite
     * User can delete a favourite
     * User can edit a favourite 
     */


    public partial class ListFavourites : Form
    {
        private List<favourite> favourites;
        private BrowserWindow browserWindow1;
        private string name, link;
        private int count;

        public ListFavourites(BrowserWindow browserWindow)
        {
            InitializeComponent();


            browserWindow1 = browserWindow;

            // Create a list of favourites
            favourites = new List<favourite>();
            name = "";
            link = "";
            Shown +=lFavourites_Shown;
        }

        private void lFavourites_Shown(object sender, EventArgs e)
        {
            listFavourites();
        }

        // Populate list with favourites from textfile
        private void listFavourites()
        {
            StreamReader sr = new StreamReader("favourites.txt");
            
            count = 0;

            // Loop until end of stream
            while (!sr.EndOfStream)
            {
                string text = sr.ReadLine();

                // The name and link is seperated by tab
                // The split method seperates them
                string[] lines = text.Split('\t');

                // Counter for words in a line
                count = 0;

                foreach (string s in lines)
                {
                    // assign the first word in a line to name
                    if (count == 0)
                    {
                        name = s;
                    }

                    // assign the second word in a line to link
                    else
                    {
                        link = s;
                    }


                    count++;
                }


                favourite fav = new favourite(name, link);
                favourites.Add(fav);
             }
                
            sr.Close();
            
            // For each favourite in the list, add it to the listBox
            foreach (favourite v in favourites)
            {
                listBox1.Items.Add(v.getName() + "\t" + v.getLink());
            }
        }

        // Delete selected favourite
        private void delete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1) 
            { 
                int value = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(value);
                favourites.RemoveAt(value);

                // Write the changes to the file
                writeToFile();
                listBox1.Refresh();
            }

        }

        public void writeToFile()
        {
            TextWriter tw = new StreamWriter("favourites.txt");

            foreach (favourite f in favourites)
            {
                tw.WriteLine(f.getName() + '\t' + f.getLink());
            }

            tw.Close();
            
        }

        private void edit_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex > -1 && textBox1.Text.Length > 0)
            {

                // Split the item from the listBox which contains link and name
                string[] lines = listBox1.SelectedItem.ToString().Split('\t');

                // assign new name from textBox and link to text
                string text = textBox1.Text + '\t' + lines[1];

                // change name in the listbox 
                listBox1.Items[listBox1.SelectedIndex] = text;

                int itemIndex = listBox1.SelectedIndex;
                count = 0;

                
                foreach (favourite f in favourites)
                {
                    // Change old name to new name in the list
                    if (count == itemIndex)
                    {
                        f.setName(textBox1.Text);
                    }

                    count++;
                }

                // write changes to the file
                writeToFile();

                listBox1.Refresh();
            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {        
            string[] lines = listBox1.SelectedItem.ToString().Split('\t');
            browserWindow1.startUrlActions(lines[1]);

            this.Close();
        }

       

      

    }
}
