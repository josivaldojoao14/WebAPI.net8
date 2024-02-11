using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet8_api.Dtos.Stock
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(100, ErrorMessage ="Title cannot be over 100 characters" )]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(100, ErrorMessage ="Content cannot be over 100 characters" )]
        public string Content { get; set; } = string.Empty;
    }
}