using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace CourseProvider_G.Functions;

public class Playground(ILogger<Playground> logger)

{

    private readonly ILogger<Playground> _logger = logger;

    [Function("Playground")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "graphql")] HttpRequestData req)
    {
        var response = req.CreateResponse();
        response.Headers.Add("Content-Type", "text/html; charset=utf-8");
        await response.WriteStringAsync(PlaygroundPage());
        return response;
    }

    private string PlaygroundPage()
    {
        return @"
            <!DOCTYPE html>
                <html>
                    <head>
                        <title>GraphQL Playground</title>
                        <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/graphql-playground-react/build/static/css/index.css"" />
                        <link rel=""shortcut icon"" href=""https://cdn.jsdelivr.net/npm/graphql-playground-react/build/favicon.png"" />
                        <script src=""https://cdn.jsdelivr.net/npm/graphql-playground-react/build/static/js/middleware.js""></script>
                    </head>
                    <body>
                        <div id=""root""></div>
                        <script>
                            window.addEventListener('load', function (event) {
                                GraphQLPlayground.init(document.getElementById('root'), {
                                    endpoint: '/api/graphql'
                                })
                            })
                        </script>
                    </body>
            </html>";
    }
}
