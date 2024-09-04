namespace TabloidMVC.Models.ViewModels
{
    public class IndexPostTagViewModel
    {
        public Post Post { get; set; }
        public PostTag PostTag { get; set; }
        public Tag Tag { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
