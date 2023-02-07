using StackOverflow.Exceptions;
using StackOverflow.Models;
using StackOverflow.Models.QueryParameters;
using StackOverflow.Repositories.Interface;
using StackOverflow.Services.Interface;
using System.Collections.Generic;

namespace StackOverflow.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repository;

        public CommentService(ICommentRepository repository)
        {
            this.repository = repository;
        }

        //CRUD methods for Comment
        public List<Comment> GetAllComments()
        {
            return this.repository.GetAllComments();
        }
        public Comment GetById(int id)
        {
            return this.repository.GetById(id);
        }
        public Comment CreateComment(Post post, Comment comment, User user)
        {
            if (user.IsBlocked)
            {
                throw new UnauthorizedOperationException("Access denied.");
            }
            comment.CreatedBy = user;
            comment.Post = post;
            Comment createdComment = this.repository.CreateComment(comment);

            return createdComment;
        }
        public Comment UpdateComment(int id, Comment comment, User user)
        {
            Comment commentToUpdate = this.repository.GetById(id);
            if (!commentToUpdate.CreatedBy.Equals(user) && !user.IsAdmin)
            {
                throw new UnauthorizedOperationException("Can't update others posts!");
            }
            if (user.IsBlocked)
            {
                throw new UnauthorizedOperationException("Access denied.");
            }
            comment.CreatedBy = user;
            Comment updatedComment = this.repository.UpdateComment(id, comment);

            return updatedComment;
        }
        public void DeleteComment(int id, User user)
        {
            Comment comment = repository.GetById(id);
            if (!comment.CreatedBy.Equals(user) && !user.IsAdmin)
            {
                throw new UnauthorizedOperationException("Error Message!");
            }

            this.repository.DeleteComment(id);
        }

        //Filters for Comment
        public PaginatedList<Comment> FilterBy(CommentQueryParameters filterParameters)
        {
            return this.repository.FilterBy(filterParameters);
        } 
        public int LikesCount(int commentID)
        {
            return this.repository.LikesCount(commentID);
        }

    }
}
