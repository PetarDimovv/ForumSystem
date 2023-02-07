using StackOverflow.Models;
using Microsoft.Extensions.Hosting;
using StackOverflow.Models.QueryParameters;
using System.Collections.Generic;

namespace StackOverflow.Services.Interface
{
    public interface ICommentService
    {
        //CRUD operations for Posts
        public List<Comment> GetAllComments();                                                                                      //GET
        public Comment GetById(int commentId);                                                                                      //GET
        public Comment CreateComment(Post post, Comment comment, User user);                                                        //CREATE
        public Comment UpdateComment(int commentId, Comment comment, User user);                                                    //UPDATE
        public void DeleteComment(int commentId, User user);                                                                        //DELETE

        //Additional commands
        public PaginatedList<Comment> FilterBy(CommentQueryParameters filterParameters);
        public int LikesCount(int commentID);

    }
}
