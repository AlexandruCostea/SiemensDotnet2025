namespace LibraryAPI.Service;

using LibraryAPI.Repo;
using LibraryAPI.Model;
using LibraryAPI.Data;

public class BookService : IBookService
{
    private readonly IBookRepo _bookRepository;

    public BookService(IBookRepo bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync() =>
        await _bookRepository.GetAllAsync();

    public async Task<Book?> GetBookByIdAsync(int id) =>
        await _bookRepository.GetByIdAsync(id);

    public async Task<Book> AddBookAsync(Book book) =>
        await _bookRepository.AddAsync(book);

    public async Task<Book> UpdateBookAsync(int id, Book book)
    {
        var existingBook = await _bookRepository.GetByIdAsync(id);
        if (existingBook == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Quantity = book.Quantity;
        existingBook.BorrowedQuantity = book.BorrowedQuantity;
        existingBook.TotalBorrowedCount = book.TotalBorrowedCount;

        await _bookRepository.UpdateAsync(existingBook);
        return existingBook;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var existingBook = await _bookRepository.GetByIdAsync(id);
        if (existingBook == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        await _bookRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<Book>> GetBooksByTitleAsync(string title) =>
        await _bookRepository.GetByTitleAsync(title);

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author) =>
        await _bookRepository.GetByAuthorAsync(author);

    public async Task<IEnumerable<Book>> GetBooksByQuantityGreaterEqualAsync(int quantity) =>
        await _bookRepository.GetByQuantityGreaterEqualAsync(quantity);
    
    public async Task<IEnumerable<Book>> GetBooksByQuantityLessAsync(int quantity) =>
        await _bookRepository.GetByQuantityLessAsync(quantity);

    public async Task<bool> IsBookAvailableAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        return book.Quantity > book.BorrowedQuantity;
    }

    public async Task<bool> BorrowBookAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        if (book.Quantity <= book.BorrowedQuantity)
        {
            return false;
        }

        book.BorrowedQuantity++;
        book.TotalBorrowedCount++;
        await _bookRepository.UpdateAsync(book);
        return true;
    }

    public async Task<bool> ReturnBookAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        if (book.BorrowedQuantity <= 0)
        {
            return false;
        }

        book.BorrowedQuantity--;
        await _bookRepository.UpdateAsync(book);
        return true;
    }

    public async Task<IEnumerable<Book>> GetTopBorrowedBooksAsync(int count) =>
        await _bookRepository.GetTopBorrowedAsync(count);
}