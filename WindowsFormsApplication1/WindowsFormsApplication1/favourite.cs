using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class favourite
    {
        private string name, link;

        public favourite(string name, string link)
        {
            this.name = name;
            this.link = link;
        }

        // Accessor for link
        public string getLink()
        {
            return link;
        }

        // Accessor for name
        public string getName()
        {
            return name;
        }

        // Mutator for name
        public void setName(string sname)
        {
            name = sname;
        }

    }
}
