using UnityEngine;

public class FireProjectile : StateOneShot
{
    [SerializeField]
    private GameObject projectilePrefab;

    private IEntity entity;

    public float Cooldown = 0.5f;
    private float timeSinceLastShot = 0f;

    private void Shoot()
    {
        if (timeSinceLastShot < Cooldown)
        {
            return;
        }
        var instance = Instantiate(projectilePrefab, transform.position, transform.rotation);
        var projectile = instance.GetComponent<Projectile>();
        projectile.ParentEntity = entity;
        timeSinceLastShot = 0;
    }

    private void Awake()
    {
        entity = GetComponent<IEntity>();
    }

    public override void OnStateTrigger(CurrentStateData stateData)
    {
        Shoot();
    }

    public override void OnStateUpdate(CurrentStateData stateData)
    {
        timeSinceLastShot += Time.deltaTime;
        Shoot();
    }
}