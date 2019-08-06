using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Libraries.Training.InventorySystem;

namespace InventoryServicesFuncv2
{
    public static class InventoryServices
    {
        private const string INVALID_CONNECTION_STRING = "Invalid Connection String Specified!";

        [FunctionName("InventoryServices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Inventory Service Started ... {DateTime.Now.ToString()}");

            var encodedConnectionString = Environment.GetEnvironmentVariable("ProductsDbConnectionString");
            var connectionString = Encoding.ASCII.GetString(Convert.FromBase64String(encodedConnectionString));

            if (string.IsNullOrEmpty(connectionString))
                return new BadRequestObjectResult(INVALID_CONNECTION_STRING);

            try
            {
                var productService = new ProductService(connectionString);
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                var productId = (int)data?.productId;
                var filteredProduct = productService.GetProductDetails(productId);

                if (filteredProduct == default(Product))
                    return new NotFoundResult();

                return new OkObjectResult(filteredProduct);
            }
            catch (Exception exceptionObject)
            {
                log.LogError(exceptionObject.Message);

                return new BadRequestObjectResult(exceptionObject);
            }
        }
    }
}
