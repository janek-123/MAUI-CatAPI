using CatAPI.Models;
using System.Net.Http.Json;

namespace CatAPI.Services;
public class CatApiService : ICatApiService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "live_Auptc9dXjMBH5TtMfYLu19fFE10gORR0xifbIsnjGYvScYggeJWs2cWacJMSGVTa"; // propably shouldn't have exposed this
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
            var breedsRes = await _httpClient.GetFromJsonAsync<List<CatApiResponse>>($"{BaseUrl}/breeds?limit={limit}");
            return breedsRes?.ConvertAll(cat => cat.ToCat()) ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching cats: {ex.Message}");
            return new ();
        }
    }

    public async Task<Cat> GetCatByIdAsync(string id)
    {
        try
        {
            var breedRes = await _httpClient.GetFromJsonAsync<CatApiResponse>($"{BaseUrl}/breeds/{id}");
            var imageRes = await _httpClient.GetFromJsonAsync<List<Image>>($"{BaseUrl}/images/search?{id}");

            breedRes.image = imageRes[0];

            return breedRes.ToCat();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching cat by id: {ex.Message}");
            return null;
        }
    }

    private class CatApiResponse
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

        public string? temperament { get; set; }
        public string? origin { get; set; }

        public Image? image { get; set; }

        public string? reference_image_id { get; set; }

        public string? life_span { get; set; }

        public override string ToString()
        {
            return $"id: {id}, name: {name}, image: ({image}), reference_image_id: {reference_image_id}";
        }

        public Cat ToCat()
        {
            return new Cat(id ?? "", image?.url ?? "")
            {
                Name = name ?? "Unknown",
                Description = description ?? "Unknown",
                Temperament = temperament ?? "",
                Origin = origin ?? ""
            };
        }
    }

    private class Image
    {
        public string? id { get; set; }
        public string? url { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }

        public override string ToString()
        {
            return $"id: {id}, url: {url}, width: {width}, height: {height}";
        }
    }
}
