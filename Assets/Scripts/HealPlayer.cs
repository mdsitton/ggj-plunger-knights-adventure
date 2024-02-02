using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealPlayer : MonoBehaviour
{
    public int healHP;
    public IEntity ParentEntity;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IAttackable>(out var attackable))
        {
            attackable.TakeDamage(ParentEntity, (-1*healHP));
        }
        //this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // Always destory when colliding with something
        //Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision collision)
    {
        Destroy(this.gameObject);
    }

    public bool HealAmount(IEntity source, int amount)
    {
        if (gameObject.IsUnityNull())
        {
            return true;
        }
        // Debug.Log($"Player taking {amount} damage from {source.GameObject.name}", source.GameObject);
        //StartCoroutine(TurnRed());
        if (source.GameObject.GetComponent<Player>().Health >= 100)
        {
            source.GameObject.GetComponent<Player>().Health = 100;
            //knightAnim.SetBool("Dead", true);
            return true;
        }
        source.GameObject.GetComponent<Player>().Health += amount;

        return false;
    }
}
