using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Contact.Ticket;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Controllers;
using ServiceHost.PresentationExtensions;

namespace ServiceHost.Areas.User.Controllers
{
    public class TicketController : UserBaseController
    {
        #region Constructor

        private readonly IContactService _contactService;

        public TicketController(IContactService contactService)
        {
            _contactService = contactService;
        }

        #endregion

        #region Actions

        #region Tickets List

        [HttpGet("user-tickets-list")]
        public async Task<IActionResult> TicketsList(FilterTicketDto ticketList)
        {
            ticketList.UserId = User.GetUserId();
            ticketList.OrderBy = FilterTicketOrder.CreateDateDescending;

            var tickets = await _contactService.TicketsList(ticketList);
            return View(tickets);
        }

        #endregion

        #region Add Ticket

        [HttpGet("add-ticket")]
        public IActionResult AddUserTicket()
        {
            return View();
        }

        [HttpPost("add-ticket"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserTicket(AddTicketDto ticket)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.AddUserTicket(ticket, User.GetUserId());

                switch (result)
                {
                    case AddTicketResult.Error:
                        TempData[ErrorMessage] = "در فرایند ثبت تیکت خطایی رخ داد، لطفا بعدا تلاش کنید";
                        return RedirectToAction("TicketsList", "Ticket", new { area = "User"});
                    case AddTicketResult.EmptyText:
                        TempData[ErrorMessage] = "لطفا متن تیکت را وارد کنید";
                        break;
                    case AddTicketResult.Success:
                        TempData[SuccessMessage] = "تیکت شما با موفقیت ثبت شد.";
                        TempData[InfoMessage] = "همکاران ما به زودی تیکت شما را بررسی خواهند کرد.";
                        return RedirectToAction("TicketsList", "Ticket", new { area = "User" });
                        
                }
            }

            return View(ticket);
        }

        #endregion

        #endregion
    }
}
