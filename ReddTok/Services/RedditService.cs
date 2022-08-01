using ReddTok.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ReddTok.Services
{
    /// <summary>
    /// Converts reddit post data to exploitable objects
    /// </summary>
    public class RedditService : IRedditService
    {
        /// <summary>
        /// Creates a Post object from reddit post url
        /// </summary>
        /// <param name="url">Url to reddit post</param>
        /// <param name="commentsCount">Number of comments collected</param>
        /// <returns>The Post object created</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public Post GetPostFromReddit(string url, int? commentsCount)
        {
            // Fetch post from url
            Console.WriteLine($"Fetching reddit post at url {url}");
            var json = this.GetStringFromUrl($"{url}.json");
            var jsonArray = JArray.Parse(json);
            if (!jsonArray.HasValues) throw new ArgumentException("No content under url");

            // Parse post
            var postData = jsonArray[0]?.SelectTokens("$..children").First().SelectToken("$..data");
            if (postData == null) throw new NullReferenceException("Post is null");
            var post = this.ParsePost(postData);

            if (jsonArray.Count <= 1) Console.WriteLine("End fetching reddit post");  return post;

            // Parse comments
            var comments = jsonArray[1]?.SelectTokens("$..children[?(@.kind == 't1')]").ToArray();
            if (comments == null) throw new NullReferenceException("Comments are null");
            for (int i = 0; i < commentsCount && i < comments.Length; i++)
            {
                var commentData = comments[i].SelectToken("$.data");
                if (commentData == null) throw new NullReferenceException("Comment is null");
                post.Comments.Add(this.ParseComment(commentData));
            }
            Console.WriteLine("End fetching reddit post");
            return post;
        }

        /// <summary>
        /// Download JSON from web page 
        /// </summary>
        /// <param name="url">Url to the web page</param>
        /// <returns>JSON file downloaded</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string GetStringFromUrl(string url)
        {
            using WebClient webClient = new();
            var json = webClient.DownloadString(url);
            if (json == null) throw new ArgumentNullException("Wrong url format");
            return json;
        }

        /// <summary>
        /// Parse JSON to create Post object
        /// </summary>
        /// <param name="postData">JToken containing data related to the reddit post</param>
        /// <returns>The Post object created</returns>
        /// <exception cref="NullReferenceException"></exception>
        private Post ParsePost(JToken postData)
        {
            var subreddit = postData.SelectToken("$.subreddit")?.Value<string>();
            var author = postData.SelectToken("$.author")?.Value<string>();
            var title = postData.SelectToken("$.title")?.Value<string>();
            var text = postData.SelectToken("$.selftext")?.Value<string>()?.Trim();

            if ((subreddit == null) || (author == null) || (title == null) || (text == null)) throw new NullReferenceException();

            return new Post(subreddit,
                new Author(author),
                title);
        }

        /// <summary>
        /// Parse JSON to create Comment object
        /// </summary>
        /// <param name="commentData">JToken containing data reated to a reddit comment</param>
        /// <returns>The Comment object created</returns>
        /// <exception cref="NullReferenceException"></exception>
        private Comment ParseComment(JToken commentData)
        {
            var author = commentData.SelectToken("$.author")?.Value<string>();
            var text = commentData.SelectToken("$.body")?.Value<string>()?.Trim();

            if ((author == null) || (text == null)) throw new NullReferenceException();

            return new Comment(new Author(author), text);
        }
    }
}
