using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.API.Services;
using Library.API.Models;
using AutoMapper;
using Library.API.Entities;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _libraryRepository.GetAuthors();
            var authors = Mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);
            return Ok(authors);          
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetAuthors(Guid id)
        {
            var authorsFromRepo = _libraryRepository.GetAuthor(id);
            if (authorsFromRepo == null)
            {
                return NotFound();
            }
            var authors = Mapper.Map<AuthorDto>(authorsFromRepo);
            return Ok(authors);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorForCreationDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            var authorEntity = Mapper.Map<Author>(author);
            _libraryRepository.AddAuthor(authorEntity);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Creating an author failed on save.");
            }
            var authorToReturn = Mapper.Map<AuthorDto>(authorEntity);

            return CreatedAtRoute("GetAuthor", 
                new { id = authorToReturn.Id},
                authorToReturn);
        }
    }

}
