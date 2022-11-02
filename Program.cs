using Microsoft.EntityFrameworkCore;
using ComicBookAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiContext>
    (opt => opt.UseInMemoryDatabase("ComicBooksDB"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication servie
builder.Services.AddAuthentication(x =>
	{
		x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	}).AddJwtBearer(o =>
	{
		var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
		o.SaveToken = true;
		o.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = Configuration["JWT:Issuer"],
			ValidAudience = Configuration["JWT:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Key)
		};
	});

//var _jwtsettings = builder.Configuration.GetSection("JwtSetting");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
