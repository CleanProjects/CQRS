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
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IActorRefFactory _actorRefFactory;
        private readonly IActorRef _eventRootActor;

        public PostsController(
            IActorRefFactory actorRefFactory, IActorRef eventRootActor)
        {
            _actorRefFactory = actorRefFactory;
            _eventRootActor = eventRootActor;
        }

        public IActionResult Index()
        {
            var rootQueryActor = _actorRefFactory.ActorOf<QueryRootActor>();
            var posts = rootQueryActor.Ask<IAsyncCursor<PostList>>(new GetPostList());
            ViewBag.IsLoggedIn = IsLoggedIn;

            return View(posts.Result.ToEnumerable());
        }

        public IActionResult Details(int id)
        {
            var rootQueryActor = _actorRefFactory.ActorOf<QueryRootActor>();
            var post = rootQueryActor.Ask<PostDetails>(new GetPostDetails(id));
            ViewBag.IsLoggedIn = IsLoggedIn;

            return View(post.Result);
        }

        public IActionResult Create()
        {
            var user = HttpContext.Session.GetString("user");
            var password = HttpContext.Session.GetString("password");
            ViewBag.IsLoggedIn = IsLoggedIn;

            if (IsLoggedIn)
            {
                return View();
            }

            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Post post)
        {
            if (IsLoggedIn) 
            {
                var commandRootActor = _actorRefFactory.ActorOf<CommandRootActor>();
                var idCommandResult = await commandRootActor.Ask<CommandResult>(
                    new SavePost(post.Title, post.Content)); 
            }

            return RedirectToAction(nameof(Index)); 
        }

       public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            }
            );
        }


    }
}
