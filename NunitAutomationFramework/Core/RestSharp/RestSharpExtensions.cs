using System.Linq;
using RestSharp;

namespace NunitAutomationFramework.Core.RestSharp
{
    public static class RestSharpExtensions
    {
        public static string GetBody(this RestRequest request)
        {
            var bodyParameter = request.Parameters
                .FirstOrDefault(p => p.Type == ParameterType.RequestBody);

            return bodyParameter == null
                ? null
                : bodyParameter.Value.ToString();
        }

    }
}