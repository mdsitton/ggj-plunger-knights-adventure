using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject itemToDrop;
    private GameObject newDrop;

    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        if (itemToDrop != null)
        {
             newDrop = Instantiate(itemToDrop);
            newDrop.transform.position = this.transform.position;
            Debug.Log("We create item when parent dies");//There is probably more optimized way to achive this. Maybe a when we get around to Pool Manager
        }
    }
}
