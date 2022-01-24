// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class Menu
    {
        public int MenuId { get; set; }
        public int? IdParentMenu { get; set; }
        public int? LevelMenu { get; set; }
        public int? ModuleId { get; set; }
        public int? Order { get; set; }
        public string MenuName { get; set; }
        public string Icon { get; set; }

        public virtual Module Module { get; set; }
    }
}
