using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    private List<IItem> items;
    private List<IItem> unequippableItems;
    private int activeIndex;

    public InventoryManager()
    {
        items = new List<IItem>();
        unequippableItems = new List<IItem>();
        activeIndex = 0;
    }

    private void ClampIndex()
    {
        if (activeIndex < 0)
        {
            activeIndex = 0;
        }
        if (activeIndex >= items.Count)
        {
            activeIndex = items.Count - 1;
        }
    }


    public void AddItemsImpl(List<IItem> itemList, IItem item)
    {
        if (itemList.Count == 0)
        {
            itemList.Add(item);
            return;
        }
        var itemType = item.ItemType;
        // Check if the item already exists in the inventory
        foreach (IItem existingItem in itemList)
        {
            if (existingItem.ItemType == itemType && existingItem.Name == item.Name)
            {
                existingItem.Quantity += item.Quantity;
                return;
            }
        }

        // If the item doesn't exist, add it to the inventory
        itemList.Add(item);
    }

    private IItem RemoveItemsImpl(List<IItem> itemList, IItem item, int quantityToRemove)
    {
        var itemType = item.ItemType;

        IItem removeEntry = null;

        IItem returnItem = null;

        // Check if the item already exists in the inventory
        foreach (IItem existingItem in itemList)
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


    public IItem GetItemFromNameImpl(List<IItem> itemList, string itemName)
    {
        foreach (IItem existingItem in itemList)
        {
            if (existingItem.Name == itemName)
            {
                return existingItem;
            }
        }
        return null;
    }

    public bool HasItemImpl(List<IItem> itemList, IItem item)
    {
        foreach (IItem existingItem in itemList)
        {
            if (existingItem.Equals(item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Removes the item from the inventory. If the item is not in the inventory null is returned.
    /// If the item is in the inventory but the quantity is fewer than the quantity to remove, the item is removed from the inventory.
    /// </summary>
    /// <param name="item"></param>
    public IItem RemoveItem(IItem item, int quantityToRemove)
    {
        if (!item.Equippable)
        {
            return RemoveItemsImpl(unequippableItems, item, quantityToRemove);
        }
        else
        {
            var rtnItem = RemoveItemsImpl(items, item, quantityToRemove);
            ClampIndex();
            return rtnItem;
        }
    }

    private bool MakeActive(int index)
    {
        if (items.Count == 0)
        {
            return true;
        }
        items[activeIndex].GameObject.SetActive(false);
        activeIndex = index;
        ClampIndex();
        items[activeIndex].OnSwitch(items[activeIndex].ParentEntity);
        items[activeIndex].GameObject.SetActive(true);
        return true;
    }

    public void AddItem(IItem item)
    {
        if (item.Equippable)
        {
            bool setActive = items.Count == 0;
            AddItemsImpl(items, item);
            if (setActive)
            {
                MakeActive(0);
            }
            return;
        }
        else
        {
            AddItemsImpl(unequippableItems, item);
        }
    }

    public void SwapPrevItem()
    {
        if (items.Count == 0)
        {
            return;
        }
        var newIndex = activeIndex - 1;
        MakeActive(newIndex);
    }

    public void SwapNextItem()
    {
        if (items.Count == 0)
        {
            return;
        }
        var newIndex = activeIndex + 1;
        MakeActive(newIndex);
    }

    public IItem GetItemFromName(string itemName)
    {
        var item = GetItemFromNameImpl(items, itemName);
        if (item == null)
        {
            item = GetItemFromNameImpl(unequippableItems, itemName);
        }
        return item;
    }

    public bool HasUpgrade(UpgradeTypes type)
    {
        foreach (IItem existingItem in unequippableItems)
        {
            if (existingItem.ItemType == ItemTypes.Upgrade && existingItem is Upgrade upgrade && upgrade.UpgradeType == type)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItem(IItem item)
    {
        if (item.Equippable)
        {
            return HasItemImpl(items, item);
        }
        else
        {
            return HasItemImpl(unequippableItems, item);
        }
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