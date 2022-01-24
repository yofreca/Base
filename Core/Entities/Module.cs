using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class Module
    {
        public Module()
        {
            Menu = new HashSet<Menu>();
            RolPermits = new HashSet<RolPermits>();
        }

        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }
        public virtual ICollection<RolPermits> RolPermits { get; set; }
    }
}
