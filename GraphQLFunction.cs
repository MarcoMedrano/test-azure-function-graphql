using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace test_azure_function_graphql;

public class GraphQLFunction
{
    [FunctionName("GraphQLHttpFunction")]
    public Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "graphql/{**slug}")]
        HttpRequest request,
        [GraphQL]
        IGraphQLRequestExecutor executor, ILogger log)
    {
        log.LogInformation("request: \n" + string.Join(',', request.Headers.Select(h => $"{h.Key}={h.Value}")));

        return executor.ExecuteAsync(request);
    }
}