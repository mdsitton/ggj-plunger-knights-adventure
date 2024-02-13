using UnityEngine;

public class ItemDropRandom : MonoBehaviour
{
    public GameObject[] itemToDrop;
    private GameObject newDrop;
    public int randomDropGen;

    private void Awake()
    {
        randomDropGen = Random.Range(0, itemToDrop.Length);
    }

    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        if (itemToDrop[randomDropGen] != null)
        {
             newDrop = Instantiate(itemToDrop[randomDropGen]);
            newDrop.transform.position = this.transform.position;
            Debug.Log("We create item when parent dies");//There is probably more optimized way to achive this. Maybe a when we get around to Pool Manager
        }
    }
}
