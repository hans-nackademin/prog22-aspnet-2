using System.Net;
using AzureFunctions.Models.Schemas;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions.Methods;

public class CreateProduct
{
    #region constructors and private fields

    private readonly ILogger _logger;

    public CreateProduct(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CreateProduct>();
    }

    #endregion


    [Function("CreateProduct")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequestData req)
    {
        try
        {
            var body = JsonConvert.DeserializeObject<ProductSchema>(new StreamReader(req.Body).ReadToEnd());
            if (body == null)
            {
                _logger.LogInformation("Provided body is missing required data.");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            using var conn = new SqlConnection(Environment.GetEnvironmentVariable("Sql"));
            
            var articleNumber = await conn.QueryFirstOrDefaultAsync<string>("SELECT ArticleNumber FROM Products WHERE ArticleNumber = @ArticleNumber", body);
            if (!string.IsNullOrEmpty(articleNumber))
            {
                _logger.LogInformation($"ERROR::Product with Article Number {articleNumber} already exists.");
                return req.CreateResponse(HttpStatusCode.Conflict);
            }
                

            await conn.ExecuteAsync("INSERT INTO Products VALUES (@ArticleNumber, @Name)", body);

            _logger.LogInformation("Product was saved to the database table.");
            return req.CreateResponse(HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"ERROR::{ex.Message}");
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }

    }
}
