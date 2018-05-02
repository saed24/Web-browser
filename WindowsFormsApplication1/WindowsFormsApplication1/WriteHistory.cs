using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class WriteHistory
    {

        public WriteHistory() 
        {
            
        }

        public void writeHistory(string url)
        {
            TextWriter tw = new StreamWriter("history.txt", true);

            // write a line of text to the file
            tw.WriteLine(url);

            // close the stream
            tw.Close();
            
        }
    }
}
