using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class BlockedUser
    {
        [Key]
        public string Email { get; set; }
    }
}
