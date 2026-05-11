using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers;

public class HomeController : Controller
{
    private ToDoContext _db;

    public HomeController(ToDoContext context)
    {
        _db = context;
    }

    public IActionResult Index() => View(_db.ToDoItems.ToList());

    public IActionResult Details(int id)
    {
        var item = _db.ToDoItems
            .FirstOrDefault(t => t.Id == id);

        if (item == null)
        { 
            return NotFound();
        }

        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var item = _db.ToDoItems.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        
        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(ToDoItem todoItem)
    {
        if (!ModelState.IsValid)
        {
            return View(todoItem);
        }

        _db.ToDoItems.Update(todoItem);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Create(int id) => View();

    [HttpPost]
    public IActionResult Create(ToDoItem todoItem)
    {
        if (!ModelState.IsValid)
        {
            return View(todoItem);
        }
        
        _db.ToDoItems.Add(todoItem);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var todoItem = _db.ToDoItems.Find(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _db.ToDoItems.Remove(todoItem);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}