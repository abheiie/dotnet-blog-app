using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectC.Models;

namespace ProjectC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DatabaseCContext _context;

        public PostsController(DatabaseCContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            return await _context.Post.Include(p => p.Tag).ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        //get the post of particular tag
        //GET : api/Posts/GetPostOfParticularTag/5
        [HttpGet("GetPostOfParticularTag/{tag}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostOfParticularTag(string tag)
        {
            var post_obj = _context.Post.Include(p=>p.Tag).Where(x => x.Tag.Any(y => y.Name == tag)).ToList();
            if (post_obj == null)
            {
                return NotFound();
            }
            return post_obj;
        }


        // GET: api/Posts/GetPostDetail/5
        [HttpGet("GetPostDetail/{id}")]
        public async Task<ActionResult<Post>> GetPostDetail(int id)
        {
            var post = _context.Post.Include(po => po.Tag).Where(po => po.PostId == id).FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }


        // PUT: api/Posts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}
