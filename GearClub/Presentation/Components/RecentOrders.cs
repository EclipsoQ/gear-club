using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GearClub.Presentation.Components
{
    public class RecentOrders : ViewComponent
    {
        private readonly IOrderService orderService;
        public RecentOrders(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IViewComponentResult Invoke()
        {
            var orders = orderService.GetAllOrders().Take(5);
            return View(orders);
        }
    }
}
