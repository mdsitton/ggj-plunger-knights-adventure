
public interface IAttackable
{
    void TakeDamage(int amount);

    EntityType EntityType { get; }
}