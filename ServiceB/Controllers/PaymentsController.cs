using Microsoft.AspNetCore.Mvc;
using ServiceB.Repositories;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentRepository _repository;

        public PaymentsController(PaymentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{uuid}")]
        public async Task<IActionResult> GetPaymentStatus(string uuid)
        {
            var payment = await _repository.GetByUuidAsync(uuid);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
    }
}
