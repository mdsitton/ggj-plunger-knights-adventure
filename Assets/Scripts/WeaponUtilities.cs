
using UnityEngine;

public static class WeaponUtilities
{
    public static void CheckWeaponInRange(IItem activeItem)
    {
        var go = activeItem.GameObject;
        if (activeItem.ItemType == ItemTypes.Weapon && activeItem is IWeapon weapon)
        {
            // Perform raycast to find IAttackable objects
            RaycastHit2D[] hits = Physics2D.RaycastAll(go.transform.position, -go.transform.up, weapon.Range);
            foreach (RaycastHit2D hit in hits)
            {
                IAttackable attackable = hit.collider.GetComponent<IAttackable>();
                if (attackable != null && attackable != activeItem.ParentEntity)
                {
                    activeItem.ParentEntity.CurrentTarget = attackable;
                    // Debug.Log($"Weapon found target: {attackable.GameObject.name}");
                    break;
                }
            }
        }
    }
}