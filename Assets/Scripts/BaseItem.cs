using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(CircleCollider2D))]
public abstract class BaseItem : MonoBehaviour
{
    public IEntity ParentEntity { get; set; }

    public GameObject GameObject => gameObject;

    public int Quantity { get; set; }

    [field: SerializeField]
    public Sprite Icon { get; set; }

    [SerializeField]
    protected float[] actionCooldowns = new float[4];

    public virtual float Cooldown
    {
        get
        {
            return lastAction == null ? 0.1f : actionCooldowns[(int)lastAction.Value];
        }
        set
        { } // do nothing
    }

    protected ItemActions? lastAction = null;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D body;
    protected CircleCollider2D circleCollider;

    public void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        body = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();

        // gameObject.name = typeof(T).Name;

        spriteRenderer.sprite = Icon;
    }

    protected abstract void OnPrimary(bool active);
    protected abstract void OnSecondary(bool active);
    protected abstract void OnTertiary(bool active);
    protected abstract void OnQuaternary(bool active);

    public virtual void Use(IEntity entityUsing, ItemActions itemAbility, bool active)
    {
        lastAction = itemAbility;

        switch (itemAbility)
        {
            case ItemActions.Primary:
                OnPrimary(active);
                break;
            case ItemActions.Secondary:
                OnSecondary(active);
                break;
            case ItemActions.Tertiary:
                OnTertiary(active);
                break;
            case ItemActions.Quaternary:
                OnQuaternary(active);
                break;
        }
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
        // make active
        gameObject.SetActive(true);
    }
}