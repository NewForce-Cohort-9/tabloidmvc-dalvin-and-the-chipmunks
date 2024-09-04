using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class PostTagController : Controller
    {
        private readonly IPostTagRepository _postTagRepository;
        public PostTagController(IPostTagRepository postTagRepository)
        {
            _postTagRepository = postTagRepository;
        }
        // GET: PostTagController
        public ActionResult Index()
        {
            var postTags = _postTagRepository.GetAll();
            return View(postTags);
        }

        // GET: PostTagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostTagController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostTagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostTag postTag)
        {
            try
            {
                _postTagRepository.Add(postTag);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(postTag);
            }
        }

        // GET: PostTagController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostTagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostTagController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostTagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
