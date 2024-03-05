using System.Collections.Generic;
using System.Threading.Tasks;
using BaSaltWellnesApp.Models;

namespace BaSaltWellnesApp.Services
{
    public interface IApiService
    {
        //The interface declares two methods - GetNutritionData and GetNewsArticles.
        // Both methods returns a 'Task<List<T>>' where 'T' represents the type of data returned by the API.
        // Each method accepts a 'string query' parameter, which may be used to specify search criteria or parameters for the API request.

        Task<List<NutritionData>> GetNutritionDataAsync(string query);
        Task<List<NewsAtricle>> GetNewsArticlesAsync(string query);

    }
}
