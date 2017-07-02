using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    public class BooksController: Controller
    {
        private ILibraryRepository _libraryRepository;

        public BooksController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet("api/authors/{authorId}/books")]
        public IActionResult GetBooksForAuthor(Guid authorId)
        {
            if (_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var booksForAuthorFromRepo = _libraryRepository.GetBooksForAuthor(authorId);
            var booksForAuthor = AutoMapper.Mapper.Map<IEnumerable<BookDto>>(booksForAuthorFromRepo);
            return Ok(booksForAuthor);

        }
        [HttpGet("{id}")]
        public IActionResult getBookForAuthor(Guid authorId, Guid id)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var booksForAuthorFromRepo = _libraryRepository.GetBookForAuthor(authorId, id);
            if (booksForAuthorFromRepo == null)
            {
                NotFound();
            }
            var booksForAuthor = AutoMapper.Mapper.Map<IEnumerable<BookDto>>(booksForAuthorFromRepo);
            return Ok(booksForAuthor);
        }
    }
}
