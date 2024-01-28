using System.Collections;
using UnityEngine;

public class SlingShot : BaseItem, IWeapon
{
    public string Name => "Slingroo";
    public ItemTypes ItemType => ItemTypes.Weapon;

    [SerializeField]
    private GameObject projectilePrefab;

    [field: SerializeField]
    public override float Cooldown { get; set; }

    public bool Equippable => true;
    public int Damage => 5;
    public float Range => 2.5f;

    private Repeater repeater;

    protected override void Awake()
    {
        repeater = new Repeater(Cooldown);
        repeater.Pause();
        base.Awake();
    }

    private void Update()
    {
        repeater.Update();

        if (repeater.HasTriggered())
        {
            var instance = Instantiate(projectilePrefab, transform.position, transform.rotation);
            var projectile = instance.GetComponent<Projectile>();
            projectile.ParentEntity = ParentEntity;
        }
    }

    protected override void OnPrimary(bool active)
    {
        if (active)
        {
            repeater.Reset();
        }
        else
        {
            repeater.Pause();
        }

    }

    protected override void OnSecondary(bool active)
    {
        if (ParentEntity.Inventory.HasUpgrade(UpgradeTypes.SlingShotFlaming))
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
        Debug.Log($"{entityUsing} {itemAbility} {active}");
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