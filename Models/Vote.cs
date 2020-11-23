using System;
using System.ComponentModel.DataAnnotations;

namespace ForumDemo.Models
{
    public class Vote
    {
        [Key] // primary key
        public int VoteId { get; set; }
        public bool IsUpvote { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // FKs (Foreign Keys), these prop names MUST match the primary key names of these models
        public int PostId { get; set; }
        public int UserId { get; set; }

        // Navigation Properties
        public User Voter { get; set; }
        public Post Post { get; set; }
    }
}
