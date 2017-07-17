using Library.API.Entities;
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
    [Route("api/authorcollections")]
    public class AuthorCollectionsController: Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorCollectionsController(ILibraryRepository libraryRepositry)
        {
            _libraryRepository = libraryRepositry;
        }
        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody] IEnumerable<AuthorForCreationDto> authorCollection)
        {
            if (authorCollection == null)
            {
                return BadRequest();
            }
            var authorEntities = AutoMapper.Mapper.Map<IEnumerable<Author>>(authorCollection);
            foreach (var author in authorEntities)
            {

                _libraryRepository.AddAuthor(author);
            }
            if(!_libraryRepository.Save())
            {
                throw new Exception("Creating an author collection failed on save");
            }
            return Ok();
        }

    }
}
