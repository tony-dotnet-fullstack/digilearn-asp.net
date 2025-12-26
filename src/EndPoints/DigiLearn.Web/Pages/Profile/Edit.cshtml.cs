using System.ComponentModel.DataAnnotations;
using Tony.Web.Infrastructure;
using Tony.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Services;

namespace Tony.Web.Pages.Profile
{
    [BindProperties]
    public class EditModel : BaseRazor
    {
        private IUserFacade _userFacade;

        public EditModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [Display(Name = "name")]
        [Required(ErrorMessage = "{0} Enter")]
        public string Name { get; set; }

        [Display(Name = "name خانوادگی")]
        [Required(ErrorMessage = "{0} Enter")]

        public string Family { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} Enter")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public async Task OnGet()
        {
            var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            if (user != null)
            {
                Name = user.Name;
                Family = user.Family;
                Email = user.Email;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _userFacade.EditUserProfile(new EditUserCommand()
            {
                Name = Name,
                Family = Family,
                Email = Email,
                UserId = User.GetUserId()
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
