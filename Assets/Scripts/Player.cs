using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IAttackable, Controls.IGameplayActions
{
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    public float speed = 5f;

    public int Health = 100;

    private Controls controls;
    private Controls.GameplayActions actions;

    private Animator knightAnim;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        controls = new Controls();
        controls.Enable();
        actions = controls.gameplay;
        knightAnim = GetComponent<Animator>();
        controls.gameplay.SetCallbacks(this);
    }

    private void Update()
    {
        Vector2 moveDirection = actions.Move.ReadValue<Vector2>();
        body.velocity = moveDirection * speed;

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
        }

        float v = moveDirection.y;
        knightAnim.SetFloat("VerticalSpeed", v);
        float h = moveDirection.x;
        knightAnim.SetFloat("HorizontalSpeed", h);
    }



    public void OnMove(InputAction.CallbackContext context)
    {
    }

    private void DoItemAction(InputAction.CallbackContext context, ItemActions itemAction)
    {
        var activeItem = Inventory.GetActiveItem();
        if (activeItem != null)
        {
            if (context.performed)
            {
                WeaponUtilities.CheckWeaponInRange(activeItem);
                activeItem.Use(this, itemAction, true);
            }
            else if (context.canceled)
            {
                activeItem.Use(this, itemAction, false);
            }
        }
    }

    public void OnItemMainAction(InputAction.CallbackContext context)
    {
        Debug.Log($"OnItemMainAction {context.performed} {context.canceled}");
        DoItemAction(context, ItemActions.Primary);
    }

    public void OnItemMinorAction1(InputAction.CallbackContext context)
    {
        DoItemAction(context, ItemActions.Secondary);
    }

    public void OnItemMinorAction2(InputAction.CallbackContext context)
    {
        DoItemAction(context, ItemActions.Tertiary);
    }

    public void OnItemMinorAction3(InputAction.CallbackContext context)
    {
        DoItemAction(context, ItemActions.Quaternary);
    }

    public void OnSwitchItems(InputAction.CallbackContext context)
    {
        var swap = context.ReadValue<float>();

        if (swap > 0)
        {
            Inventory.SwapNextItem();
        }
        else if (swap < 0)
        {
            Inventory.SwapPrevItem();
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