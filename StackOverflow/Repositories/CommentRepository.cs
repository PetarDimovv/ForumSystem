using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Exceptions;
using StackOverflow.Models;
using StackOverflow.Models.QueryParameters;
using StackOverflow.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext context;

        //CRUD methods for Comment
        public CommentRepository(ApplicationContext context)
        {
            this.context = context;
        }
        private IQueryable<Comment> GetComments() 
        {
            return this.context.Comments;
        }
        public List<Comment> GetAllComments()
        {
            return this.GetComments().ToList();
        }
        public Comment GetById(int id)
        {
            Comment comment = this.context.Comments.Where(c => c.Id == id).FirstOrDefault();
            return comment ?? throw new EntityNotFoundException("Comment not found!");
        }
        public Comment CreateComment(Comment comment)
        {
            this.context.Comments.Add(comment);
            this.context.SaveChanges();

            return comment;
        }
        public Comment UpdateComment(int id, Comment comment)
        {
            Comment commentToUpdate = this.GetById(id);
            commentToUpdate.Content = comment.Content;
            this.context.Update(commentToUpdate);
            this.context.SaveChanges();

            return commentToUpdate;
        }
        public void DeleteComment(int id)
        {
            Comment existingComment = this.GetById(id);
            this.context.Comments.Remove(existingComment);
            this.context.SaveChanges();
        }

        //Filters for Comment
        public PaginatedList<Comment> FilterBy(CommentQueryParameters filterParameters)
        {
            IQueryable<Comment> result = this.GetComments();

            result = FilterByUsername(result, filterParameters.Username);
            result = FilterById(result, filterParameters.Id);
            int totalPages = (result.Count() + 1) / filterParameters.PageSize;
            result = Paginate(result, filterParameters.PageNumber, filterParameters.PageSize);

            return new PaginatedList<Comment>(result.ToList(), totalPages, filterParameters.PageNumber);
        }
        private static IQueryable<Comment> FilterByUsername(IQueryable<Comment> comments, string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return comments.Where(comment => comment.CreatedBy.Username.Contains(username));
            }
            else
            {
                return comments;
            }
        }
        private static IQueryable<Comment> FilterById(IQueryable<Comment> comments, int? id)
        {
            if (id.HasValue)
            {
                return comments.Where(comment => comment.Id >= id);
            }
            else
            {
                return comments;
            }
        }
        private static IQueryable<Comment> Paginate(IQueryable<Comment> comments, int pageNumber, int pageSize)
        {
            return comments.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public int LikesCount(int commentID)
        {
            Comment currentComment = this.GetById(commentID);

            return currentComment.Likes;
        }

        //TODO - should we add method like?
    }
}
