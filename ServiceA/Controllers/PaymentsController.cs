using Microsoft.AspNetCore.Mvc;
using ServiceA.DTOs;
using ServiceA.Services;

namespace ServiceA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentsController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntention([FromBody] PaymentIntentionDto dto)
        {
            var uuid = await _paymentService.CreatePaymentIntention(dto);
            return Ok(new { Uuid = uuid });
        }
    }
}
