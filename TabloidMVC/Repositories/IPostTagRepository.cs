using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostTagRepository
    {
        void Add(PostTag postTag);
        List<PostTag> GetAll();
    }
}
