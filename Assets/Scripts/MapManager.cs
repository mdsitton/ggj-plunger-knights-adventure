using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public GameObject DropsParent;
}