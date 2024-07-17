﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModel
{
    public class CategoryView
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

    }
}