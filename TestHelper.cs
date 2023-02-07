using StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflow.Test
{
    public class TestHelper
    {
        public static User TestUser
        {
            get
            {
                return new User
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "User",
                    Email = "test@email.com",
                    Username = "TestUser1",
                };
            }
        }

        public static Post TestPost
        {
            get
            {
                return new Post
                {
                    Id = 1,
                    Title = "TestTitle",
                    Content = "Test",
                };
            }
        }

        public static Comment TestComment
        {
            get
            {
                return new Comment
                {
                    Id = 1,
                    Content = "Test",
                };
            }
        }
    }
}
