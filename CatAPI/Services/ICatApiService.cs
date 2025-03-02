using CatAPI.Models;

namespace CatAPI.Services;
public interface ICatApiService
{
    Task<List<Cat>> GetCatsAsync(int limit);
    Task<Cat> GetCatByIdAsync(string id);
}
