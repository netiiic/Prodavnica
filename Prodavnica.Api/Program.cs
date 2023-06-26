using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodavnica.Api.Infrastructure;
using Prodavnica.Api.Interfaces;
using Prodavnica.Api.Mapping;
using Prodavnica.Api.Repository;

namespace Prodavnica.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ProdavnicaDbContext>(o =>
            {
                o.UseSqlServer(builder.Configuration.GetConnectionString("Prodavnica"));
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // SERVICES
            builder.Services.AddScoped<IRepository, RepositoryImplemantation>();

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
        }
    }
}