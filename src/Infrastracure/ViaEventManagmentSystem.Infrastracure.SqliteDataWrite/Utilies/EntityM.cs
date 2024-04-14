namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.Utilies;

public class EntityM
{
    public MId<Guid> Id { get; }
    public EntityM(MId<Guid> id) => Id = id;
    private EntityM(){}
}