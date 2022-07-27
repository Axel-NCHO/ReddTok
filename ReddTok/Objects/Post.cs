using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReddTok.Objects
{
    public class Post
    {
        public string Subreddit { get ; }

        public Author Author { get ; } 

        public string Text { get ; }

        public List<Comment> Comments { get ; }



        public Post(string subreddit, Author author, string text)
        {
            this.Subreddit = subreddit;
            this.Author = author;
            this.Text = text;
            this.Comments = new();
        }
    }
}
