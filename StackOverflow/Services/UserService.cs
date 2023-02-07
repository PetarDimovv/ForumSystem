using StackOverflow.Models;

using System;
using System.Collections.Generic;
using StackOverflow.Services.Interface;
using StackOverflow.Repositories.Interface;
using StackOverflow.Exceptions;
using StackOverflow.Models.Enum;
using System.Linq;
using StackOverflow.Models.QueryParameters;

namespace StackOverflow.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        //CRUD methods for User
        public List<User> GetAll()
        {
            return this.repository.GetAll();
        }
        public User GetById(int id)
        {
            return this.repository.GetById(id);
        }
        public User GetByUsername(string username)
        {
            return this.repository.GetByUsername(username);
        }
        public bool UsernameExists(string username)
        {
            bool usernameExists = true;

            try
            {
                _ = this.repository.GetByUsername(username);
            }
            catch (EntityNotFoundException)
            {
                usernameExists = false;
            }

            return usernameExists;
        }
        public User GetByEmail(string username)
        {
            return this.repository.GetByEmail(username);
        }                                                               
        public User CreateUser(User user)
        {
            bool duplicateUsername = true;
            bool duplicateEmail = true;

            try
            {
                this.repository.GetByUsername(user.Username);
            }
            catch (EntityNotFoundException)
            {
                duplicateUsername = false;
            }

            try
            {
                this.repository.GetByEmail(user.Email);
            }
            catch (EntityNotFoundException)
            {
                duplicateEmail = false;
            }

            if (duplicateUsername)
            {
                throw new DuplicateEntityException($"User {user.Username} already exists.");
            }
            if (duplicateEmail)
            {
                throw new DuplicateEntityException($"User {user.Email} already exists.");
            }

            User createdUser = this.repository.CreateUser(user);

            return createdUser;
        }
        public User UpdateUser(int userId, User user)
        {
            User userToUpdate = this.repository.GetByUsername(user.Username);
            if (userToUpdate.IsBlocked.Equals(user) && user.Role != Role.Admin) //Add to check the if hte user is the user
            {
                throw new UnauthorizedOperationException("You do not have permission to do such action.");
            }

            bool duplicateExists = true;

            try
            {
                User existingUser = this.repository.GetByUsername(user.Username);
                if (existingUser.Email == user.Email)
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
                throw new DuplicateEntityException($"User {user.Email} already exists.");
            }

            User updatedUser = this.repository.UpdateUser(userId, user);

            return updatedUser;
        }//TODO - Check for password and how to encrypt it
        public void DeleteUser(int userId, User user)
        {
            User userToDelete = repository.GetById(userId);
            if (userToDelete.Id != user.Id && user.Role != Role.Admin || user.Role != Role.User)
            {
                throw new UnauthorizedOperationException("You do not have permission to do such action.");
            }

            this.repository.DeleteUser(userId);
        }

        //Filters for User
        public List<User> FilterBy(UserQueryParameters filterParameters)
        {
            return this.repository.FilterBy(filterParameters);
        }

        //Admin features for Admins
        public User MakeAdmin(string username, User user)
        {
            User userToAdmin = this.repository.GetByUsername(username);

            if (user.Role != Role.Admin && userToAdmin.Role == Role.Admin)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }

            return this.repository.MakeAdmin(username, user);

        }
        public void BlockUser(int userId, User user)
        {
            if (user.Role != Role.Admin)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }

            this.repository.BlockUser(userId, user);
        } 
        public void UnblockUser(int userId, User user)
        {
            if (user.Role != Role.Admin)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }

            this.repository.UnblockUser(userId, user);
        }
        public bool IsBlocked(int userId)
        {
            User userChecking = this.repository.GetById(userId);
            if (userChecking.Role != Role.Admin)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }

            return this.repository.IsBlocked(userId);
        }

        //CRUD methods for Phones
        public string GetPhone(User user)
        {
            User userToAdmin = this.repository.GetByUsername(user.Username);

            if (user.Role != Role.Admin && userToAdmin.Phone == null)
            {
                throw new UnauthorizedAccessException("Error, such phone doesn't exist or unatorized access.");
            }

            return this.repository.GetPhone(user);
        }
        public string GetPhoneById(int userId)
        {
            User userToAdmin = this.repository.GetById(userId);

            if (userToAdmin.Role != Role.Admin && userToAdmin.Phone == null)
            {
                throw new UnauthorizedAccessException("Error, such phone doesn't exist or unatorized access.");
            }

            return this.repository.GetPhoneById(userId);
        }
        public string GetByPhone(string phone)
        {
            User user = this.GetAll().Where(b => b.Phone == phone).FirstOrDefault();
            string userPhone = user.Phone;

            if (user.Role != Role.Admin && user.Phone == null)
            {
                throw new UnauthorizedAccessException("Error, such phone doesn't exist or unatorized access.");
            }
            return this.repository.GetByPhone(phone);
        }
        public string CreatePhone(string phoneNumber, User user)
        {
            if (user.Phone != null && GetByPhone(phoneNumber) != null && user.IsAdmin != true)
            {
                throw new DuplicateEntityException($"The phone you are trying to add already exists, or you already have a phone number - {user.Phone}");
            }

            return phoneNumber;
        }
        public string UpdatePhone(string newPhone, string oldPhone, User user)
        {
            User userToUpdate = this.repository.GetById(user.Id);
            if (!user.IsAdmin)
            {
                throw new UnauthorizedOperationException("Access denied.");
            }

            bool duplicateExists = true;
            try
            {
                string existingPhone = this.repository.GetByPhone(oldPhone);
                string possibleDup = this.repository.GetByPhone(newPhone); //Checks if the new phone is in the DB
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }

            if (duplicateExists)
            {
                throw new DuplicateEntityException($"Phone {newPhone} already exists.");
            }

            string updatedPhone = this.repository.UpdatePhone(newPhone, oldPhone, user);

            return updatedPhone;
        }
        public void DeletePhone(string phone, User user)
        {
            User userToDelete = this.repository.GetById(user.Id);

            if (userToDelete.Id != user.Id && !user.IsAdmin)
            {
                throw new UnauthorizedOperationException("Access denied.");
            }

            this.repository.DeletePhone(phone);
        }

    }
}
