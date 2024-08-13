using Application.Core.Contract.Shared;
using System.Globalization;
using System.Resources;

namespace Application.Core.Service.Shared;

public sealed class MessageHandlingService : IMessageHandling
{
    private readonly IApplicationCulture _applicationCulture;
    private readonly ResourceManager _resourceManager;

    public MessageHandlingService(string baseName, IApplicationCulture applicationCulture)
    {
        _applicationCulture = applicationCulture;
        _resourceManager = new ResourceManager($"Application.Core.Service.Shared.MessageResources.{baseName}", typeof(MessageHandlingService).Assembly);
    }

    public string GetMessage(string code, string language)
    {
        CultureInfo culture = new(language);

        return _resourceManager.GetString(code, culture) ?? string.Empty;
    }

    public string GetMessage(string? code, CultureInfo? culture)
    {
        culture ??= _applicationCulture.GetCurrentCulture();

        if (string.IsNullOrEmpty(code))
        {
            return string.Empty;
        }

        return _resourceManager.GetString(code, culture) ?? code;
    }

    public string GetMessage(string code)
    {
        return _resourceManager.GetString(code, _applicationCulture.GetCurrentCulture()) ?? string.Empty;
    }
}
