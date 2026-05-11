namespace ToDoApp.Models;

public class ToDoItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}