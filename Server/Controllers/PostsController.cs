using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;
using System.Linq;

using aspnet102.Server.Data;
using aspnet102.Server.Models;

namespace aspnet102.Server.Controllers;

public class PostsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PostsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Post
    public IActionResult Index()
    {
        var posts = _context.Posts.ToList();
        return View(posts);
    }

    // GET: Post/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Post/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Title,Content")] Post post)
    {
        if (ModelState.IsValid)
        {
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            _context.Add(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }

    // GET: Post/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = _context.Posts.Find(id);
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    // POST: Post/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Title,Content")] Post post)
    {
        if (id != post.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                post.UpdatedAt = DateTime.Now;
                _context.Update(post);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Posts.Any(e => e.Id == post.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }

    // GET: Post/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = _context.Posts
            .FirstOrDefault(m => m.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // POST: Post/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var post = _context.Posts.Find(id);
        _context.Posts.Remove(post);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
