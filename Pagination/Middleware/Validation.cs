namespace Pagination.Middleware
{
    public class Validation : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
               
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Problem is: {ex.Message}");
            }
        }
    }
}
