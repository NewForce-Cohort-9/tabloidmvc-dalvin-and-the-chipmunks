namespace TabloidMVC.Models.ViewModels
{
    public class PostCommentsViewModel
    {
        public Post Post { get; set; }
        public List<Comment>? Comments { get; set; }
        public int UserId { get; set; }
    }
}
