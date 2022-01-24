// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class Login
    {
        public long LoginId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }

        public virtual User User { get; set; }
    }
}
