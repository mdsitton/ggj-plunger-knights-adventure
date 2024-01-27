using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IAttackable
{
    private Rigidbody2D body;

    public float speed = 5f;

    public int Health = 100;

    private Controls controls;
    private Controls.GameplayActions actions;

    private Repeater attackTimer = new Repeater(0.25f);

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        controls = new Controls();
        controls.Enable();
        actions = controls.@gameplay;
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