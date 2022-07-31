namespace ReddTok.Objects
{
    /// <summary>
    /// Represents a post
    /// </summary>
    public class Post
    {
        public string Subreddit { get ; }

        public Author Author { get ; } 

        public string Text { get ; }

        public List<Comment> Comments { get ; }


        /// <summary>
        /// Creates a new Post object
        /// </summary>
        /// <param name="subreddit"></param>
        /// <param name="author"></param>
        /// <param name="text">The title of the post</param>
        public Post(string subreddit, Author author, string text)
        {
            this.Subreddit = subreddit;
            this.Author = author;
            this.Text = text;
            this.Comments = new();
        }
    }
}
