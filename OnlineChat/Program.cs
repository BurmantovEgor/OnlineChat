using Microsoft.AspNetCore.Builder;
using OnlineChat.Core.Abstactions;
using OnlineChat.Core.Services;
using OnlineChat.Data.Abstractions;
using Microsoft.OpenApi.Models;
using OnlineChat.Data.Repository;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MessageMapper));

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddSingleton(sp =>
    builder.Configuration.GetConnectionString("DefaultConnection")
    );
    
builder.Services.AddDbContext<OnlineChatDBContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));
    
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Chat API", Version = "v1" });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OnlineChatDBContext>();
    dbContext.Database.Migrate();
}
app.UseExceptionHandler("/Error");
app.UseHsts();

app.MapHub<MessageHub>("/messageHub");

app.MapControllers();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Chat API v1");
    c.RoutePrefix = "swagger"; 
});

app.UseMiddleware<LogService>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
