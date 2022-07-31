namespace ReddTok.Objects
{
    /// <summary>
    /// Represents a comment
    /// </summary>
    public class Comment
    {
        public string Text { get; }

        public Author Author { get; }

        /// <summary>
        /// Creates an new Comment object
        /// </summary>
        /// <param name="author"></param>
        /// <param name="text"></param>
        public Comment(Author author, string text)
        {
            this.Author = author;
            this.Text = text;
        }
    }
}
