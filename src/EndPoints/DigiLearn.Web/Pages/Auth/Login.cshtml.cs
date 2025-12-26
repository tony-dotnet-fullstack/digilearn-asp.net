using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Common.Application.SecurityUtil;
using Tony.Web.Infrastructure.RazorUtils;
using Microsoft.IdentityModel.JsonWebTokens;
using UserModule.Core.Services;
using Tony.Web.Infrastructure.JwtUtil;

namespace Tony.Web.Pages.Auth;


[BindProperties]
public class LoginModel : BaseRazor
{
    private IUserFacade _userFacade;
    private IConfiguration _configuration;
    public LoginModel(IUserFacade userFacade, IConfiguration configuration)
    {
        _userFacade = userFacade;
        _configuration = configuration;
    }

    [Display(Name = "telephone numbers")]
    [Required(ErrorMessage = "{0} Enter")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} Enter")]
    [MinLength(5, ErrorMessage = "Password باید بیشتر از 5 کاراکتر باشد")]
    public string Password { get; set; }

    public bool IsRememberMe { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userFacade.GetUserByPhoneNumber(PhoneNumber);
        if (user == null)
        {
            ErrorAlert("userی با مشخصات وارد شده یافت نشد");
            return Page();
        }
        var isComparePassword = Sha256Hasher.IsCompare(user.Password, Password);
        if (isComparePassword == false)
        {
            ErrorAlert("userی با مشخصات وارد شده یافت نشد");
            return Page();
        }

        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        if (IsRememberMe)
        {
            HttpContext.Response.Cookies.Append("digi-token", token, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(30),
                Secure = true
            });
        }
        else
        {
            HttpContext.Response.Cookies.Append("digi-token", token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true
            });
        }

        return RedirectToPage("../Index");
    }
}