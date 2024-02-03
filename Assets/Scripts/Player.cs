using System.Collections;
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

    private Animator knightAnim;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        controls = new Controls();
        controls.Enable();
        actions = controls.gameplay;
        knightAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveDirection = actions.Move.ReadValue<Vector2>();
        body.velocity = moveDirection * speed;

        var activeItem = Inventory.GetActiveItem();

        if (activeItem != null)
        {
            // Rotate the item to face the last movement direction
            if (moveDirection.x != 0 || moveDirection.y != 0)
            {
                knightAnim.SetFloat("VerticalSpeed", moveDirection.y);
                knightAnim.SetFloat("HorizontalSpeed", moveDirection.x);
                activeItem.GameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, -moveDirection);
            }
            activeItem.GameObject.transform.position = (transform.position + (Vector3)moveDirection * 0.3f) + Vector3.up * 0.3f;
            if (moveDirection.y > 0)
            {
                spriteRenderer.sortingOrder = 20;
            }
            else
            {
                spriteRenderer.sortingOrder = 1;
            }

            var primaryPressed = actions.ItemMainAction.WasPerformedThisFrame();
            var primaryReleased = actions.ItemMainAction.WasReleasedThisFrame();

            var secondaryPressed = actions.ItemMinorAction1.WasPressedThisFrame();
            var secondaryReleased = actions.ItemMinorAction1.WasReleasedThisFrame();

            var tertiaryPressed = actions.ItemMinorAction2.WasPressedThisFrame();
            var tertiaryReleased = actions.ItemMinorAction2.WasReleasedThisFrame();

            var quaternaryPressed = actions.ItemMinorAction3.WasPressedThisFrame();
            var quaternaryReleased = actions.ItemMinorAction3.WasReleasedThisFrame();

            if (primaryPressed)
            {
                DoItemAction(activeItem, ItemActions.Primary, true);
            }
            if (primaryReleased)
            {
                DoItemAction(activeItem, ItemActions.Primary, false);
            }

            if (secondaryPressed)
            {
                DoItemAction(activeItem, ItemActions.Secondary, true);
            }
            if (secondaryReleased)
            {
                DoItemAction(activeItem, ItemActions.Secondary, false);
            }

            if (tertiaryPressed)
            {
                DoItemAction(activeItem, ItemActions.Tertiary, true);
            }
            if (tertiaryReleased)
            {
                DoItemAction(activeItem, ItemActions.Tertiary, false);
            }

            if (quaternaryPressed)
            {
                DoItemAction(activeItem, ItemActions.Quaternary, true);
            }
            if (quaternaryReleased)
            {
                DoItemAction(activeItem, ItemActions.Quaternary, false);
            }

            if (actions.SwitchItems.WasPressedThisFrame())
            {
                var swap = actions.SwitchItems.ReadValue<float>();

                if (swap > 0)
                {
                    Inventory.SwapNextItem();
                }
                else if (swap < 0)
                {
                    Inventory.SwapPrevItem();
                }
            }
        }

    }

    private void DoItemAction(IItem item, ItemActions itemAction, bool pressed)
    {
        if (pressed)
        {
            WeaponUtilities.CheckWeaponInRange(item);
        }
        item.Use(this, itemAction, pressed);
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

    public bool TakeDamage(IEntity source, int amount)
    {
        if (gameObject.IsUnityNull())
        {
            return true;
        }
        // Debug.Log($"Player taking {amount} damage from {source.GameObject.name}", source.GameObject);
        StartCoroutine(TurnRed());
        Health -= amount;
        if (Health <= 0)
        {
            knightAnim.SetBool("Dead", true);
            SceneManager.LoadScene(3);
            return true;
        }
        return false;
    }

    public IEnumerator TurnRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}