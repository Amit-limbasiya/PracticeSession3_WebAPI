using System.ComponentModel.DataAnnotations;

namespace PracticeSession3.Models
{
    public class UserProfile
    {
        public int id { get; set; }
        [MaxLength(50)]
        [Required]
        public string name { get; set; }=string.Empty;
        [Required]
        [MaxLength(50)]
        public string email { get; set; }=string.Empty;
        [Required]
        [StringLength(10)]
        public string PhoneNumber{ get; set; }=string.Empty;
        [Required]
        public int age { get; set; } = 18;
    }
}
