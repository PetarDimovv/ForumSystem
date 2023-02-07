using Microsoft.AspNetCore.Identity;
using StackOverflow.Models.Enum;
using System.Collections.Generic;

namespace StackOverflow.Models
{
    public class User
    {
        public int Id { get; set; } // Primaty key
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }   
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get;  set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsBlocked { get; set; }
        public Role Role { get; set; }

        public string Phone { get; set; }


        //public override bool Equals(object obj)
        //{
        //    User other = obj as User;
        //    if (obj != null)
        //    {
        //        return this.Id == other.Id
        //                && this.FirstName == other.FirstName
        //                && this.LastName == other.LastName
        //                && this.Email == other.Email
        //                && this.Username == other.Username;
        //    }
        //    else
         //    {
        //        return false;
        //    }
        //}
    }
}
