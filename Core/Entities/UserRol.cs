// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class UserRol
    {
        public int RolId { get; set; }
        public long UserId { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual User User { get; set; }
    }
}
