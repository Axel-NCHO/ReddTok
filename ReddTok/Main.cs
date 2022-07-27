using System;
using ReddTok.Objects;
using ReddTok.Services;

namespace ReddTok
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            RedditService redditService = new();

            string url = "https://www.reddit.com/r/AskReddit/comments/w997iz/whats_a_massive_scandal_controversy_that_people/";

            try
            {
                Post post = redditService.GetPostFromReddit(url, 2);

                Console.WriteLine("Subreddit : " + post.Subreddit);
                Console.WriteLine("Author : " + post.Author.Pseudo);
                Console.WriteLine("Question : " + post.Text);

                if (post.Comments.Count != 0) Console.WriteLine("One comment : " + post.Comments[0].Author.Pseudo +
                     " - " + post.Comments[0].Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

    }
}
