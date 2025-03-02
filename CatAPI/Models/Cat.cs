
namespace CatAPI.Models;
public class Cat
{
    public string Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; } = "";

    public string Temperament { get; set; } = "";
    public string Origin { get; set; } = "";

    public string Url { get; set; }

    public string LifeSpan { get; set; } = "";
    public string Indoor { get; set; } = "";
    public string Adaptability { get; set; } = "";
    public string Intelligence { get; set; } = "";

    public string CfaUrl { get; set; } = "";
    public string VetstreetUrl { get; set; } = "";
    public string VcahospitalsUrl { get; set; } = "";

    public Cat(string id, string name, string url)
    {
        Id = id;
        Name = name;
        Url = url;
    }
}
