using System.ComponentModel.DataAnnotations;
using Microsoft.JSInterop.Infrastructure;

namespace News_Website.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; } // This attribute marks the property as the primary key

        [MaxLength(10)]
        public string? Phone { get; set; }

        [MaxLength(50)]
        public string ?Email { get; set; }

        [MaxLength(150)]
        public string? Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<News> ?NewsList { get; set; }

        public List<UserRole>? UserRoles { get; set; }

        public string? Name { get; set; } // Make Name nullable
        // Add other properties as needed
    }
}