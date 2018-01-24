using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogApp.Models;
using BlogApp.Actors;
using BlogApp.Commands;
using Akka.Actor;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly MySqlDbContext _context;
        private readonly MongoDBContext _mongoContext;
        private readonly IActorRefFactory _actorRefFactory;

        public PostsController(
            MySqlDbContext context, MongoDBContext mongoContext,
            IActorRefFactory actorRefFactory)
        {
            _context = context;
            _mongoContext = mongoContext;
            _actorRefFactory = actorRefFactory;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Post post)
        {
            var commandRootActor = _actorRefFactory.ActorOf<CommandRootActor>();
            var idCommandResult = await commandRootActor.Ask<CommandResult>(
                new SavePost(post.Title, post.Content));

            return RedirectToAction(nameof(Index));            
        }

    }
}
