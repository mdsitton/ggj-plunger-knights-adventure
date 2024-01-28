using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public int Damage = 10;
    public float Range = 10f;

    private Vector3 startPosition;
    public IEntity ParentEntity;

    private void Start()
    {
        startPosition = transform.position;
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
        var attackable = other.collider.GetComponent<IAttackable>();
        // only attack if the object is attackable
        if (attackable != null)
        {
            attackable.TakeDamage(Damage);
        }
        // Always destory when colliding with something
        Destroy(gameObject);
    }

}