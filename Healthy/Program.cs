using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Site.Common.Identity;
using Site.Persistance.Contexts;
using Site.Infrastructure;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Site.Application.Contracts.Infrastructure;
using Site.Infrastructure.Mail;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

builder.Services.ConfigureInfrastracturServices(builder.Configuration);

builder.Services.AddScoped<IEmailSender, EmailSender>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer
    ("Server=.;Database=SamplePro;trusted_connection=true;TrustServerCertificate=True;MultipleActiveResultSets=true;");
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    //configure identity options
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 4;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
     .AddUserManager<UserManager<IdentityUser>>()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<DataBaseContext>()
    .AddDefaultTokenProviders();
builder.Services.AddMvc();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypeStore.Admin, true.ToString()));
});
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
