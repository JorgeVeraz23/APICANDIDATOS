namespace LinkQuality.Api.Middlewares
{

    public static class LoginResponseHTTPMiddlewareExtension
    {
        public static IApplicationBuilder UseLoginResponseHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoginResponseHTTPMiddleware>();
        }
    }

    public class LoginResponseHTTPMiddleware
    {
        private readonly RequestDelegate siguiente;

        public LoginResponseHTTPMiddleware(RequestDelegate siguiente, ILogger<LoginResponseHTTPMiddleware> logger)
        {
            this.siguiente = siguiente;
        }

        //Para iniciar debe ser Invoke o InvokeAsync

        public async Task InvokeAsync(HttpContext contexto)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpoOriginalRespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;

                await siguiente(contexto);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpoOriginalRespuesta);
                contexto.Response.Body = cuerpoOriginalRespuesta;

            }
        }
    }
}
