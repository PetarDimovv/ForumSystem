using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Exceptions;
using StackOverflow.Helpers;
using StackOverflow.Models;
using StackOverflow.Services.Interface;
using System.Collections.Generic;

namespace StackOverflow.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly AuthManager authManager;
        public PostsController(IPostService postService, AuthManager authManager)
        {
            this.postService = postService;
            this.authManager = authManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Post> posts = this.postService.GetAllPosts();
            return this.View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var post = new Post();
            return this.View(post);
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(post);
            }

            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var user = this.authManager.CurrentUser;
            var createdBeer = this.postService.CreatePost(post, user);

            return this.RedirectToAction("Details", "Posts", new { id = createdBeer.Id });
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction(actionName: "Login", controllerName: "Users");
            }

            try
            {
                var post = this.postService.GetById(id);

                if (post.UserId != this.authManager.CurrentUser.Id && !this.authManager.CurrentUser.IsAdmin)
                {
                    this.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = $"You cannot edit the post with id = {post.Id} since your are not the creator..";

                    return this.View("Error");
                }

                return this.View(post);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Post post)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(post);
            }

            try
            {
                var currentUser = this.authManager.CurrentUser;
                var updatedPost = this.postService.UpdatePost(id, post, currentUser);

                return this.RedirectToAction("Index", "Beers");
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
            catch (UnauthorizedOperationException ex)
            {
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var post = this.postService.GetById(id);

                return this.View(post);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id) //TODO authen, auther
        {
            try
            {
                //var user = this.userService.GetById(id);
                //this.postService.DeletePost(id, user); //TODO

                return this.RedirectToAction("Index", "Posts");
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }
        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                Post posts = this.postService.GetById(id);

                return View(posts);
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;

                return this.View(e.Message);
            }
        }
        
    }
}
