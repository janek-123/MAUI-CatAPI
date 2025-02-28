using CatAPI.Models;

namespace CatAPI.Services;
public interface ICatApiService
{
    Task<List<Cat>> GetCatsAsync(int limit = 10);
    Task<Cat> GetCatByIdAsync(string id);
}
