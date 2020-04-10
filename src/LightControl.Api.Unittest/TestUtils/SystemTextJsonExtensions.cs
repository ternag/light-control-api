using System.Text.Json;

namespace LightControl.Api.UnitTest.TestUtils
{
  public static class SystemTextJsonExtensions
  {
    public static string ToFormatedString(this JsonDocument document)
    {
      return JsonSerializer.Serialize(document.RootElement, new JsonSerializerOptions{ WriteIndented = true });
    }
  }
}