using System.Collections;
using UnityEngine;

public class Plunger : BaseItem, IWeapon
{
    public string Name => "Plungy McPlunger";
    public ItemTypes ItemType => ItemTypes.Weapon;

    public bool Equippable => true;

    public int Damage => 5;
    public float Range => 3f;

    protected override void OnPrimary()
    {
        if (ParentEntity.CurrentTarget != null)
        {
            Debug.Log("Performing primary plunger attack");
            ParentEntity.CurrentTarget.TakeDamage(Damage);
        }

    }

    protected override void OnSecondary()
    {
        if (ParentEntity.Inventory.HasUpgrade(UpgradeTypes.PlungerSuckThrow))
        {
            
        }
    }

    protected override void OnTertiary()
    {
        // TODO - Implement tertiary plunger attack
    }

    protected override void OnQuaternary()
    {
        // TODO - Implement quaternary plunger attack
    }

    public override void Use(IEntity entityUsing, ItemActions itemAbility)
    {
        // Non-players cannot use this item
        if (entityUsing.EntityType != EntityType.Player)
        {
            return;
        }
        base.Use(entityUsing, itemAbility);
    }

    // Can be used to trigger any animations or sounds
    public override void OnPickUp(IEntity entityUsing)
    {
        // Non-players cannot use this item
        if (entityUsing.EntityType != EntityType.Player)
        {
            entityUsing.DropItem(this);
            return;
        }
        base.OnPickUp(entityUsing);
    }
}