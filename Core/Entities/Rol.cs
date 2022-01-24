using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class Rol
    {
        public Rol()
        {
            RolPermits = new HashSet<RolPermits>();
            UserRol = new HashSet<UserRol>();
        }

        public int RolId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<RolPermits> RolPermits { get; set; }
        public virtual ICollection<UserRol> UserRol { get; set; }
    }
}
