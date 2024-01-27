using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LockedDoor : MonoBehaviour, IEntity
{
    [SerializeField]
    private string KeyName;

    private void UnlockDoor()
    {
        Destroy(gameObject);
        // TODO - Add animation/sound/etc
    }

    public IAttackable CurrentTarget { get; set; }

    void OnCollisionEnter2D(Collision2D other)
    {
        // If we hit an enemy, attack it
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            var item = player.Inventory.GetItemFromName(KeyName);
            if (item != null && item is Key key)
            {
                UnlockDoor();
            }
        }
    }

    public EntityType EntityType => EntityType.LockedDoor;
    public GameObject GameObject => gameObject;

    public InventoryManager Inventory { get; } = null; // Door has no inventory
}