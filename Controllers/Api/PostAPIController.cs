using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Models;
using StackOverflow.Services.Interface;
using System;
using System.Collections.Generic;
using StackOverflow.Helpers;
using StackOverflow.Exceptions;
using StackOverflow.Models.DTOs;

namespace StackOverflow.Controllers
{
    [ApiController]
	[Route("api/posts")]
    
    public class PostAPIController : ControllerBase 
	{
		private readonly IPostService postService;
        private readonly ModelMapper modelMapper;
        private readonly AuthManager authManager;

        public PostAPIController(IPostService postServices, ModelMapper modelMapper, AuthManager authManager)
        {
            this.postService = postServices;
            this.authManager = authManager;
			this.modelMapper = modelMapper;

		}
        [HttpGet("")]
		public IActionResult GetAllPosts() 
		{
			List<Post> result = this.postService.GetAllPosts();

			return this.StatusCode(StatusCodes.Status200OK, result);
		}

        [HttpGet("{id}")]
		public IActionResult GetPostById(int id) 
		{
			try
			{
				Post post = this.postService.GetById(id);

				return this.StatusCode(StatusCodes.Status200OK, post);
			}
			catch (EntryPointNotFoundException e)
			{
				return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
			}
		}

        [HttpPost("")]
        public IActionResult CreatePost([FromHeader] string username, [FromBody] PostDTO postDTO)
        {
            try
            {
                User user = this.authManager.TryGetUser(username);
                Post post = this.modelMapper.MapPost(postDTO);
                Post createdPost = this.postService.CreatePost(post, user);

                return this.StatusCode(StatusCodes.Status201Created, createdPost);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
        }
        [HttpPut("{id}")]
		public IActionResult UpdatePost(int id, [FromHeader] string username, [FromBody] PostDTO dto)
		{
			try
			{
				User user = this.authManager.TryGetUser(username);
				Post post = this.modelMapper.MapPost(dto);

				Post updatedPost = this.postService.UpdatePost(id, post, user);

				return this.StatusCode(StatusCodes.Status200OK, updatedPost);
			}
			catch (UnauthorizedOperationException e)
			{
				return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
			}
			catch (EntityNotFoundException e)
			{
				return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
			}
			catch (DuplicateEntityException e)
			{
				return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeletePost(int id, [FromHeader] string username)
		{
			try
			{
				User user = this.authManager.TryGetUser(username);
				this.postService.DeletePost(id, user);

				return this.StatusCode(StatusCodes.Status200OK);
			}
			catch (UnauthorizedOperationException e)
			{
				return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
			}
			catch (EntityNotFoundException e)
			{
				return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
			}
		}
	}
}
