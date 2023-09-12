using MiddlewareExample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

#region MiddlewareExample
/*
// middleware 1
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("From middleware 1\n");
    await next(context); // <== calls middleware 2
});

// middleware 2
//app.UseMiddleware<MyCustomMiddleware>();
//app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

// middleware 3
app.Run(async (context) =>
{
    await context.Response.WriteAsync("From middleware 3\n");
    // return to middleware 2
});
*/
#endregion

#region UseWhenExample

app.UseWhen(context => context.Request.Query.ContainsKey("username"),
    app =>
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello from Middleware branch\n");
            await next();
        });
    });

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from middleware at main chain\n");
});
#endregion

app.Run();

