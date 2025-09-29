using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using NewsSite.Attributes;

namespace NewsSite.Models
{
    public class News
    {
        public int Id { get; set; }

        [LocalizedRequired("Title")]
        [LocalizedStringLength(200, "Title")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public string? ImageFileName { get; set; }

        [LocalizedRequired("Subtitle")]
        [LocalizedStringLength(500, "Subtitle")]
        [Display(Name = "Subtitle")]
        public string Subtitle { get; set; }


        [LocalizedRequired("Content")]
        [LocalizedMinLength(50, "Content")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [NotMapped]
        [LocalizedRequired("NewsImage")]
        [Display(Name = "NewsImage")]
        public IFormFile ImageFile { get; set; }
    }
}
