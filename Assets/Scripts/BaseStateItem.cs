using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(CircleCollider2D))]
public abstract class BaseStateItem : MonoBehaviour
{
    public IEntity ParentEntity { get; set; }

    public GameObject GameObject => gameObject;

    public int Quantity { get; set; }

    public bool Equippable => false;

    [field: SerializeField]
    public Sprite Icon { get; set; }

    public float Cooldown => 0.0f;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D body;
    protected CircleCollider2D circleCollider;

    public void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        body = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();

        spriteRenderer.sprite = Icon;
    }

    public virtual void Use(IEntity entityUsing, ItemActions itemAbility, bool active)
    {
    }

    public virtual void OnPickUp(IEntity entityUsing)
    {
        // reparent the item to the entity
        ParentEntity = entityUsing;
        transform.SetParent(entityUsing.GameObject.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        circleCollider.enabled = false;
        body.simulated = false;
    }

    public virtual void OnDrop(IEntity entityUsing)
    {
        ParentEntity = null;
        transform.SetParent(MapManager.Instance.DropsParent.transform);
        transform.position = entityUsing.GameObject.transform.position;
        transform.rotation = entityUsing.GameObject.transform.rotation;
        gameObject.SetActive(true);
        circleCollider.enabled = true;
        body.simulated = true;
    }

    public virtual void OnSwitch(IEntity entityUsing)
    {
        // State Items cannot be switched to
        throw new System.NotImplementedException();
    }
}