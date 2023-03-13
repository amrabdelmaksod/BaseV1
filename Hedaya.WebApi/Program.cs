using Hedaya.Application.Auth.Abstractions;
using Hedaya.Application.Auth.Models;
using Hedaya.Application.Auth.Services;
using Hedaya.Application.Certificates.Services;
using Hedaya.Application.Infrastructure;
using Hedaya.Application.Interfaces;
using Hedaya.Application.Services;
using Hedaya.Domain.Entities.Authintication;
using Hedaya.Domain.Entities.Seeds;
using Hedaya.Infrastructure.Presistence;
using Hedaya.WebApi;
using Hedaya.WebApi.Extensions;
using Hedaya.WebApi.Interfaces;
using Hedaya.WebApi.Middlewares;
using Hedaya.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);





    //Add JWT Configurations
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}
).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidAudience = builder.Configuration["JWT:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
});
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddTransient<ICertificateRepository, CertificateRepository>();
builder.Services.AddTransient<IPdfService, PdfService>();

builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);










//Add Google External Login Service
builder.Services.AddAuthentication()
                              .AddGoogle(options =>
                          {
                              IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authintication:Google");
                              options.ClientId = googleAuthSection["ClientId"];
                              options.ClientSecret = googleAuthSection["ClientSecret"];
                          });




//Add Send Grid Service For Sending Emails
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);


builder.Services.ConfigureApplicationCookie(o => {
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
}); 

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
       o.TokenLifespan = TimeSpan.FromHours(3));



//Add Twilio SMS Service
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
builder.Services.AddTransient<ISMSService, SMSService>();

// Add services to the container.
builder.Services.ConfigureLoggerService();
builder.Services.AddControllers();

//Add Localization
builder.Services.AddLocalization();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(JsonStringLocalizerFactory));
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
    };

    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures= supportedCultures;
    options.SupportedUICultures= supportedCultures;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v4", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

//Add Mediator Services
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(typeof(ApplicationDbContext).Assembly);


builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});



// Add ApiExplorer to discover versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();



builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

//seeding default users and roles

var scope = app.Services.CreateScope();


var services = scope.ServiceProvider;

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

var logger = loggerFactory.CreateLogger("app");

try
{
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRoles.SeedAsync(roleManager);
    await DefaultUsers.SeedBasicUserAsync(userManager);
    await DefaultUsers.SeedSuperAdminUserAsync(userManager,roleManager);

    logger.LogInformation("Data Seeded");

}
catch (Exception)
{

    logger.LogWarning("Ann error occured while seeding default data!");
}




LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
var loggerManager = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(loggerManager);


app.UseMiddleware<ExceptionMiddleware>();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });





app.UseHttpsRedirection();

app.UseAuthentication();

var supportedCultures = new[] {"en-US", "ar-EG" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0]).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);


app.UseAuthorization();

app.MapControllers();

app.Run();
