using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogApp.Models.MySQL;
using BlogApp.Actors;
using BlogApp.Commands;
using Akka.Actor;
using BlogApp.Models;
using BlogApp.Query;
using BlogApp.Models.MongoDB;
using MongoDB.Driver;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly MySqlDbContext _context;
        private readonly IActorRefFactory _actorRefFactory;
        private readonly IActorRef _eventRootActor;

        public PostsController(
            MySqlDbContext context, MongoDBContext mongoContext,
            IActorRefFactory actorRefFactory, IActorRef eventRootActor)
        {
            _context = context;
            _actorRefFactory = actorRefFactory;
            _eventRootActor = eventRootActor;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            var rootQueryActor = _actorRefFactory.ActorOf<QueryRootActor>();
            var posts = rootQueryActor.Ask<IAsyncCursor<PostList>>(new GetPostList());
            Console.WriteLine(posts.Result);
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
