using System;
using StackOverflow.Models;
using System.Collections.Generic;
using StackOverflow.Models.QueryParameters;

namespace StackOverflow.Repositories.Interface
{
    public interface IPostRepository
    {
        //CRUD operations for Posts
        public List<Post> GetAllPosts();                                                                                            //GET
        public Post GetById(int postId);                                                                                            //GET
        public Post GetByTitle(string title);                                                                                       //GET
        public Post CreatePost(Post post);                                                                                          //CREATE
        public Post UpdatePost(int id, Post post);                                                                                  //UPDATE
        public void DeletePost(int id);                                                                                             //DELETE

        //Additional commands
        public PaginatedList<Post> FilterBy(PostQueryParameters filterParameters);
        public int LikesCount(int postId);


        //Possible additional commands

        //Comment on other posts - should have that
        //like and edit users posts - should have that
    }
}
