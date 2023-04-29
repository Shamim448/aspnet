public interface IIdBase<G>
{
    public G Id { get; set; }
}

public interface IEntity<G>
{
    G Id { get; set; }
}