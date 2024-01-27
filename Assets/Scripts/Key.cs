using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : BaseItem, IItem
{
    [field: SerializeField]
    public string Name { get; set; }

    public ItemTypes ItemType => ItemTypes.Key;

    protected override void OnPrimary()
    {
        
    }

    protected override void OnQuaternary()
    {
        
    }

    protected override void OnSecondary()
    {
        
    }

    protected override void OnTertiary()
    {
        
    }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
