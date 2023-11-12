using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebApiNet6.Data;
using WebApiNet6.Models;
using WebApiNet6.Repositories;

namespace WebApiNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly BookStoreContext _context;

        public BooksController(IBookRepository bookRepository, BookStoreContext context)
        {
            _bookRepository = bookRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var books = await _bookRepository.GetAllBookAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookAsync(id);
                if(book == null)
                    return NotFound();
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookmodel)
        {
            try
            {
                var newBookId = await _bookRepository.AddBookAsync(bookmodel);
                return StatusCode(StatusCodes.Status201Created, 
                    _context.Books.FirstOrDefault(b => b.Id == newBookId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookModel bookmodel)
        {
            try
            {
                if(id != bookmodel.Id)
                {
                    return BadRequest("khác id");
                }
                await _bookRepository.UpdateBookAsync(id, bookmodel);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return NotFound();
            try
            {
                await _bookRepository.DeleteBookAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
