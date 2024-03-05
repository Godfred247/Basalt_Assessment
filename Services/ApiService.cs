

using Newtonsoft.Json;
using RestSharp;
using BaSaltWellnesApp.Models;
using Microsoft.Extensions.Configuration;

namespace BaSaltWellnesApp.Services
{
    public class ApiService : IApiService
    {
        private readonly RestClient _nutritionixClient;
        private readonly RestClient _newsClient;
        private readonly string _nutritionixApiKey;
        private readonly string _newsApiKey;

        public ApiService(IConfiguration configuration)
        {
            //Retieving API key from configuration
            _newsApiKey = configuration["NewsApiKey"];
            _nutritionixApiKey = configuration["NutritionixApiKey"];

            // Initialize RestClient for Nutritionix API with base URL
            _nutritionixClient = new RestClient("https://api.nutritionix.com/v1_1/");

            // Initialize RestClient for News API with base URL
            _newsClient = new RestClient("https://newsapi.org/v2/");
        }

        public async Task<List<NutritionData>> GetNutritionDataAsync(string query)
        {
            var request = new RestRequest("search/instant", Method.Get);
            request.AddParameter("query", query);
            request.AddParameter("detailed", "true");
            request.AddParameter("branded", "false");
            request.AddHeader("x-app-id", "217eaeac");
            request.AddHeader("x-app-key", _nutritionixApiKey);

            var response = await _nutritionixClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new Exception("Failed to retrieve nutrition data from Nutritionix API");

            var responseData = JsonConvert.DeserializeObject<List<NutritionData>>(response.Content);

            return responseData;
        }

        public async Task<List<NewsAtricle>> GetNewsArticlesAsync(string query)
        {
            var request = new RestRequest("everything", Method.Get);
            request.AddParameter("q", query);
            request.AddParameter("apiKey", _newsApiKey);

            var response = await _newsClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new Exception("Failed to retrieve news articles from News API");

            var allNewsArticles = JsonConvert.DeserializeObject<List<NewsAtricle>>(response.Content);

            // Filter news articles related to nutrition or health
            var filteredNewsArticles = allNewsArticles.Where(article =>
                article.Description.Contains("nutrition", StringComparison.OrdinalIgnoreCase) ||
                article.Description.Contains("health", StringComparison.OrdinalIgnoreCase) ||
                article.Name.Contains("nutrition", StringComparison.OrdinalIgnoreCase) ||
                article.Name.Contains("health", StringComparison.OrdinalIgnoreCase) ||
                article.Url.Contains("nutrition", StringComparison.OrdinalIgnoreCase) ||
                article.Url.Contains("health", StringComparison.OrdinalIgnoreCase) ||
                article.Category.Contains("nutrition", StringComparison.OrdinalIgnoreCase) ||
                article.Category.Contains("health", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return filteredNewsArticles;
        }
    }
}
