using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(StateManager))]
public class Enemy : MonoBehaviour, IAttackable, IStateSystem
{
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D body;

    public float speed = 5f;
    public float AttackRadius = 2f;
    public float angle = 0f;

    public int Damage = 10;
    public int Health = 40;
    public float AttackCooldown = 0.5f;

    public float AgroRange = 1.5f;
    public float DeagroRange = 3f;

    public StateManager stateManager;

    private Vector2 startingPostion;
    public bool isInRange;

    private int playerMask;

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        body = GetComponent<Rigidbody2D>();
        stateManager = GetComponent<StateManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        startingPostion = body.position;
    }

    private void Update()
    {
        // Move in a circle for testing
        // angle += speed * Time.deltaTime;
        // float x = Mathf.Cos(angle) * radius;
        // float y = Mathf.Sin(angle) * radius;
        // Vector2 newPosition = startingPostion + new Vector2(x, y);
        // body.MovePosition(newPosition);
    }

    public IAttackable CurrentTarget { get; set; }

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
            // Debug.Log("Found player in radius, attacking", gameObject);
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
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    public (AiState nextState, float delayTime) OnIdleState(AiState previousState)
    {
        return (AiState.Search, 0.25f);
    }

    public (AiState nextState, float delayTime) OnSearchState(AiState previousState)
    {
        var player = FindPlayerInRadius(AgroRange);
        // Debug.Log($"player in radius {player}", gameObject);
        if (player == null)
        {
            return (AiState.Idle, 0.25f);
        }
        CurrentTarget = player;

        return (AiState.Attack, 0.25f);
    }

    public (AiState nextState, float delayTime) OnAttackState(AiState previousState)
    {
        if (CurrentTarget == null)
        {
            return (AiState.Idle, 0.25f);
        }

        var distance = Vector2.Distance(body.position, CurrentTarget.GameObject.transform.position);

        if (wasProjectile == false && distance > DeagroRange)
        {
            CurrentTarget = null;
            return (AiState.Idle, 0.25f);
        }

        if (distance < AttackRadius && CurrentTarget.TakeDamage(this, Damage))
        {
            // Other is dead
            CurrentTarget = null;
            return (AiState.Idle, 0.25f);
        }
        return (AiState.Attack, AttackCooldown);
    }

    public (AiState nextState, float delayTime) OnDeadState(AiState previousState)
    {
        // Play death animation?
        //Destroy(gameObject);
        gameObject.SetActive(false);
        return (AiState.Dead, 0f);
    }

    private bool wasProjectile = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        // If we hit an enemy target it
        if (other.gameObject.TryGetComponent<IAttackable>(out var attackable))
        {
            if (CurrentTarget == null && attackable.EntityType == EntityType.Player)
            {
                CurrentTarget = attackable;
                // Debug.Log(this.gameObject.name + " is Game is attacking " + other.gameObject.name);
            }
        }

        if (other.gameObject.TryGetComponent<Projectile>(out var projectile) && projectile.ParentEntity is IAttackable attck)
        {
            CurrentTarget = attck;
            if (stateManager.CurrentStateData.newState != AiState.Dead)
            {
                stateManager.ChangeState(AiState.Attack);
                // TODO - add a timeout for how long enemy will chase player
                wasProjectile = true;
            }
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
//        Debug.Log($"Enemy taking {amount} damage from {source.GameObject.name}", source.GameObject);
        Health -= amount;
        if (isActiveAndEnabled)
        {
            StartCoroutine(TurnRed());
        }
        if (Health <= 0)
        {
            stateManager.ChangeState(AiState.Dead);
            return true;
        }
        return false;
    }

    public IEnumerator TurnRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}