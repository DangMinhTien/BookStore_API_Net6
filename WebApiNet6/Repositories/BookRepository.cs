using WebApiNet6.Models;
using WebApiNet6.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApiNet6.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddBookAsync(BookModel model)
        {
            Book newBook = _mapper.Map<Book>(model);
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task DeleteBookAsync(int id)
        {
            var deleteBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if(deleteBook != null)
            {
                _context.Books.Remove(deleteBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookModel>> GetAllBookAsync()
        {
            var books = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> GetBookAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task UpdateBookAsync(int id, BookModel model)
        {
            if(id == model.Id)
            {
                Book updateBook = _mapper.Map<Book>(model);
                _context.Books.Update(updateBook);
                await _context.SaveChangesAsync();
            }
        }
    }
}
