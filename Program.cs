using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Services;
using MyStore.Operations.Items;
using MyStore.Operations.Customers;
using MyStore.Operations.CartItems;
using MyStore.Operations.Users;
using MyStore.Filters;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddScoped<IItemService, EFItemService>();
builder.Services.AddScoped<ValidateSaveItem, ValidateSaveItem>();
builder.Services.AddScoped<ICustomerService, EFCustomerService>();
builder.Services.AddScoped<IUserService, EFUserService>();
builder.Services.AddScoped<ValidateSaveCustomer, ValidateSaveCustomer>();
builder.Services.AddScoped<ICartItemService, EFCartItemService>();
builder.Services.AddScoped<ValidateSaveCartItem, ValidateSaveCartItem>();
builder.Services.AddScoped<AuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthenticationFilter, AuthenticationFilter>();
builder.Services.AddScoped<ValidateRegister, ValidateRegister>();

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSqlConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
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

app.UseCors(MyAllowSpecificOrigins);
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
