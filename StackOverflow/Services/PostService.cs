using StackOverflow.Exceptions;
using StackOverflow.Models;
using StackOverflow.Models.QueryParameters;
using StackOverflow.Repositories.Interface;
using StackOverflow.Services.Interface;
using System;
using System.Collections.Generic;
using StackOverflow.Models.Enum;
using System.ComponentModel.Design;

namespace StackOverflow.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;
        public PostService(IPostRepository repository)
        {
            this.repository = repository;
        }

        //CRUD methods for Post
        public List<Post> GetAllPosts()
        {
            return this.repository.GetAllPosts();
        }
        public Post GetById(int id)
        {
            return this.repository.GetById(id);
        }
        public Post GetByTitle(string title)
        {
            return this.repository.GetByTitle(title);
        }//TODO
        public Post CreatePost(Post post, User user)
        {
            bool duplicatePostExists = true;

            try
            {
                this.repository.GetByTitle(post.Title);
            }
            catch (Exception)
            {
                duplicatePostExists = false;
            }

            if (duplicatePostExists)
            {
                throw new DuplicateEntityException("Already exists!"); 
            }
            post.CreatedBy = user;
            Post createdPost = this.repository.CreatePost(post);

            return createdPost;
        }
        public Post UpdatePost(int id, Post post, User user)
        {
            Post postToUpdate = this.repository.GetById(id);
            if (!postToUpdate.CreatedBy.Equals(user) && user.Role != Role.Admin)
            {
                throw new UnauthorizedOperationException("Can't update others posts!");
            }
            bool duplicateExists = true;
            try
            {
                Post existingPost = this.repository.GetByTitle(post.Title);
                if (existingPost.Id == id)
                {
                    duplicateExists = false;
                }
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }
            if (duplicateExists)
            {
                throw new DuplicateEntityException("Already exists!");
            }

            post.CreatedBy = user;
            Post updatedPost = this.repository.UpdatePost(id, post);

            return updatedPost;
        }
        public void DeletePost(int id, User user)
        {
            Post post = repository.GetById(id);
            if (!post.CreatedBy.Equals(user) && !user.IsAdmin || user.Role != Role.User)//Might not need the 2nd part of the If
            {
                throw new UnauthorizedOperationException("Error Message!");
            }

            this.repository.DeletePost(id);
        }

        //Filters for User
        public PaginatedList<Post> FilterBy(PostQueryParameters filterParameters)
        {
            return this.repository.FilterBy(filterParameters);
        }
        public int LikesCount(int postId)
        {
            return this.repository.LikesCount(postId);
        }//TODO - i think we need a like counter
    }
}
