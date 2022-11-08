using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FacilitySense.Repositories.SQL;
using FacilitySense.IRepositories;
using FacilitySense.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using System.Diagnostics;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});

// Add services to the container.
builder.Services.AddRazorPages();

//Register the DB Context
builder.Services.AddDbContext<FacilitySenseDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FacilitySenseDBContext") ?? throw new InvalidOperationException("Connection string 'FacilitySenseDBContext' not found."),
                              x => x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
                         ));

builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
    {
        jwtOptions.MetadataAddress = $"https://{builder.Configuration["B2C:TenantName"]}.b2clogin.com/{builder.Configuration["B2C:TenantId"]}/{builder.Configuration["B2C:Policy"]}/v2.0/.well-known/openid-configuration";
        Trace.WriteLine($"Oauth2 metadata: {jwtOptions.MetadataAddress}");
        //jwtOptions.Authority = $"https://login.microsoftonline.com/tfp/{Configuration["B2C:TenantId"]}/{Configuration["B2C:Policy"]}/v2.0/";
        jwtOptions.Audience = builder.Configuration["B2C:ClientId"];
        jwtOptions.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = (ctx) =>
            {
                Trace.WriteLine($"Bearer token validation failed: {ctx.Exception.Message}");
                var addr = ctx.Options.MetadataAddress;
                return Task.FromResult(0);
            }
        };
    });


builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<FacilitySenseDBContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
                name: "default",
                pattern: "{ controller = Home}/{ action = Index}/{ id ?}")
                .RequireCors(MyAllowSpecificOrigins); ;
    endpoints.MapRazorPages();
});

//app.UseSwagger();
//app.UseSwaggerUI(swaggerUIOptions =>
//{
//    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Facility Watch API v1");
//});

app.MapRazorPages();

app.Run();
