using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : BaseStateItem, IItem
{
    public string Name { get; set; }

    public ItemTypes ItemType => ItemTypes.Upgrade;

    public UpgradeTypes UpgradeType;
}
