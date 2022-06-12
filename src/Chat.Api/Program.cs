using Chat.Api.Constants;
using Chat.Api.Extensions;
using Chat.Api.Hubs;
using Chat.Api.Models;
using Chat.Db;
using Chat.Db.Extensions;
using Chat.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbServices(postgresConnectionString);
builder.Services.AddApiServices();

builder.Services
    .AddIdentityCore<AppIdentityUser>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredUniqueChars = 1;
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
        options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    })
    .AddRoles<AppIdentityRole>()
    .AddEntityFrameworkStores<ChatDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddMvcCore()
    .AddDataAnnotations()
    .AddApiExplorer()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(context.ModelState);
    });

builder.Services.AddRazorPages();

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseEndpoints(builder =>
{
    builder.MapDefaultControllerRoute();
    builder.MapControllers();
    builder.MapHub<ChatHub>($"{ApiConstant.SocketPath}/{{groupId}}");
});

app.Run();
