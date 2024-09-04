namespace TabloidMVC.Models.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public List<Tag> Tags { get; set; }
        public List<PostTag> PostTags { get; set; }
    }
}