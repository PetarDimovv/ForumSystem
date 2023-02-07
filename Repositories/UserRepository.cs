
using StackOverflow.Data;
using StackOverflow.Exceptions;
using StackOverflow.Models;
using StackOverflow.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Models.QueryParameters;
using StackOverflow.Models.DTOs;
using StackOverflow.Models.Enum;

namespace StackOverflow.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        } 
        public IQueryable<User> GetAllUsers()
        {
            return this.context.Users
                    .Include(user => user.Comments)
                    .Include(comments => comments.Posts);
        }
        public List<User> GetAll()
        {
            return this.GetAllUsers().ToList();
        }
        public User GetById(int id)
        {
            User user = this.GetAllUsers().Where(u => u.Id == id).FirstOrDefault();

            return user ?? throw new EntityNotFoundException("User with such details does not exist.");
        }
        public User GetByUsername(string username)
        {
            User user = this.context.Users.Where(p => p.Username == username).FirstOrDefault();

            return user ?? throw new EntityNotFoundException("User with such details does not exist.");
        }
        public User GetByEmail(string email)
        {
            User user = this.GetAllUsers().Where(u => u.Email == email).FirstOrDefault();

            return user ?? throw new EntityNotFoundException("User with such details does not exist.");
        }
        public User CreateUser(User user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }
        public User UpdateUser(int userId, User user)
        {
            User userToUpdate = this.GetById(userId);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password; 
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        } //Check this out - passoword!!
        public void DeleteUser(int userId)
        {
            User currentUser = this.GetById(userId);
            this.context.Users.Remove(currentUser);
            this.context.SaveChanges();
        }
        
        //Filters for User
        public List<User> FilterBy(UserQueryParameters filterParameters)
        {
            IQueryable<User> result = this.GetAllUsers();

            result = FilterByUsername(result, filterParameters.Username);
            result = FilterByEmail(result, filterParameters.Email);
            result = FilterByFirstName(result, filterParameters.FirstName);
            result = SortBy(result, filterParameters.SortBy);
            result = SortOrder(result, filterParameters.SortOrder);

            return result.ToList();
        }
        private static IQueryable<User> FilterByUsername(IQueryable<User> users, string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return users.Where(user => user.Username.Contains(username));
            }
            else
            {
                return users;
            }
        }
        private static IQueryable<User> FilterByEmail(IQueryable<User> users, string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return users.Where(user => user.Email.Contains(email));
            }
            else
            {
                return users;
            }
        }
        private static IQueryable<User> FilterByFirstName(IQueryable<User> users, string firstName)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                return users.Where(user => user.FirstName.Contains(firstName));
            }
            else
            {
                return users;
            }
        }
        private static IQueryable<User> SortBy(IQueryable<User> users, string sortCriteria)
        {
            switch (sortCriteria)
            {
                case "username":
                    return users.OrderBy(user => user.Username);
                case "first name":
                    return users.OrderBy(user => user.FirstName);
                case "last name":
                    return users.OrderBy(user => user.LastName);
                default:
                    return users;
            }
        }
        private static IQueryable<User> SortOrder(IQueryable<User> users, string sortOrder)
        {
            switch (sortOrder)
            {
                case "desc":
                    return users.Reverse();
                default:
                    return users;
            }
        }

        //Admin features for Admins
        public User MakeAdmin(string username, User user)
        {
            User currentUser = GetByUsername(username);
            user.Role = Role.Admin;
            currentUser.Role = user.Role;

            return currentUser;
        }
        public void BlockUser(int userId, User user)
        {
            User currentUser = GetById(userId);
            user.IsBlocked = true;
            currentUser.IsBlocked = user.IsBlocked;
        }
        public void UnblockUser(int userId, User user)
        {
            User currentUser = GetById(userId);
            user.IsBlocked = false;
            currentUser.IsBlocked = user.IsBlocked;
        }
        public bool IsBlocked(int userId)
        {
            User currentUser = GetById(userId);

            return currentUser.IsBlocked;
        }

        //CRUD methods for Phones
        public string GetPhone(User user)
        {
            User currentUser = user;

            return currentUser.Phone.ToString();
        }
        public string GetPhoneById(int userId)
        {
            User user = GetById(userId);

            return user.Phone.ToString();
        }
        public User GetUserByPhone(string phone)
        {
            User user = this.context.Users.Where(p => p.Phone == phone).FirstOrDefault();

            return user ?? throw new EntityNotFoundException("User with such details does not exist.");
        }
        public string GetByPhone(string phone)
        {
            User user = this.GetAllUsers().Where(u => u.Phone == phone).FirstOrDefault();
            string userPhone = user.Phone;

            return userPhone ?? throw new EntityNotFoundException("User with such details does not exist.");
        }

        public string CreatePhone(string phoneNumber, User user)
        {
            user.Phone = phoneNumber; 
            this.context.SaveChanges();
            
            return phoneNumber;

        }
        public string UpdatePhone(string newPhone, string oldPhone, User user)
        {
            User currentUser = this.GetByUsername(user.Username);
            currentUser.Phone = user.Phone;
            this.context.Update(currentUser);
            this.context.SaveChanges();

            return currentUser.Phone;
        }
        public void DeletePhone(string phone)
        {
            User user = this.GetUserByPhone(phone);
            user.Phone = null;
            this.context.SaveChanges();
        } 
        
    }
}
