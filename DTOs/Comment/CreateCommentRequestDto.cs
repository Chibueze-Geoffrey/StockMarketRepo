using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs.Comment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "Title must be 5 characters and above")]
        [MaxLength(250,ErrorMessage = "Title must not exceed 250")]
        public string Title { get; set; } = string.Empty;

         [Required]
        [MinLength(5,ErrorMessage = "Content must be 5 characters and above")]
        [MaxLength(550,ErrorMessage = "Content must not exceed 250")]
        public string Content { get; set; } = string.Empty;
        
    }
}