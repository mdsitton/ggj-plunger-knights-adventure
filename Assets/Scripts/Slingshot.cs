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

    // slingshot doesn't do damage, but the projectile does
    public int Damage => 1;
    public float Range => 1;

    private float timeSinceLastShot = 0f;

    bool buttonPressed = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Shoot()
    {
        if (timeSinceLastShot < Cooldown)
        {
            return;
        }
        var instance = Instantiate(projectilePrefab, transform.position, transform.rotation);
        var projectile = instance.GetComponent<Projectile>();
        projectile.ParentEntity = ParentEntity;
        timeSinceLastShot = 0;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (buttonPressed)
        {
            Shoot();
        }
    }

    protected override void OnPrimary(bool active)
    {
        buttonPressed = active;
        if (active)
        {
            Shoot();
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