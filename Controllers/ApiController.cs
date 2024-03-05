

using Microsoft.AspNetCore.Mvc;
using BaSaltWellnesApp.Services;

namespace BaSaltWellnesApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        //The API class defines two GET endpoints:
        // - /api/nutrition;
        // - /api/news;
        //Error handling is implemented to catch any exceptions that occur during API calls and return an appropriate error response.

        public readonly IApiService _apiService;

        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }

        //The GetNutritionData method calls the GetNutritionDataAsync method of the IApiService to retrieve nutrition data
        [HttpGet("nutrition")]
        public async Task<IActionResult> GetNutritionData(string query)
        {
            try
            {
                var nutritionData = await _apiService.GetNutritionDataAsync(query);
                return Ok(nutritionData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //The GetNewsArticles method calls the GetNewsArticlesAsync method of the IApiService to retrieve news articles.
        [HttpGet("news")]
        public async Task<IActionResult> GetNewsArticles(string query)
        {
            try
            {
                var newsArticles = await _apiService.GetNewsArticlesAsync(query);
                return Ok(newsArticles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
