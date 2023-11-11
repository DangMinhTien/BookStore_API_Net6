using WebApiNet6.Data;
using WebApiNet6.Models;

namespace WebApiNet6.Repositories
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAllBookAsync();
        public Task<Book> GetBookAsync(int id);
        public Task<int> AddBookAsync(BookModel model);

        public Task UpdateBookAsync(int id, Book model);
        public Task DeleteBookAsync(int id);
    }
}
