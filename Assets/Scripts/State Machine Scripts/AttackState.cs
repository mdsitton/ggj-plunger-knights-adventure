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

        Debug.Log("We found Player");
        var colliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, radius);
        foreach (var collider in colliders)
        {
            var attackable = collider.gameObject.GetComponent<IAttackable>();
            if (attackable != null && attackable.EntityType == EntityType.Player)
            {
                Debug.Log("we Found Player");
                //CurrentTarget = collider.gameObject.GetComponent<IAttackable>();
                return true;
            }
        }
        return false;
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
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }


}
