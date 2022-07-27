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
    public class RedditService : IRedditService
    {
        public Post GetPostFromReddit(string url, int? commentsCount)
        {
            // Fetch post from url
            var json = this.GetStringFromUrl($"{url}.json");
            var jsonArray = JArray.Parse(json);
            if (!jsonArray.HasValues) throw new ArgumentException("No content under url");

            // Parse post
            var postData = jsonArray[0]?.SelectTokens("$..children").First().SelectToken("$..data");
            if (postData == null) throw new NullReferenceException("Post is null");
            var post = this.ParsePost(postData);

            if (jsonArray.Count <= 1) return post;

            // Parse comments
            var comments = jsonArray[1]?.SelectTokens("$..children[?(@.kind == 't1')]").ToArray();
            if (comments == null) throw new NullReferenceException("Comments are null");
            for (int i = 0; i < commentsCount && i < comments.Length; i++)
            {
                var commentData = comments[i].SelectToken("$.data");
                if (commentData == null) throw new NullReferenceException("Comment is null");
                post.Comments.Add(this.ParseComment(commentData));
            }

            return post;

            throw new NotImplementedException();
        }

        private string GetStringFromUrl(string url)
        {
            using WebClient webClient = new();
            var json = webClient.DownloadString(url);
            return json;
        }

        private Post ParsePost(JToken postData)
        {
            var subreddit = postData.SelectToken("$.subreddit")?.Value<string>();
            var author = postData.SelectToken("$.author")?.Value<string>();
            var title = postData.SelectToken("$.title")?.Value<string>();
            var text = postData.SelectToken("$.selftext")?.Value<string>()?.Trim(); //.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x));
            // var flair = postData.SelectToken("$.link_flair_text")?.Value<string>() ?? "";

            if ((subreddit == null) || (author == null) || (title == null) || (text == null)) throw new NullReferenceException();

            return new Post(subreddit,
                new Author(author),
                title);
        }

        private Comment ParseComment(JToken commentData)
        {
            var author = commentData.SelectToken("$.author")?.Value<string>();
            var text = commentData.SelectToken("$.body")?.Value<string>()?.Trim(); //.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x));

            if ((author == null) || (text == null)) throw new NullReferenceException();

            return new Comment(new Author(author), text);
        }
    }
}
