using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiHomeController : ControllerBase
{
    private ToDoContext _db;

    public ApiHomeController(ToDoContext context)
    {
        _db = context;
    }

    [HttpGet]
    public ActionResult<List<ToDoItem>> GetAll()
    {
        return _db.ToDoItems.ToList();
    }
    
    [HttpGet("{id}")]
    public ActionResult<ToDoItem> Details(int id)
    {
        var toDoItem = _db.ToDoItems.Find(id);
        if  (toDoItem == null)
        {
            return NotFound();
        }
        
        return toDoItem;
    }

    [HttpPost]
    public IActionResult Create(ToDoItem toDoItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        _db.ToDoItems.Add(toDoItem);
        _db.SaveChanges();
        return CreatedAtAction(nameof(Details), new { id = toDoItem.Id }, toDoItem);
    }

    [HttpPut]
    public IActionResult Update(int id, ToDoItem toDoItem)
    {
        if (id != toDoItem.Id)
        {
            return BadRequest();
        }

        _db.ToDoItems.Update(toDoItem);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var toDoItem = _db.ToDoItems.Find(id);
        if (toDoItem == null)
        {
            return NotFound();
        }

        _db.ToDoItems.Remove(toDoItem);
        _db.SaveChanges();
        return NoContent();
    }
}