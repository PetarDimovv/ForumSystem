using StackOverflow.Models;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using StackOverflow.Models.QueryParameters;

namespace StackOverflow.Repositories.Interface
{
    public interface ICommentRepository
    {
        //CRUD operations for Comment
        public List<Comment> GetAllComments();                                                                                      //GET
        public Comment GetById(int commentId);                                                                                      //GET
        public Comment CreateComment(Comment comment);                                                                              //CREATE
        public Comment UpdateComment(int commentId, Comment comment);                                                               //UPDATE
        public void DeleteComment(int commentId);                                                                                   //DELETE

        //Additional commands
        public PaginatedList<Comment> FilterBy(CommentQueryParameters filterParameters);
        public int LikesCount(int commentID);
    }
}
