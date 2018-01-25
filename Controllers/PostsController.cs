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
        private readonly IActorRefFactory _actorRefFactory;
        private readonly IActorRef _eventRootActor;

        public PostsController(
            IActorRefFactory actorRefFactory, IActorRef eventRootActor)
        {
            _actorRefFactory = actorRefFactory;
            _eventRootActor = eventRootActor;
        }

        public async Task<IActionResult> Index()
        {
            var rootQueryActor = _actorRefFactory.ActorOf<QueryRootActor>();
            var posts = rootQueryActor.Ask<IAsyncCursor<PostList>>(new GetPostList());

            return View(posts.Result.ToEnumerable());
        }

        public async Task<IActionResult> Details(int id)
        {
            var rootQueryActor = _actorRefFactory.ActorOf<QueryRootActor>();
            var post = rootQueryActor.Ask<PostDetails>(new GetPostDetails(id));

            return View(post.Result);
        }

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
