using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreService.Web.DTO;
using StoreService.Web.Models;
using StoreService.Web.Services;

namespace StoreService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IMapper mapper;
        private readonly IAuthorService authSvc;

        public AuthorsController(
            StoreContext context, 
            IMapper mapper,
            IAuthorService authSvc)
        {
            _context = context;
            this.mapper = mapper;
            this.authSvc = authSvc;
        }

        // GET: api/Authors
        [HttpGet]
        [Authorize("query")]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            return await _context.Authors
                .ProjectTo<AuthorDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [Authorize("query")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            _context.Entry(author)
                .Collection(c => c.ProgramBooks).Load();

            foreach (var pb in author.ProgramBooks)
            {
                _context.Entry(pb).Reference(r => r.Topic).Load();
                _context.Entry(pb).Collection(c => c.ProgramListings).Load();
            }

            return mapper.Map<AuthorDto>(author);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        [Authorize("update:author")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDto author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            _context.Entry(mapper.Map<Author>(author)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        [HttpPost]
        [Authorize("add:author")]
        public async Task<ActionResult<AuthorDto>> PostAuthor(AuthorDto author)
        {
            var authormod = mapper.Map<Author>(author);
            _context.Authors.Add(authormod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = authormod.AuthorId }, 
                mapper.Map<AuthorDto>(authormod));
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize("delete:author")]
        public async Task<ActionResult<AuthorDto>> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return mapper.Map<AuthorDto>(author);
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
