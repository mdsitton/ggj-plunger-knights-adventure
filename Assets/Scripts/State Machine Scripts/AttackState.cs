using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : States
{
    public int CurrentTarget;
    public float attackRadius = 0.5f;
    public override States RunCurrentState()
    {
        if (true)
        {
            if (!FindPlayerInRadius(attackRadius))
            {
                Debug.Log("I have attacked");
            }
        }
        return this;
    }
    private bool FindPlayerInRadius(float radius)
    {
        OnDrawGizmosSelected();

        var colliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, radius);
        foreach (var collider in colliders)
        {
            var attackable = collider.gameObject.GetComponent<IAttackable>();
            if (attackable != null && attackable.EntityType == EntityType.Player)
            {
                //CurrentTarget = collider.gameObject.GetComponent<IAttackable>();
                return true;
            }
        }
        return false;
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }


}
