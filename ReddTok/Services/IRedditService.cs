using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReddTok.Objects;

namespace ReddTok.Services
{
    public interface IRedditService
    {
        public Post GetPostFromReddit(string url, int? commentsCount);
    }
}
