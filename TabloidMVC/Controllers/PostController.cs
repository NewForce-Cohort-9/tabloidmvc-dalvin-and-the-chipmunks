using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            List<Tag> tags = _tagRepository.GetTagsByPostId(id);

            var post = _postRepository.GetPublishedPostById(id);

            PostDetailsViewModel vm = new PostDetailsViewModel()
            {
                Post = post,
                Tags = tags
            };

            return View(vm);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            }
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }
        public IActionResult CreatePostTag(int postId)
        {
            var vm = new PostTagCreateViewModel();
            vm.Post = new Post { Id = postId };
            vm.Tags = _tagRepository.GetAllTags();
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreatePostTag(PostTagCreateViewModel vm, int id)
        {
            try
            {
                vm.PostTag.PostId = id;

                _postTagRepository.Add(vm.PostTag);

                return RedirectToAction("Details", new { id = vm.PostTag.PostId });
            }
            catch
            {
                vm.Tags = _tagRepository.GetAllTags();
                return View(vm);
            }
        }

        [Route("Post/Tags/{id}")]
        public IActionResult IndexPostTag(int id)
        {
            var tags = _tagRepository.GetTagsByPostId(id);

            var post = _postRepository.GetPublishedPostById(id);

            IndexPostTagViewModel vm = new IndexPostTagViewModel()
            {
                Post = post,
                Tags = tags,
            };

            return View(vm);
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}