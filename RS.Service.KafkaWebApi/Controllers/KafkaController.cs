using Microsoft.AspNetCore.Mvc;
using RS.Service.Core;

namespace RS.Service.KafkaWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KafkaController : ControllerBase
{
    private readonly KafkaProducerService _producerService;

    public KafkaController(KafkaProducerService producerService)
    {
        _producerService = producerService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string message)
    {
        await _producerService.SendMessageAsync(message);
        return Ok();
    }
    [HttpGet("Consume")]
    public ActionResult<string> Consume(CancellationToken cancellationToken)
    {
        var data = new KafkaConsumerService();

        return Ok(data.ConsumeMessages2("test-topic", cancellationToken));
    }
}
