using CatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.Services;

public interface ICatApiService
{
    Task<List<Cat>> GetCatsAsync(int limit = 10);
    Task<Cat> GetCatByIdAsync(string id);
}
