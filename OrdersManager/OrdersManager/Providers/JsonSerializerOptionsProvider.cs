using System.Text.Json;

namespace OrdersManager.Providers
{
    internal static class JsonSerializerOptionsProvider
    {
        public static JsonSerializerOptions GetOptionsObject()
        {
            return new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
        }
    }
}