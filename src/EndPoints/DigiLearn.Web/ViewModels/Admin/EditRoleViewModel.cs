using System.ComponentModel.DataAnnotations;
using UserModule.Data.Entities._Enums;

namespace Tony.Web.ViewModels.Admin;

public class EditRoleViewModel
{
    public Guid RoleId { get; set; }
    [Display(Name = "Title")]
    [Required(ErrorMessage = "{0} Enter")]
    public string Name { get; set; }
    public List<Permissions> SelectedPermissions { get; set; } = new();

}