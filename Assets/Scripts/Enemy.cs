using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IAttackable
{
    private Rigidbody2D body;

    public float speed = 5f;
    public float radius = 2f;
    private float angle = 0f;

    public int Damage = 10;
    public int Health = 40;

    private Vector2 startingPostion;
    public bool isInRange;

    private int playerMask;

    //public 

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        body = GetComponent<Rigidbody2D>();
        startingPostion = body.position;
        StartCoroutine(AttackStateMachine());
    }

    private void Update()
    {
        // Move in a circle for testing
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        Vector2 newPosition = startingPostion + new Vector2(x, y);
        body.MovePosition(newPosition);

        if (CurrentTarget != null && Vector2.Distance(body.position, CurrentTarget.GameObject.transform.position) > radius)
        {
            CurrentTarget = null;
        }
    }

    public IAttackable CurrentTarget { get; set; }

    private enum State
    {
        Idle,
        Search,
        Attack,
        Dead

    }

    private IAttackable FindPlayerInRadius(float radius)
    {
        var player = Physics2D.OverlapCircle(body.position, radius, playerMask);
        if (player == null)
        {
            return null;
        }
        var attackable = player.gameObject.GetComponent<IAttackable>();
        if (attackable != null && attackable.EntityType == EntityType.Player)
        {
            Debug.Log("Found player in radius, attacking", gameObject);
            return attackable;
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        OnDrawGizmosSelected();
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        //Vector3 offSetPosition = new Vector3(transform.position.x, transform.position.y -1, transform.position.z);
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private State currentAttackState = State.Idle;
    IEnumerator AttackStateMachine()
    {
        while (true)
        {
            switch (currentAttackState)
            {
                case State.Idle:
                    yield return new WaitForSeconds(1f);
                    currentAttackState = State.Search;
                    break;

                //currentAttackState = State.Attack;
                // //Debug.Log("In attack state");
                // break;
                case State.Search:
                    yield return new WaitForSeconds(0.25f);
                    var player = FindPlayerInRadius(radius);
                    if (player == null)
                    {
                        currentAttackState = State.Search;
                    }
                    else
                    {
                        currentAttackState = State.Attack;
                        CurrentTarget = player;
                    }
                    break;
                case State.Attack:
                    if (CurrentTarget == null)
                    {
                        currentAttackState = State.Idle;
                        break;
                    }
                    if (CurrentTarget.TakeDamage(this, Damage))
                    {
                        currentAttackState = State.Idle;
                        CurrentTarget = null;
                        // Debug.Log("In attack state");
                    }
                    yield return new WaitForSeconds(0.25f);
                    currentAttackState = State.Attack;
                    // Debug.Log("Attacking Target State");
                    break;
                case State.Dead:
                    // Play death animation?
                    break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // If we hit an enemy target it
        IAttackable attackable = other.gameObject.GetComponent<IAttackable>();
        if (attackable != null && CurrentTarget == null && attackable.EntityType == EntityType.Player)
        {
            CurrentTarget = attackable;
            Debug.Log(this.gameObject.name + " is Game is attacking " + other.gameObject.name);
        }
    }

    public EntityType EntityType => EntityType.HairBoy;

    public GameObject GameObject => gameObject;

    public InventoryManager Inventory { get; } = new();

    public bool TakeDamage(IEntity source, int amount)
    {
        if (this.IsUnityNull())
        {
            return true;
        }
        Debug.Log($"Enemy taking {amount} damage from {source.GameObject.name}", source.GameObject);
        Health -= amount;
        if (Health <= 0)
        {
            //Destroy(gameObject);
            this.gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}