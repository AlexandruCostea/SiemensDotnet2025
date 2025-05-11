using LibraryAPI.Model;


namespace LibraryAPI.Service;



public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);
    Task<IEnumerable<Book>> GetBooksByTitleAsync(string title);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
    Task<IEnumerable<Book>> GetBooksByQuantityGreaterEqualAsync(int quantity);
    Task<IEnumerable<Book>> GetBooksByQuantityLessAsync(int quantity);
    Task<bool> IsBookAvailableAsync(int id);
    Task<bool> BorrowBookAsync(int id);
    Task<bool> ReturnBookAsync(int id);
    Task<IEnumerable<Book>> GetTopBorrowedBooksAsync(int count);
}
