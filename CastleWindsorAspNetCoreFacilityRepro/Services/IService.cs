namespace CastleWindsorAspNetCoreFacilityRepro.Services
{
	public interface IService
	{
		string GetInfo();
	}

	public class Service : IService
	{
		public string GetInfo()
		{
			return $"Service: {GetHashCode()}";
		}
	}
}