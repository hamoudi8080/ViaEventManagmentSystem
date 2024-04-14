using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.Utilies;

public class EntityMConfig:IEntityTypeConfiguration<EntityM>
{
    

    public void Configure(EntityTypeBuilder<EntityM> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(m => m.Id)
            .HasConversion(
                mId => mId.Value,
                dbValue => MId<Guid>.FromGuid(dbValue)
            );
    }
}