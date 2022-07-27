using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReddTok.Objects
{
    public class Comment
    {
        public string Text { get; }

        public Author Author { get; }


        public Comment(Author author, string text)
        {
            this.Author = author;
            this.Text = text;
        }
    }
}
