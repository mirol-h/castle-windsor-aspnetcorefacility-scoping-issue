namespace CastleWindsorAspNetCoreFacilityRepro.Services
{
	public interface IFacade
	{
		string GetInfo();
	}

	public class Facade : IFacade
	{
		private readonly IService service;

		public Facade(IService service)
		{
			this.service = service;
		}

		public string GetInfo()
		{
			return $"[Facade]{GetHashCode()}, Service: {service.GetHashCode()}";
		}
	}
}