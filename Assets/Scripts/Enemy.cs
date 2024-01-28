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

    //public 

    private void Start()
    {
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
    }

    public IAttackable CurrentTarget { get; set; }

    private enum State
    {
        Idle,
        Search,
        Attack,
        Dead

    }

    private bool FindPlayerInRadius(float radius)
    {
        var colliders = Physics2D.OverlapCircleAll(body.position, radius);
        foreach (var collider in colliders)
        {
            var attackable = collider.gameObject.GetComponent<IAttackable>();
            if (attackable != null && attackable.EntityType == EntityType.Player)
            {
                CurrentTarget = collider.gameObject.GetComponent<IAttackable>();
                return true;
            }
        }
        return false;
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
                    // Debug.Log("Waiting...1 sec");
                    if (!FindPlayerInRadius(5))
                    {
                        currentAttackState = State.Idle;
                        // Debug.Log("In idle state");
                        break;
                    }
                    else
                    {
                        currentAttackState = State.Search;
                        // Debug.Log("Searching for player");
                        break;
                    }
                //currentAttackState = State.Attack;
                // //Debug.Log("In attack state");
                // break;
                case State.Search:
                    yield return new WaitForSeconds(1);
                    if (FindPlayerInRadius(5))
                    {
                        // Debug.Log("Player found");
                        currentAttackState = State.Attack;
                        break;
                    }
                    else
                    {
                        currentAttackState = State.Search;
                    }
                    break;
                case State.Attack:
                    if (CurrentTarget.TakeDamage(Damage))
                    {
                        currentAttackState = State.Idle;
                        // Debug.Log("In attack state");
                    }
                    yield return new WaitForSeconds(0.5f);
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
        if (attackable != null && CurrentTarget == null)
        {
            CurrentTarget = attackable;
            Debug.Log(this.gameObject.name + " is Game is attacking " + other.gameObject.name);
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
            //Destroy(gameObject);
            this.gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}