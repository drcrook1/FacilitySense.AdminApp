using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FacilitySense.Repositories.SQL;
using FacilitySense.IRepositories;
using FacilitySense.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Register the DB Context
builder.Services.AddDbContext<FacilitySenseDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FacilitySenseDBContext") ?? throw new InvalidOperationException("Connection string 'FacilitySenseDBContext' not found."),
                              x => x.UseNetTopologySuite()
                         ));

builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
                name: "default",
                pattern: "{ controller = Home}/{ action = Index}/{ id ?}");
    endpoints.MapRazorPages();
});

app.MapRazorPages();

app.Run();
