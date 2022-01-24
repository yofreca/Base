using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class User
    {
        public User()
        {
            Login = new HashSet<Login>();
            UserRol = new HashSet<UserRol>();
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Charge { get; set; }

        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<UserRol> UserRol { get; set; }
    }
}
