
//using System.Text;

//namespace Crud_with_pagination.Middlewares
//{
//    public class AuthMiddleware : IMiddleware
//    {
//        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//        {
//            if (!context.Request.Headers.ContainsKey("Authorization"))
//            {

//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync("Unauthorized");
//                return;
//            }

//            var headers = context.Request.Headers["Authorization"].ToString();
//            var creds = headers.Substring(6);
//            var encodedString = Encoding.UTF8.GetString(Convert.FromBase64String(creds));
            
//            string[] credentialString = encodedString.Split(':');

//            string username = credentialString[0];
//            string password = credentialString[1];

//            if (username != "zeeshan01" && password != "zeeshan@123")
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync("Unauthorized");
//                return;

//            }
//            await next(context);

//        }


//    }
//}
