using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Exceptions;
using StackOverflow.Helpers;
using StackOverflow.Models;
using StackOverflow.Models.QueryParameters;
using StackOverflow.Services.Interface;

namespace StackOverflow.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly AuthManager authManager;
        private readonly ModelMapper modelMapper;

        public UsersController(IUserService userService, AuthManager authManager, ModelMapper modelMapper)
        {
            this.modelMapper = modelMapper;
            this.userService = userService;
            this.authManager = authManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            try
            {
                this.authManager.Login(viewModel.Username, viewModel.Password);

                return this.RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedOperationException ex)
            {
                this.ModelState.AddModelError("Username", ex.Message);
                this.ModelState.AddModelError("Password", ex.Message);

                return this.View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            this.authManager.Logout();

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            if (this.userService.UsernameExists(viewModel.Username))
            {
                this.ModelState.AddModelError("Username", "User with same username already exists.");

                return this.View(viewModel);
            }

            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");

                return this.View(viewModel);
            }

            User user = this.modelMapper.Map(viewModel);
            this.userService.CreateUser(user);

            return this.RedirectToAction("Login", "Users");
        }
    }
}
