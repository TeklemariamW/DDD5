using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        public WeatherForecastController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var domesticAccounts = _repository.AccountRep.FindByCondition(x => x.AccountType.Equals("Domestic"));
            var owners = _repository.OwnerRep.FindAll();

            return new string[] { "value-1", "v-2" };
        }
    }
}