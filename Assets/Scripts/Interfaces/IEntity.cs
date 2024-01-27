using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    EntityType EntityType { get; }
    GameObject GameObject { get; }

    InventoryManager Inventory { get; }

    IAttackable CurrentTarget { get; set; }

    void DropItem(IItem item)
    {
        if (Inventory.HasItem(item))
        {
            Inventory.RemoveItem(item, 1);
            item.OnDrop(this);
        }
    }

    void PickupItem(IItem item)
    {
        if (item.ParentEntity != null)
        {
            item.ParentEntity.Inventory.RemoveItem(item, item.Quantity);
            item.OnDrop(item.ParentEntity);
        }
        Inventory.AddItem(item);
        item.OnPickUp(this);
    }
}