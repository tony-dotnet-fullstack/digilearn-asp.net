using System.ComponentModel.DataAnnotations;
using Tony.Web.Infrastructure;
using Tony.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketModule.Core.DTOs.Tickets;
using TicketModule.Core.Services;
using UserModule.Core.Services;

namespace Tony.Web.Pages.Profile.Tickets
{
    [BindProperties]
    public class AddModel : BaseRazor
    {
        private IUserFacade _userFacade;
        private ITicketService _ticketService;

        public AddModel(IUserFacade userFacade, ITicketService ticketService)
        {
            _userFacade = userFacade;
            _ticketService = ticketService;
        }

        [Display(Name = "Title تیکت")]
        [Required(ErrorMessage = "{0} Enter")]
        public string Title { get; set; }

        [Display(Name = "texts تیکت")]
        [Required(ErrorMessage = "{0} Enter")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            var command = new CreateTicketCommand()
            {
                PhoneNumber = user.PhoneNumber,
                OwnerFullName = $"{user.Name} {user.Family}",
                Text = Text,
                Title = Title,
                UserId = user.Id
            };
            var result = await _ticketService.CreateTicket(command);

            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
