using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Models;
using StackOverflow.Models.QueryParameters;
using StackOverflow.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext context;
        public PostRepository(ApplicationContext context)
        {
            this.context = context;
        }

        //CRUD methods for Post
        private IQueryable<Post> GetPosts()
        {
            return this.context.Posts
                    .Include(post => post.Comments)
                    .Include(post => post.CreatedBy);
        }
        public List<Post> GetAllPosts()
        {
            return this.GetPosts().ToList();
        }
        public Post GetById(int id)
        {
            Post post = this.context.Posts.Where(p => p.Id == id).FirstOrDefault();
            return post ?? throw new Exception();
        }
        public Post GetByTitle(string title)
        {
            Post post = this.context.Posts.Where(p => p.Title == title).FirstOrDefault();

            return post ?? throw new EntryPointNotFoundException();
        }
        public Post CreatePost(Post post)
        {
            this.context.Posts.Add(post);
            this.context.SaveChanges();

            return post;
        }
        public Post UpdatePost(int id, Post post)
        {
            Post postToUpdate = this.GetById(id);
            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            this.context.Update(postToUpdate);
            this.context.SaveChanges();

            return postToUpdate;
        }
        public void DeletePost(int id)
        {
            Post existingPost = this.GetById(id);
            this.context.Posts.Remove(existingPost);
            this.context.SaveChanges();
        }

        //Filters for User
        public PaginatedList<Post> FilterBy(PostQueryParameters filterParameters)
        {
            IQueryable<Post> result = this.context.Posts;
            result = FilterByTitle(result, filterParameters.Title);
            result = SortBy(result, filterParameters.SortBy);
            result = Order(result, filterParameters.SortOrder);
            int totalPages = (result.Count() + 1) / filterParameters.PageSize;
            result = Paginate(result, filterParameters.PageNumber, filterParameters.PageSize);

            return new PaginatedList<Post>(result.ToList(), totalPages, filterParameters.PageNumber);
        }
        private static IQueryable<Post> FilterByTitle(IQueryable<Post> posts, string title)
        {

            if (!string.IsNullOrEmpty(title))
            {
                return posts.Where(post => post.Title.Contains(title));
            }
            else
            {
                return posts;
            }
        } 
        private static IQueryable<Post> SortBy(IQueryable<Post> posts, string sortCriteria)
        {
            switch (sortCriteria)
            {
                case "title":
                    return posts.OrderBy(post => post.Title);
                case "content":
                    return posts.OrderBy(post => post.Content);
                // The following handles null or empty strings
                default:
                    return posts;
            }
        }
        private static IQueryable<Post> Order(IQueryable<Post> posts, string sortOrder)
        {
            switch (sortOrder)
            {
                case "desc":
                    return posts.Reverse();
                default:
                    return posts;
            }
        }
        private static IQueryable<Post> Paginate(IQueryable<Post> posts, int pageNumber, int pageSize)
        {
            return posts.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public int LikesCount(int postId)
        {
            Post currentPost = GetById(postId);

            return currentPost.Likes;
        }
    }
}
