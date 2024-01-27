using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : BaseStateItem, IItem
{
    [field: SerializeField]
    public string Name { get; set; }

    public ItemTypes ItemType => ItemTypes.Key;

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
