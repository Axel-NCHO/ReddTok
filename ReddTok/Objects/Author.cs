namespace ReddTok.Objects
{
    /// <summary>
    /// Represents the author of a post or a comment
    /// </summary>
    public class Author
    {
        public string Pseudo { get ; }

        /// <summary>
        /// Creates a new Athor object
        /// </summary>
        /// <param name="pseudo"></param>
        public Author(string pseudo)
        {
            this.Pseudo = pseudo;
        }
    }
}
