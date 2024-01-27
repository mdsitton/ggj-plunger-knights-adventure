using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IAttackable
{
    private Rigidbody2D body;

    public float speed = 5f;
    private float radius = 2f;
    private float angle = 0f;

    public int Damage = 10;
    public int Health = 40;

    private Repeater attackTimer = new Repeater(0.5f);

    private Vector2 startingPostion;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startingPostion = body.position;
    }

    private void Update()
    {
        // Move in a circle for testing
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        Vector2 newPosition = startingPostion + new Vector2(x, y);
        body.MovePosition(newPosition);

        attackTimer.Update();

        // we can only have 1 attack target at a time
        // the first collision will set the target and trigger an attack after the timer has elapsed
        if (CurrentTarget != null && attackTimer.HasTriggered())
        {
            CurrentTarget.TakeDamage(Damage);
            CurrentTarget = null;
        }
    }

    public IAttackable CurrentTarget { get; set; }

    void OnCollisionEnter2D(Collision2D other)
    {
        // If we hit an enemy, attack it
        IAttackable attackable = other.gameObject.GetComponent<IAttackable>();
        if (attackable != null && CurrentTarget == null)
        {
            Debug.Log("Collision Enemy");
            CurrentTarget = attackable;
            attackTimer.Reset();
        }
    }

    public EntityType EntityType => EntityType.HairBoy;

    public GameObject GameObject => gameObject;

    public InventoryManager Inventory { get; } = new();

    public bool TakeDamage(int amount)
    {
        if (this.IsUnityNull())
        {
            return true;
        }
        Health -= amount;
        if (Health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}