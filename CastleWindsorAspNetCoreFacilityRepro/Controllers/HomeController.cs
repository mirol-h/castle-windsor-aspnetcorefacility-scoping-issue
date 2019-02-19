using System.Text;
using CastleWindsorAspNetCoreFacilityRepro.Services;
using Microsoft.AspNetCore.Mvc;

namespace CastleWindsorAspNetCoreFacilityRepro.Controllers
{
	public class HomeController : Controller
	{
		private readonly IService service;
		private readonly IFacade facade;

		public HomeController(IService service, IFacade facade)
		{
			this.service = service;
			this.facade = facade;
		}

		public IActionResult Index()
		{
			var sb = new StringBuilder();
			sb.AppendLine(service.GetInfo());
			sb.AppendLine(facade.GetInfo());
			sb.AppendLine($"Controller: {GetHashCode()}");
			return View("Index", sb.ToString());
		}
	}
}