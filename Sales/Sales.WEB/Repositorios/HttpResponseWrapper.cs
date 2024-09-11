namespace Sales.WEB.Repositorios
{
    public class HttpResponseWrapper<T>(T? response, bool error = false, HttpResponseMessage responseMessage = null!)
    {
        public bool Error { get; set; } = error;
        public T? Response { get; set; } = response;
        public HttpResponseMessage HttpResponseMessage { get; set; } = responseMessage;


        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error) return null;

            var statusCode = HttpResponseMessage.StatusCode;

            if (statusCode == System.Net.HttpStatusCode.NotFound)
                return $"Error: {statusCode} => No encontrado";

            if (statusCode == System.Net.HttpStatusCode.BadRequest)
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            
            if (statusCode == System.Net.HttpStatusCode.Unauthorized)
                return $"Error: {statusCode} => Tienes que loguearte para realizar la operación";

            if (statusCode == System.Net.HttpStatusCode.Forbidden)
                return $"Error: {statusCode} => No tienes permiso para realizar la operación";

            var message = await HttpResponseMessage.Content.ReadAsStringAsync();
            return $"Error: {statusCode} => {message}";
        }

    }
}
