using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FacilitySense.Repositories.SQL;
using FacilitySense.IRepositories;
using FacilitySense.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;


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
