
namespace CatAPI.Models;
public class Cat
{
    public string Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public string Temperament { get; set; }
    public string Origin { get; set; }

    public string Url { get; set; }

    public int Width { get; set; }
    public int Height { get; set; }

    public Cat(string id, string url)
    {
        Id = id;
        Url = url;
    }
}
