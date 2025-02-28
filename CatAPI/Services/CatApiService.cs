using CatAPI.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CatAPI.Services;

public class CatApiService : ICatApiService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "live_Auptc9dXjMBH5TtMfYLu19fFE10gORR0xifbIsnjGYvScYggeJWs2cWacJMSGVTa";
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
            var response = await _httpClient.GetFromJsonAsync<CatApiResponse>($"{BaseUrl}/breeds/{id}");

            Trace.WriteLine($"FETCH: {BaseUrl}/breeds/{id}");
            Trace.WriteLine($"FETCH1: {BaseUrl}/images/search?{id}");
            Trace.WriteLine($"RESPONSE: {response}");

            var response1 = await _httpClient.GetFromJsonAsync<List<Image>>($"{BaseUrl}/images/search?{id}");

            return new Cat
            {
                Id = response.id,
                Url = response1[0].url,
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

        public string reference_image_id { get; set; }

        public override string ToString()
        {
            return $"id: {id}\n" +
                $"name: {name}\n" +
                $"image: {image}\n" +
                $"breeds: {breeds}\n" +
                $"reference_image_id: {reference_image_id}\n";
        }
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
