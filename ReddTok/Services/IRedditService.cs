using ReddTok.Objects;

namespace ReddTok.Services
{
    public interface IRedditService
    {
        public Post GetPostFromReddit(string url, int? commentsCount);
    }
}
