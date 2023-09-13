using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PineconeEmbeddings.Controllers
{
    [EnableCors("corsapp")]
    [Route("api/[controller]")]
    [ApiController]
    public class PineconeController : ControllerBase
    {
        private readonly PineconeService _service;
        public PineconeController()
        {
            _service = new PineconeService();
        }

        [HttpPost]
        public ActionResult<string> PostQuestion(List<double> list)
        {
            var text = _service.FindTheSimilarText(list);
            return text;
        }
    }
}
