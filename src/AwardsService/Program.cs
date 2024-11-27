
using AwardsService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

 

builder.Services.AddControllers();
 
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
 
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AwardsDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try{
    
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AwardsDbContext>();
        context.Database.EnsureCreated();
        DbInitializer.InitDb(app);
    }

}
catch(Exception ex){
    Console.WriteLine(ex.ToString());
}

app.Run();
  
 