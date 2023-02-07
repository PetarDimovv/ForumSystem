using StackOverflow.Models;
using StackOverflow.Models.DTOs;
using StackOverflow.Services.Interface;

namespace StackOverflow.Helpers
{
	public class ModelMapper
	{
		private readonly IUserService userService;
		private readonly IPostService postService;

		public ModelMapper(IUserService userService, IPostService postService)
		{
			this.userService = userService;
			this.postService = postService;
		}
		public User MapUser(RegisterViewModel dto)
        {
			return new User
			{
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Email = dto.Email,
			    Username = dto.Username,
				Password = dto.Password
			};
        }
		public Post MapPost(PostDTO dto)
		{
			return new Post
			{
				Title = dto.Title,
				Content = dto.Content,
				CreatedBy = this.userService.GetById(dto.UserId)
			}; 
		}
		public Comment MapComment(CommentDTO dto)
        {
			return new Comment
			{
				Content = dto.Content,
				Post = this.postService.GetById(dto.PostId),
				CreatedBy = this.userService.GetById(dto.UserId)
			};
        }
		public User Map(RegisterViewModel viewModel)
		{
			return new User
			{
				Username = viewModel.Username,
				Password = viewModel.Password
			};
		}
	}
}
