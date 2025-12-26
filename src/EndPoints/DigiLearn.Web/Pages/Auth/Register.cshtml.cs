using System.ComponentModel.DataAnnotations;
using Common.Application;
using Tony.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Services;

namespace Tony.Web.Pages.Auth;


[BindProperties]
public class RegisterModel : BaseRazor
{
    private readonly IUserFacade _userFacade;

    public RegisterModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }


    [Display(Name = "telephone numbers")]
    [Required(ErrorMessage = "{0} Enter")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} Enter")]
    [MinLength(5, ErrorMessage = "Password باید بیشتر از 5 کاراکتر باشد")]
    public string Password { get; set; }

    [Display(Name = "تکرار Password")]
    [Required(ErrorMessage = "{0} Enter")]
    [Compare("Password", ErrorMessage = "Password صحیح نمی باشد")]
    public string ConfirmPassword { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userFacade.RegisterUser(new RegisterUserCommand()
        {
            PhoneNumber = PhoneNumber,
            Password = Password
        });
        if (result.Status == OperationResultStatus.Success)
        {
            result.Message = "register name با موفقیت انجام شد";
        }
        return RedirectAndShowAlert(result, RedirectToPage("Login"));
    }
}