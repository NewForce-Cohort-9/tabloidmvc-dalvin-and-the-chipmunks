using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        void Add(Tag tag);
        void Update(Tag tag);
        void Delete(int id);
        Tag GetTagById(int id);
        //List<Tag> GetTagsByPostId(int postId);

    }
}
