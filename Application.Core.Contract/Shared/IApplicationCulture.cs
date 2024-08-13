using System.Globalization;

namespace Application.Core.Contract.Shared;

public interface IApplicationCulture
{
    CultureInfo GetCurrentCulture();
    string GetCultureName();
    void SetCurrentCulture(string currentCulture);
}
