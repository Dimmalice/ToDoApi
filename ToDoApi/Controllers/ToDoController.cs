using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoApi.Models;


namespace ToDoApi.Controllers
{
    [Route("api/ToDo")] 
   // [Route("api/[controller]")]
    [ApiController]  //access sto controller base
    public class TodoController : ControllerBase //to controller base dinei access se methodous NotFound kai File
    {
        private readonly ToDoContext _context;  //dependency injection

        public TodoController(ToDoContext context)
        {
            _context = context;

           
        }


        [HttpGet]  //gia na girisoume dedomena se entity form me GET
        public ActionResult<List<ToDoItem>> GetAll()     //methodos pou epistrefei ta ===>todoitems
        {
            return _context.ToDoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetToDo" )] //to GetToDo einai name kai dimiourgei ena http link
        public ActionResult<ToDoItem> GetById(long id) //prosthetei to ID sto URL
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound(); //404
            }
            return item; //200 OK
        }


        [HttpPost]
        public IActionResult Create(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetToDo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, ToDoItem item)
        {
            var todo = _context.ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.ToDoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}