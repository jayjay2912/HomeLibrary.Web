using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Gemstone.HomeLibrary.Func;

public class HealthTrigger
{
    private readonly ILogger _logger;

    public HealthTrigger(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HealthTrigger>();
    }

    [Function("HealthTrigger")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("Testing health endpoint");

        return new OkObjectResult("Healthy");
    }
}
