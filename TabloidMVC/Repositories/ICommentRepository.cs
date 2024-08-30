using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAllCommentsByPost(int PostId);
        void Add(Comment comment);
    }
}
