using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace StackOverflow.Models
{
    public class Post
    {
        public int Id { get; set; } // Priamry key
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; } // Foreign key
        public User CreatedBy { get; set; }
        public List<Comment> Comments { get; set; }
        public int Likes { get; set; }
        public int CommentCount { get; set; }


        //public override bool Equals(object obj)
        //{
        //    Post other = obj as Post;
        //    if (obj != null)
        //    {
        //        return this.Id == other.Id
        //                && this.Title == other.Title
        //                && this.Content == other.Content
        //                && this.UserId == other.UserId;//Petar change the name of this property!! 
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }

}
