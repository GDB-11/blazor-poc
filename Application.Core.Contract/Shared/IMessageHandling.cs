using System.Globalization;

namespace Application.Core.Contract.Shared;

public interface IMessageHandling
{
    string GetMessage(string code, string language);
    string GetMessage(string? code, CultureInfo? culture);
    string GetMessage(string code);
}
