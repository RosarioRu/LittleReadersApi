using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittleReaders.Models;

namespace LittleReaders.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly LittleReadersContext _db;

    public BooksController(LittleReadersContext db)
    {
      _db = db;
    }

    // GET api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
      return await _db.Books.ToListAsync();
    }

    // POST api/Books
    [HttpPost]
    public async Task<ActionResult<Book>> Post(Book book)
    {
      _db.Books.Add(book);
      await _db.SaveChangesAsync();

      return CreatedAtAction("Post", new { id = book.BookId }, book);
    }
  }
}