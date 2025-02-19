namespace WebAppCliente.Helper
{
    public static class QueryStringHelper
    {
        public static string BuildQueryString<T>(string paramName, IEnumerable<T> values)
        {
            if (values == null || !values.Any())
                return string.Empty;

            // Constrói cada par "nome=valor" com a codificação adequada
            var queryParams = values.Select(value =>
                $"{Uri.EscapeDataString(paramName)}={Uri.EscapeDataString(value.ToString())}");

            return "?" + string.Join("&", queryParams);
        }
    }
}
