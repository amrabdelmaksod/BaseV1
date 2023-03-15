using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
   
    public class JsonController : ControllerBase
    {
        private readonly ILogger<JsonController> _logger;

        public JsonController(ILogger<JsonController> logger)
        {
            _logger = logger;
        }

        [HttpPost("EnglishToArabic")]
        public IActionResult EnglishToArabic([FromBody] Dictionary<string, string> data)
        {
            try
            {
                // Read the contents of the JSON file
                string filePath = "Resources/ar-EG.json";
                string jsonString = System.IO.File.ReadAllText(filePath);

                // Deserialize the JSON string into a JObject
                JObject json = JObject.Parse(jsonString);

                // Modify the JObject based on the request data
                foreach (KeyValuePair<string, string> entry in data)
                {
                    if (!json.ContainsKey(entry.Key))
                    {
                        json[entry.Key] = entry.Value;
                    }
                }

                // Write the modified JObject back to the JSON file
                jsonString = json.ToString();
                System.IO.File.WriteAllText(filePath, jsonString);

                // Return a successful response
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while modifying JSON file");
                return BadRequest("Failed to modify JSON file");
            }
        }


        [HttpPost("EnglishToEnglish")]
        public IActionResult EnglishToEnglish([FromBody] Dictionary<string, string> data)
        {
            try
            {
                // Read the contents of the JSON file
                string filePath = "Resources/en-US.json";
                string jsonString = System.IO.File.ReadAllText(filePath);

                // Deserialize the JSON string into a JObject
                JObject json = JObject.Parse(jsonString);

                // Modify the JObject based on the request data
                foreach (KeyValuePair<string, string> entry in data)
                {
                    if (!json.ContainsKey(entry.Key))
                    {
                        json[entry.Key] = entry.Value;
                    }
                }

                // Write the modified JObject back to the JSON file
                jsonString = json.ToString();
                System.IO.File.WriteAllText(filePath, jsonString);

                // Return a successful response
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while modifying JSON file");
                return BadRequest("Failed to modify JSON file");
            }
        }
    }

}
