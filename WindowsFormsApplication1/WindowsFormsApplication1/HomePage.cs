using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class HomePage
    {
   
        public HomePage()
        { }


        public void write(string URI)
        {
            TextWriter tw = new StreamWriter("homepage.txt");

            // write a line of text to the file
            tw.WriteLine(URI);

            // close the stream
            tw.Close();
        }

        public string read()
        {

            StreamReader tr = new StreamReader("homepage.txt");
            string line; 
     
            // read a line of text
            line = tr.ReadLine();

            // close the stream
            tr.Close();

            // return homePage link
            return line;

      
            
        }
    }

}