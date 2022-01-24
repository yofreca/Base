// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class RolPermits
    {
        public int ModuleId { get; set; }
        public int RolId { get; set; }
        public int? PermitId { get; set; }

        public virtual Module Module { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
