using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Model;
using LibraryAPI.Service;

namespace LibraryAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _service.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        if (book == null)
        {
            return BadRequest("Book cannot be null");
        }

        var createdBook = await _service.AddBookAsync(book);
        return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
    {
        if (book == null)
        {
            return BadRequest("Book cannot be null");
        }

        try
        {
            var updatedBook = await _service.UpdateBookAsync(id, book);
            return Ok(updatedBook);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await _service.DeleteBookAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("search/title/{title}")]
    public async Task<IActionResult> GetBooksByTitle(string title)
    {
        var books = await _service.GetBooksByTitleAsync(title);
        return Ok(books);
    }

    [HttpGet("search/author/{author}")]
    public async Task<IActionResult> GetBooksByAuthor(string author)
    {
        var books = await _service.GetBooksByAuthorAsync(author);
        return Ok(books);
    }

    [HttpGet("search/quantity/greater-equal/{quantity}")]
    public async Task<IActionResult> GetBooksByQuantityGreaterEqual(int quantity)
    {
        var books = await _service.GetBooksByQuantityGreaterEqualAsync(quantity);
        return Ok(books);
    }

    [HttpGet("search/quantity/less/{quantity}")]
    public async Task<IActionResult> GetBooksByQuantityLess(int quantity)
    {
        var books = await _service.GetBooksByQuantityLessAsync(quantity);
        return Ok(books);
    }

    [HttpGet("{id}/availability")]
    public async Task<IActionResult> IsBookAvailable(int id)
    {
        try
        {
            var isAvailable = await _service.IsBookAvailableAsync(id);
            return Ok(isAvailable);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("{id}/borrow")]
    public async Task<IActionResult> BorrowBook(int id)
    {
        try
        {
            var success = await _service.BorrowBookAsync(id);
            if (success)
            {
                return Ok("Book borrowed successfully.");
            }
            return BadRequest("Book is not available for borrowing.");
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        try
        {
            var success = await _service.ReturnBookAsync(id);
            if (success)
            {
                return Ok("Book returned successfully.");
            }
            return BadRequest("Book was not borrowed.");
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("top-borrowed/{count}")]
    public async Task<IActionResult> GetTopBorrowedBooks(int count)
    {
        var books = await _service.GetTopBorrowedBooksAsync(count);
        return Ok(books);
    }
}