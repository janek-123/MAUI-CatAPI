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

    public async Task<List<Cat>> GetCatsAsync(int limit = 20)
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
            var imageRes = await _httpClient.GetFromJsonAsync<List<Image>>($"{BaseUrl}/images/search?{id}&limit=1");

            if (breedRes == null)
                return new Cat("Not found", "Not found", "/");

            if (imageRes != null && imageRes.Count > 0)
                breedRes.image = imageRes[0];

            return breedRes.ToCat();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching cat by id: {ex.Message}");
            return new Cat("Not found", "Not found", "/");
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
        public int? indoor { get; set; }
        public int? adaptability { get; set; }
        public int? intelligence { get; set; }

        // urls
        public string? cfa_url { get; set; }
        public string? vetstreet_url { get; set; }
        public string? vcahospitals_url { get; set; }

        public override string ToString()
        {
            return $"id: {id}, name: {name}, image: ({image}), reference_image_id: {reference_image_id}";
        }

        public Cat ToCat()
        {
            return new Cat(id ?? "", name ?? "Unknown", image?.url ?? "")
            {
                Description = description ?? "Unknown",
                Temperament = temperament ?? "",
                Origin = origin ?? "",
                LifeSpan = life_span ?? "",
                Indoor = indoor != null ? $"{indoor}/5" : "",
                Adaptability = adaptability != null ? $"{adaptability}/5" : "",
                Intelligence = intelligence != null ? $"{intelligence}/5" : "",
                CfaUrl = cfa_url ?? "",
                VetstreetUrl = vetstreet_url ?? "",
                VcahospitalsUrl = vcahospitals_url ?? ""
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
