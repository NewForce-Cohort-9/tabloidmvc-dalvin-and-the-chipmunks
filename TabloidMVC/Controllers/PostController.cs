using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers; 

// This attribute ensures that only authenticated users can access the actions in this controller
[Authorize]
public class PostController : Controller 
{
    // These private fields store references to repositories that interact with the data for posts and categories
    private readonly IPostRepository _postRepository;
    private readonly ICategoryRepository _categoryRepository;

    // This is the constructor. It gets called when the PostController is created.
    // It receives the repositories and stores them in the private fields above.
    public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
    {
        _postRepository = postRepository; // Store the post repository for later use
        _categoryRepository = categoryRepository; // Store the category repository for later use
    }

    // This action method handles requests to the main list of posts.
    // It retrieves all published posts and passes them to the view to be displayed.
    public IActionResult Index()
    {
        var posts = _postRepository.GetAllPublishedPosts(); // Get all published posts from the repository
        return View(posts); // Pass the posts to the view to be displayed
    }

    [Authorize]
    public IActionResult Edit(int id)
    {
        var posts = _postRepository.GetPublishedPostById(id); 
        return View(posts); // Pass the posts to the view to be displayed
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Post post)
    {
        //try
        //{
            post.UserProfileId = GetCurrentUserProfileId();
            _postRepository.UpdatePost(post);

            return RedirectToAction("Index");
        //}
        //catch (Exception ex)
        //{
        //    return View(post);
        //}
    }


    // This action method handles requests to view details of a single post.
    // It tries to get the post by its ID, first checking if it is published.
    // If the post isn't found, it checks if the current user is the author of the post.
    public IActionResult Details(int id)
    {
        var post = _postRepository.GetPublishedPostById(id); // Try to find the post by its ID, but only if it's published
        if (post == null)
        {
            // If the post is not found, get the current user's ID
            int userId = GetCurrentUserProfileId();

            // Try to find the post again, but this time include posts created by the current user
            post = _postRepository.GetUserPostById(id, userId);

            if (post == null)
            {
                // If the post is still not found, return a 404 Not Found response
                return NotFound();
            }
        }
        // If the post is found, pass it to the view to be displayed
        return View(post);
    }

    // This action method handles requests to show the "Create Post" form.
    // It prepares a view model that includes a list of categories the user can choose from.
    public IActionResult Create()
    {
        var vm = new PostCreateViewModel(); // Create a new view model for creating a post
        vm.CategoryOptions = _categoryRepository.GetAll(); // Get all categories and store them in the view model
        return View(vm); // Pass the view model to the view to be displayed
    }

    // This action method handles the form submission when a new post is created.
    // It tries to save the new post, and if it fails, it redisplays the form with an error message.
    [HttpPost]
    public IActionResult Create(PostCreateViewModel vm)
    {
        try
        {
            // Set the creation date of the post to the current date and time
            vm.Post.CreateDateTime = DateAndTime.Now;

            // Mark the post as approved
            vm.Post.IsApproved = true;

            // Set the ID of the user who is creating the post
            vm.Post.UserProfileId = GetCurrentUserProfileId();

            // Add the new post to the repository (i.e., save it to the database)
            _postRepository.Add(vm.Post);

            // Redirect the user to the details page for the newly created post
            return RedirectToAction("Details", new { id = vm.Post.Id });
        }
        catch
        {
            // If something goes wrong, reload the form with the existing data
            vm.CategoryOptions = _categoryRepository.GetAll(); // Re-fetch the categories in case of an error
            return View(vm); // Redisplay the form with the data the user entered
        }
    }

    // This is a helper method that gets the ID of the currently logged-in user.
    private int GetCurrentUserProfileId()
    {
        // Get the user ID from the current user's claims (a way to store information about the user)
        string id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Convert the user ID from a string to an integer and return it
        return int.Parse(id);
    }
}
