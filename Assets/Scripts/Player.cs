using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IAttackable
{
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    public float speed = 5f;

    public int Health = 100;

    private Controls controls;
    private Controls.GameplayActions actions;

    private Repeater attackTimer = new Repeater(0.25f);

    private Animator knightAnim;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        controls = new Controls();
        controls.Enable();
        actions = controls.@gameplay;
        knightAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveDirection = actions.Move.ReadValue<Vector2>();

        body.velocity = moveDirection * speed;

        attackTimer.Update();

        // if (CurrentTarget != null && attackTimer.HasTriggered())
        // {
        //     CurrentTarget.TakeDamage(Damage);
        //     CurrentTarget = null;
        // }

        var swap = actions.SwitchItems.ReadValue<float>();

        if (swap > 0)
        {
            Inventory.SwapNextItem();
        }
        else if (swap < 0)
        {
            Inventory.SwapPrevItem();
        }

        var activeItem = Inventory.GetActiveItem();


        if (activeItem != null)
        {
            activeItem.GameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, -moveDirection);
            activeItem.GameObject.transform.position = (transform.position + (Vector3)moveDirection * 0.3f) + Vector3.up * 0.3f;
            if (moveDirection.y > 0)
            {
                spriteRenderer.sortingOrder = 20;
            }
            else
            {
                spriteRenderer.sortingOrder = 1;
            }
            // spriteRenderer.sortingOrder = 5;
            if (actions.ItemMainAction.triggered)
            {
                WeaponUtilities.CheckWeaponInRange(activeItem);
                activeItem.Use(this, ItemActions.Primary);
            }

            if (actions.ItemMinorAction1.triggered)
            {
                WeaponUtilities.CheckWeaponInRange(activeItem);
                activeItem.Use(this, ItemActions.Secondary);
            }

            if (actions.ItemMinorAction2.triggered)
            {
                WeaponUtilities.CheckWeaponInRange(activeItem);
                activeItem.Use(this, ItemActions.Tertiary);
            }

            if (actions.ItemMinorAction3.triggered)
            {
                WeaponUtilities.CheckWeaponInRange(activeItem);
                activeItem.Use(this, ItemActions.Quaternary);
            }
        }

        float v = moveDirection.y;
        knightAnim.SetFloat("VerticalSpeed", v);
        float h = moveDirection.x;
        knightAnim.SetFloat("HorizontalSpeed", h);
    }

    public IAttackable CurrentTarget { get; set; }

    void OnTriggerEnter2D(Collider2D other)
    {
        IItem item = other.gameObject.GetComponent<IItem>();

        // Item pickup
        if (item != null)
        {
            item.OnPickUp(this);
            Inventory.AddItem(item);
        }
    }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     var go = other.gameObject;
    //     // If we hit an enemy, attack it
    //     IAttackable attackable = go.GetComponent<IAttackable>();
    //     if (attackable != null && CurrentTarget == null)
    //     {
    //         Debug.Log("Collision Player");
    //         CurrentTarget = attackable;
    //         attackTimer.Reset();
    //     }

    // }

    public EntityType EntityType => EntityType.Player;

    public GameObject GameObject => gameObject;

    public InventoryManager Inventory { get; } = new InventoryManager();

    public bool TakeDamage(int amount)
    {
        if (gameObject.IsUnityNull())
        {
            return true;
        }
        Health -= amount;
        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return true;
        }
        return false;
    }
}