
using UnityEngine;

public interface IAttackable : IEntity
{
    /// <summary>
    /// Deal damage to the entity
    /// </summary>
    /// <param name="amount">amount of hp to deal</param>
    /// <returns>true if the entity was destroyed</returns>
    bool TakeDamage(int amount);
}