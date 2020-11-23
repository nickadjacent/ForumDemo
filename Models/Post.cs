using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumDemo.Models
{
    public class Post
    {
        [Key] // PostId is the primary key
        public int PostId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 charactes.")]
        [MaxLength(45, ErrorMessage = "Must be less than 45 charactes.")]
        public string Topic { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 charactes.")]
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties for 1 User to Many Posts relationship
        public int UserId { get; set; } // this FK must match PK prop name

        // 1 user related to each Post, this prop can be named anything
        // Because it is of type user, and the FK is for a User, entity framework automatically can get all the User info
        public User Author { get; set; }
        public List<Vote> Votes { get; set; } // Many to Many between User & Post: 1 Post can have Many votes

        // if you add your own constructor then you need to also add a parameterless constructor
        public Post() { }
    }
}
