using System.Collections;
using UnityEngine;

public class Plunger : BaseItem, IWeapon
{
    public string Name => "Plungy McPlunger";
    public ItemTypes ItemType => ItemTypes.Weapon;

    public int Damage => 5;
    public float Range => 3f;

    protected override void OnPrimary()
    {
        // // Calculate swipe rotation
        // Quaternion swipeRotation = Quaternion.Euler(0f, 90f, 0f);

        // // Set swipe rotation speed
        // float swipeSpeed = 1.0f;

        // Perform swipe motion
        // StartCoroutine(PerformSwipeMotion(swipeRotation, swipeSpeed));

        if (ParentEntity.CurrentTarget != null)
        {
            Debug.Log("Performing primary plunger attack");
            ParentEntity.CurrentTarget.TakeDamage(Damage);
        }

    }

    // private IEnumerator PerformSwipeMotion(Quaternion swipeRotation, float swipeSpeed)
    // {
    //     float currentRotation = 0.0f;

    //     while (currentRotation < 90f)
    //     {
    //         // Rotate the plunger
    //         transform.rotation *= Quaternion.Euler(0f, swipeSpeed * Time.deltaTime, 0f);

    //         // Update the current rotation
    //         currentRotation += swipeSpeed * Time.deltaTime;

    //         yield return null;
    //     }
    // }

    protected override void OnSecondary()
    {
        // TODO - Implement secondary plunger attack
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