using AhmadHRMSBackend.Services.Performances;
using AhmadHRMSBackend.Services.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        public readonly ReportsService _service;

        public ReportsController(ReportsService service)
        {
            _service = service;
        }


    }
}
