using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 15f;
    public int Damage = 10;
    public float Range = 10f;

    private Vector3 startPosition;
    public IEntity ParentEntity;

    private void Start()
    {
        startPosition = transform.position + Vector3.up;
        GetComponent<Rigidbody2D>().velocity = -transform.up * Speed;
    }

    private void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > Range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // only attack if the object is attackable
        if (other.gameObject.TryGetComponent<IAttackable>(out var attackable))
        {
            attackable.TakeDamage(ParentEntity, Damage);
        }
        // Always destory when colliding with something
        Destroy(gameObject);
    }

}