using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IAttackable
{
    private Rigidbody2D body;

    private float speed = 5f;
    private float radius = 2f;
    private float angle = 0f;

    public int Damage = 10;
    public int Health = 100;

    private Repeater attackTimer = new Repeater(0.25f);

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move in a circle for testing
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        Vector2 newPosition = new Vector2(-x, y);
        body.MovePosition(newPosition);

        attackTimer.Update();

        // we can only have 1 attack target at a time
        // the first collision will set the target and trigger an attack after the timer has elapsed
        if (currentAttackTarget != null && attackTimer.HasTriggered())
        {
            currentAttackTarget.TakeDamage(Damage);
            currentAttackTarget = null;
        }
    }

    private IAttackable currentAttackTarget;

    void OnCollisionEnter2D(Collision2D other)
    {
        // If we hit an enemy, attack it
        IAttackable attackable = other.gameObject.GetComponent<IAttackable>();
        if (attackable != null && currentAttackTarget == null)
        {
            Debug.Log("Collision Player");
            currentAttackTarget = attackable;
            attackTimer.Reset();
        }
    }


    public EntityType EntityType => EntityType.Player;

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}