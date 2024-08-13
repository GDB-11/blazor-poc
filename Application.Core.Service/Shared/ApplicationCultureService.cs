using Application.Core.Contract.Shared;
using Application.Core.Models.BlazorDTO.Shared;
using System.Globalization;
using System.Text.Json;

namespace Application.Core.Service.Shared;

public sealed class ApplicationCultureService : IApplicationCulture
{
    private CultureInfo _currentCulture;
    private string _cultureName;

    public ApplicationCultureService(string culture)
    {
        _currentCulture = new(culture);
        _cultureName = culture;

        CultureInfo.DefaultThreadCurrentCulture = _currentCulture;
        CultureInfo.DefaultThreadCurrentUICulture = _currentCulture;
    }

    public CultureInfo GetCurrentCulture() => _currentCulture;
    public string GetCultureName() => _cultureName;

    public void SetCurrentCulture(string currentCulture)
    {
        _currentCulture = new(currentCulture);
        _cultureName = currentCulture;

        CultureInfo.DefaultThreadCurrentCulture = _currentCulture;
        CultureInfo.DefaultThreadCurrentUICulture = _currentCulture;

        CultureData cultureData = new() { Culture = currentCulture };
        string cultureJson = JsonSerializer.Serialize(cultureData);
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(baseDirectory, "culture.json");
        File.WriteAllText(filePath, cultureJson);
    }
}
