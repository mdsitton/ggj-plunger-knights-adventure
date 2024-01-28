
using UnityEngine;

public interface IAttackable : IEntity
{
    /// <summary>
    /// Deal damage to the entity
    /// </summary>
    /// <param name="amount">amount of hp to deal</param>
    /// <param name="attacker">the entity that is dealing the damage</param>
    /// <returns>true if the entity was destroyed</returns>
    bool TakeDamage(IEntity attacker, int amount);
}