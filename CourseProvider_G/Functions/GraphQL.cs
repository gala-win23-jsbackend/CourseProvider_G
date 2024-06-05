using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CourseProvider_G.Functions;

public class GraphQL(ILogger<GraphQL> logger, IGraphQLRequestExecutor graphQLRequestExecutor)
{
    private readonly ILogger<GraphQL> _logger = logger;
    private readonly IGraphQLRequestExecutor _graphQLRequestExecutor = graphQLRequestExecutor;
    
    
    [Function("GraphQL")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "graphql")] HttpRequest req)
    {
        try
        {
            return await _graphQLRequestExecutor.ExecuteAsync(req);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing GraphQL request");
            return new BadRequestObjectResult(ex.Message);
        }
    }
}
