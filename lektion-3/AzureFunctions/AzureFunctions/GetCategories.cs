using System.Net;
using AzureFunctions.Contexts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AzureFunctions
{
    internal class GetCategories
    {
        private readonly ILogger _logger;
        private readonly DataContext _context;

        public GetCategories(ILoggerFactory loggerFactory, DataContext context)
        {
            _logger = loggerFactory.CreateLogger<GetCategories>();
            _context = context;
        }

        [Function("GetCategories")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "categories")] HttpRequestData req)
        {
            var items = await _context.Categories.ToListAsync();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(items);
            return response;
        }
    }
}
