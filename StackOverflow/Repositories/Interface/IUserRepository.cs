using System;
using StackOverflow.Models;
using StackOverflow.Models.DTOs;
using System.Collections.Generic;
using StackOverflow.Models.QueryParameters;
using System.Linq;

namespace StackOverflow.Repositories.Interface
{
    public interface IUserRepository
    {
        //CRUD operations for Users
        public List<User> GetAll();                                                                                                 //GET
        public User GetById(int userId);                                                                                            //GET
        public User GetByUsername(string username);                                                                                 //GET
        public User GetByEmail(string email);                                                                                       //GET
        public User CreateUser(User user);                                                                                          //CREATE
        public User UpdateUser(int userId, User user);                                                                              //UPDATE
        public void DeleteUser(int userId);                                                                                         //DELETE
        
        //Additional commands
        public List<User> FilterBy(UserQueryParameters filterParameters);
        public /*List<*/User/*>*/ MakeAdmin(string username, User user); //TODO - used to be a list- not sure what is better
        public void BlockUser(int userId, User user);
        public void UnblockUser(int userId, User user);
        public bool IsBlocked(int userId);

        //CRUD operations for Phones
        public string GetPhone(User user);                                                                                          //GET
        public string GetByPhone(string phone);                                                                                     //GET
        public string GetPhoneById(int userId);                                                                                     //GET
        public User GetUserByPhone(string phone);                                                                                   //GET
        public string CreatePhone(string phoneNumber, User user);                                                                   //CREATE
        public string UpdatePhone(string newPhone, string oldPhone, User user);                                                     //UPDATE
        public void DeletePhone(string phone);                                                                                      //DELETE


        //Possible additional commands

        //public User LogIn(User newUser); //TODO - must check what parameters are needed to use(def not User)
        //public User LogOut(User newUser); //TODO - must check what parameters are needed to use(def not User)

    }
}
