using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    private List<IItem> items;
    private int activeIndex;

    public IReadOnlyList<IItem> Items => items;

    public InventoryManager()
    {
        items = new List<IItem>();
        activeIndex = 0;
    }

    /// <summary>
    /// Removes the item from the inventory. If the item is not in the inventory null is returned.
    /// If the item is in the inventory but the quantity is fewer than the quantity to remove, the item is removed from the inventory.
    /// </summary>
    /// <param name="item"></param>
    public IItem RemoveItem(IItem item, int quantityToRemove)
    {
        var itemType = item.ItemType;

        IItem removeEntry = null;

        IItem returnItem = null;

        // Check if the item already exists in the inventory
        foreach (IItem existingItem in items)
        {
            if (existingItem.ItemType == itemType && existingItem.Name == item.Name)
            {
                if (existingItem.Quantity <= quantityToRemove)
                {
                    item.Quantity += existingItem.Quantity;
                    removeEntry = existingItem;
                    returnItem = existingItem;
                    break;
                }
                else
                {
                    existingItem.Quantity -= quantityToRemove;
                    // Clone the game object and return it with the correct quantity
                    var clone = GameObject.Instantiate(existingItem.GameObject, existingItem.GameObject.transform.position, existingItem.GameObject.transform.rotation);
                    var newItem = clone.GetComponent<IItem>();
                    newItem.Quantity = quantityToRemove;
                    returnItem = newItem;
                    break;
                }
            }
        }

        if (removeEntry != null)
        {
            items.Remove(removeEntry);
        }
        return returnItem;
    }

    private void MakeActive(int index)
    {
        if (items.Count == 0)
        {
            return;
        }
        items[activeIndex].GameObject.SetActive(false);
        activeIndex = index;
        items[index].OnSwitch(items[index].ParentEntity);
        items[index].GameObject.SetActive(true);
    }

    public void AddItem(IItem item)
    {
        if (items.Count == 0)
        {
            items.Add(item);
            MakeActive(0);
            return;
        }
        var itemType = item.ItemType;
        // Check if the item already exists in the inventory
        foreach (IItem existingItem in items)
        {
            if (existingItem.ItemType == itemType && existingItem.Name == item.Name)
            {
                existingItem.Quantity += item.Quantity;
                return;
            }
        }

        // If the item doesn't exist, add it to the inventory
        items.Add(item);
    }

    public void SwapPrevItem()
    {
        if (items.Count == 0)
        {
            return;
        }
        activeIndex--;
        if (activeIndex <= 0)
        {
            activeIndex = items.Count - 1;
        }
        MakeActive(activeIndex);
    }

    public void SwapNextItem()
    {
        if (items.Count == 0)
        {
            return;
        }
        activeIndex++;
        if (activeIndex >= items.Count)
        {
            activeIndex = 0;
        }
        MakeActive(activeIndex);
    }

    public IItem GetItemFromName(string itemName)
    {
        foreach (IItem existingItem in items)
        {
            if (existingItem.Name == itemName)
            {
                return existingItem;
            }
        }
        return null;
    }

    public bool HasItem(IItem item)
    {
        foreach (IItem existingItem in items)
        {
            if (existingItem.Equals(item))
            {
                return true;
            }
        }
        return false;
    }

    public IItem GetActiveItem()
    {
        if (items.Count == 0)
        {
            return null;
        }

        return items[activeIndex];
    }
}