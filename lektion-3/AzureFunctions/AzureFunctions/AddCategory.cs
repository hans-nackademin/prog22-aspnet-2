using System.Net;
using AzureFunctions.Contexts;
using AzureFunctions.Models.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions
{
    internal class AddCategory
    {
        private readonly ILogger _logger;
        private readonly DataContext _context;

        public AddCategory(ILoggerFactory loggerFactory, DataContext context)
        {
            _logger = loggerFactory.CreateLogger<AddCategory>();
            _context = context;
        }

        [Function("AddCategory")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "categories")] HttpRequestData req)
        {
            var categoryEntity = JsonConvert.DeserializeObject<CategoryEntity>(new StreamReader(req.Body).ReadToEnd());
            if (categoryEntity != null) 
            {
                _context.Categories.Add(categoryEntity!);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Category saved to database");

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(categoryEntity);

                return response;
            }

            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
