namespace StackOverflow.Models
{
    public class Comment
    {
        public int Id { get; set; } // Foreign key
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User CreatedBy { get; set; }
        public int Likes { get; set; }

        //public override bool Equals(object obj)
        //{
        //    Comment other = obj as Comment;
        //    if (obj != null)
        //    {
        //        return this.Id == other.Id
        //                && this.Content == other.Content;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

    }
}
