using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prodavnica.Api.Models;

namespace Prodavnica.Api.Infrastrucutre
{
    public class Configuration : IEntityTypeConfiguration<Oreder>
    {
        public void Configure(EntityTypeBuilder<Oreder> builder)
        {

            builder.HasMany(o => o.Items)
                   .WithMany(o => o.Order);
        }
    }
}
