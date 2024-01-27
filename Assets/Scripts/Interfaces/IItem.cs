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
    void Use(IEntity entityUsing, ItemActions itemAbility);
    void OnPickUp(IEntity entityUsing);
    void OnDrop(IEntity entityUsing);
    void OnSwitch(IEntity entityUsing);
}