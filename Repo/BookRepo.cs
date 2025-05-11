namespace LibraryAPI.Repo;

using LibraryAPI.Model;
using LibraryAPI.Data;
using Microsoft.EntityFrameworkCore;

 

public class BookRepo : IBookRepo
{
    private readonly LibraryContext _context;

    public BookRepo(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync() =>
        await _context.Books.ToListAsync();

    public async Task<Book?> GetByIdAsync(int id) =>
        await _context.Books.FindAsync(id);

    public async Task<Book> AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await GetByIdAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Book>> GetByTitleAsync(string title) =>
        await _context.Books.Where(b => b.Title.Contains(title)).ToListAsync();

    public async Task<IEnumerable<Book>> GetByAuthorAsync(string author) =>
        await _context.Books.Where(b => b.Author.Contains(author)).ToListAsync();

    public async Task<IEnumerable<Book>> GetByQuantityGreaterEqualAsync(int quantity) =>
        await _context.Books.Where(b => b.Quantity >= quantity).ToListAsync();

    public async Task<IEnumerable<Book>> GetByQuantityLessAsync(int quantity) =>
        await _context.Books.Where(b => b.Quantity < quantity).ToListAsync();

    public async Task<IEnumerable<Book>> GetTopBorrowedAsync(int count) =>
        await _context.Books
            .OrderByDescending(b => b.BorrowedQuantity)
            .Take(count)
            .ToListAsync();
}