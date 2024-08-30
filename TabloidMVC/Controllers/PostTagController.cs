using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;

namespace TabloidMVC.Controllers
{
    public class PostTagController : Controller
    {
        // GET: PostTagController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PostTagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostTagController/Create
        public ActionResult Create()
        {
            try
            {
                _postTagRepository.Add(PostTag postTag)
            }
        }

        public ActionResult Create(Tag tag)
        {
            try
            {
                _tagRepository.Add(tag);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }

        // POST: PostTagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
