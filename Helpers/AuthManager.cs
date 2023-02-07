using Microsoft.AspNetCore.Http;
using StackOverflow.Exceptions;
using StackOverflow.Models;
using StackOverflow.Services;
using StackOverflow.Services.Interface;

namespace StackOverflow.Helpers
{
    public class AuthManager
    {
        private const string CURRENT_USER = "CURRENT_USER";
        private readonly IUserService usersService;
        private readonly IPostService postService;
        private readonly IHttpContextAccessor contextAccessor;


        public AuthManager(IUserService usersService, IPostService postService, IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.usersService = usersService;
            this.postService = postService;
        }

        public User TryGetUser(string username)
        {
            try
            {
                return this.usersService.GetByUsername(username);
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorizedOperationException("Details Not Found");
            }
        }
        public User TryGetUser(string username, string password)
        {
            try
            {
                User user = this.usersService.GetByUsername(username);

                if (password != user.Password)
                {
                    throw new UnauthorizedOperationException("Invalid username or/and password");
                }

                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorizedOperationException("Invalid username or/and password");
            }
        }
        public Post TryGetPostById(int id)
        {
            try
            {
                return this.postService.GetById(id);
            }
            catch (EntityNotFoundException)
            {

                throw new UnauthorizedOperationException("Details Not Found");
            }
        }

        public User CurrentUser
        {
            get
            {
                try
                {
                    string username = this.contextAccessor.HttpContext.Session.GetString(CURRENT_USER);
                    User user = this.usersService.GetByUsername(username);
                    return user;
                }
                catch (EntityNotFoundException)
                {
                    return null;
                }
            }
            set
            {
                // User
                User user = value;
                if (user != null)
                {
                    // add username to session
                    this.contextAccessor.HttpContext.Session.SetString(CURRENT_USER, user.Username);
                }
                else
                {
                    this.contextAccessor.HttpContext.Session.Remove(CURRENT_USER);
                }
            }
        }
        public void Login(string username, string password)
        {
            this.CurrentUser = this.TryGetUser(username, password);

            if (this.CurrentUser == null)
            {
                int? loginAttempts = this.contextAccessor.HttpContext.Session.GetInt32("LOGIN_ATTEMPTS");

                if (loginAttempts.HasValue && loginAttempts == 5)
                {
                }
                else
                {
                    this.contextAccessor.HttpContext.Session.SetInt32("LOGIN_ATTEMPTS", (int)loginAttempts + 1);
                }

            }
        }
        public void Logout()
        {
            this.CurrentUser = null;
        }
    }
}


