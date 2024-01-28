using UnityEngine;

public interface IItem
{
    string Name { get; }
    Sprite Icon { get; }

    IEntity ParentEntity { get; set; }
    GameObject GameObject { get; }

    int Quantity { get; set; }

    ItemTypes ItemType { get; }
    float Cooldown { get; }

    bool Equippable { get; }

    void Use(IEntity entityUsing, ItemActions itemAbility, bool active);
    void OnPickUp(IEntity entityUsing);
    void OnDrop(IEntity entityUsing);
    void OnSwitch(IEntity entityUsing);
}