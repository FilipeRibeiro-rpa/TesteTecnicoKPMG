using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Points.Domain.AggregatesModel.GameResultAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Points.Infrastructure.EntityConfigurations
{
    class GameResultEntityTypeConfiguration : IEntityTypeConfiguration<GameResult>
    {
        public void Configure(EntityTypeBuilder<GameResult> builder)
        {
            builder.ToTable("games", PointsContext.DEFAULT_SCHEMA);

            builder.Property(b => b.Id)
                .UseHiLo("gameseq", PointsContext.DEFAULT_SCHEMA);

            builder.Property(b => b.IdentityGuid)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex("IdentityGuid")
              .IsUnique(true);

            builder.Property(b => b.PlayerId);

            builder.Property(b => b.GameId);

            builder.Property(b => b.Win);
            
            builder.Property(b => b.TimeStamp);

            builder.Property(b => b.CreateDate);
        }
    }
}
