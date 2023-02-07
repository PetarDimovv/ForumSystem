using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Exceptions;
using StackOverflow.Helpers;
using StackOverflow.Models;
using StackOverflow.Models.DTOs;
using StackOverflow.Services.Interface;
using System;

namespace StackOverflow.Controllers
{
    [ApiController]
    [Route("api/comments")]
    
    public class CommentAPIController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly ModelMapper modelMapper;
        private readonly AuthManager authManager;
        public CommentAPIController(ICommentService commentService, ModelMapper modelMapper, AuthManager authManager)
        {
            this.commentService = commentService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
        }
        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            try
            {
                Comment comment = this.commentService.GetById(id);

                return this.StatusCode(StatusCodes.Status200OK, comment);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("{id}")]
        public IActionResult CreateComment(int id, [FromHeader] string username, [FromBody] CommentDTO commentDto)
        {

            try
            {
                User user = this.authManager.TryGetUser(username);
                Post post = this.authManager.TryGetPostById(id);
                Comment comment = this.modelMapper.MapComment(commentDto);
                Comment createdComment = this.commentService.CreateComment(post, comment, user);

                return this.StatusCode(StatusCodes.Status201Created, createdComment);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, [FromHeader] string username, [FromBody] CommentDTO dto)
        {
            try
            {
                User user = this.authManager.TryGetUser(username);
                Comment comment = this.modelMapper.MapComment(dto);

                Comment updatedComment = this.commentService.UpdateComment(id, comment, user);

                return this.StatusCode(StatusCodes.Status200OK, updatedComment);
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
        public IActionResult DeleteComment(int id, [FromHeader] string username)
        {
            try
            {
                User user = this.authManager.TryGetUser(username);
                this.commentService.DeleteComment(id, user);

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
