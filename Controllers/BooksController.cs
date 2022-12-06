using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittleReaders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;





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

      return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
    }

    //Get api/books/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
      var book = await _db.Books.FindAsync(id);

      if (book == null)
      {
        return NotFound();
      }

      return book;
    }

    // Put api/books/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Book book)
    {
      if (id != book.BookId)
      {
        return BadRequest();
      }

      _db.Entry(book).State = EntityState.Modified;


      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BookExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Book> patchBookToPatch)
    {
      var bookToPatch = _db.Books.FirstOrDefault(e => e.BookId == id);

      if (bookToPatch == null)
      {
        return NotFound();
      }

      patchBookToPatch.ApplyTo(bookToPatch, ModelState);
      
      _db.Entry(bookToPatch).State = EntityState.Modified;
      await _db.SaveChangesAsync();
    
      return Ok(bookToPatch);
    }



    private bool BookExists(int id)
    {
      return _db.Books.Any(e => e.BookId == id);
    }

  }
}