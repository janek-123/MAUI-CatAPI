using CatAPI.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CatAPI.Services;

public class CatApiService : ICatApiService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "";
    private const string BaseUrl = "https://api.thecatapi.com/v1";

    public CatApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
    }

    public async Task<List<Cat>> GetCatsAsync(int limit = 10)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<CatApiResponse>>($"{BaseUrl}/breeds?limit={limit}");

            return response.ConvertAll(cat => new Cat
            {
                Id = cat.id,
                Url = cat.image.url,
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching cats: {ex.Message}");
            return new List<Cat>();
        }
    }

    public async Task<Cat> GetCatByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<CatApiResponse>($"{BaseUrl}/images/{id}");

            return new Cat
            {
                Id = response.id,
                Url = response.image.url,
                Breeds = response.breeds?.Count > 0 ? response.breeds[0].name : "Unknown"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching cat by id: {ex.Message}");
            return null;
        }
    }

    private class CatApiResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public Image image { get; set; }
        public List<BreedInfo> breeds { get; set; }
    }

    private class Image
    {
        public string id { get; set; }
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    private class BreedInfo
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
