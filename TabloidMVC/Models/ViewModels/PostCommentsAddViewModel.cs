namespace TabloidMVC.Models.ViewModels
{
    public class PostCommentsAddViewModel
    {
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }
        public int UserId { get; set; }
    }
}
