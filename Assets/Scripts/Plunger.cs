using System.Collections;
using UnityEngine;

public class Plunger : BaseItem, IWeapon
{
    public string Name => "Plungy McPlunger";
    public ItemTypes ItemType => ItemTypes.Weapon;

    public bool Equippable => true;

    [field: SerializeField]
    public int Damage { get; set; } = 5;

    [field: SerializeField]
    public float Range { get; set; } = 2.0f;

    protected override void OnPrimary(bool active)
    {
        if (active && ParentEntity.CurrentTarget != null)
        {
            Debug.Log("Performing primary plunger attack");
            ParentEntity.CurrentTarget.TakeDamage(ParentEntity, Damage);
        }
    }

    protected override void OnSecondary(bool active)
    {
        if (ParentEntity.Inventory.HasUpgrade(UpgradeTypes.PlungerSuckThrow))
        {

        }
    }

    protected override void OnTertiary(bool active)
    {
        // TODO - Implement tertiary plunger attack
    }

    protected override void OnQuaternary(bool active)
    {
        // TODO - Implement quaternary plunger attack
    }

    public override void Use(IEntity entityUsing, ItemActions itemAbility, bool active)
    {
        // Non-players cannot use this item
        if (entityUsing.EntityType != EntityType.Player)
        {
            return;
        }
        base.Use(entityUsing, itemAbility, active);
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