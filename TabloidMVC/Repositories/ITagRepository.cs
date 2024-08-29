using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        void Add(Tag tag);
        void Update(Tag tag);
        Tag GetTagById(int id);
    }
}
