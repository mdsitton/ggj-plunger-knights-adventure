using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private void Awake()
    {
        AttackStateMachine();
    }


    private Rigidbody2D body;
    public int Damage = 10;
    public enum States
    {
        Idle,
        Attack,
        Dead
    }

    public IAttackable CurrentTarget { get; set; }

    private States currentAttackState = States.Idle;
    IEnumerator AttackStateMachine()
    {
        while (true)
        {
            switch (currentAttackState)
            {
                case States.Idle:
                    yield return new WaitForSeconds(1f);
                    Debug.Log("Waiting...1 sec");
                    if (!FindPlayerInRadius(5))
                    {
                        currentAttackState = States.Idle;
                        Debug.Log("In idle state");
                        break;
                    }
                    currentAttackState = States.Attack;
                    Debug.Log("In attack state");
                    break;
                case States.Attack:
                    if (CurrentTarget.TakeDamage(Damage))
                    {
                        currentAttackState = States.Idle;
                        Debug.Log("In attack state");
                    }
                    yield return new WaitForSeconds(0.5f);
                    currentAttackState = States.Attack;
                    Debug.Log("Attacking Target State");
                    break;
                case States.Dead:
                    // Play death animation?
                    break;
            }
        }
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

}
