using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReddTok.Objects
{
    public class Author
    {
        public string Pseudo { get ; }

        public Author(string pseudo)
        {
            this.Pseudo = pseudo;
        }
    }
}
