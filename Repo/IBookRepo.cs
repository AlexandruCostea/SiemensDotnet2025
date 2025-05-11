namespace LibraryAPI.Repo;
using LibraryAPI.Model;

public interface IBookRepo
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);

    Task<IEnumerable<Book>> GetByTitleAsync(string title);
    Task<IEnumerable<Book>> GetByAuthorAsync(string author);
    Task<IEnumerable<Book>> GetByQuantityGreaterEqualAsync(int quantity);
    Task<IEnumerable<Book>> GetByQuantityLessAsync(int quantity);
    Task<IEnumerable<Book>> GetTopBorrowedAsync(int count);
}