using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class CatController : MonoBehaviour
{
    // Variables related to temporary invincibility
    public float timeIncincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    // Variables related to slow healing
    public float timeNotHealing = 2.0f;
    bool isNotHealing;
    float healingCooldown;

    // Variables related to health system
    public int maxHealth = 9;
    public int health {  get { return currentHealth;  } }
    int currentHealth;

    // Variables related to player's movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 5.0f;

    //Animation related variables
    Animator animator;
    Vector2 moveDirection = new Vector2 (1, 0);

    //Variables related to dialogs
    public InputAction talkAction;

    //Variables related to door opening
    public Boolean hasKey;

    //Floor hole
    HoleFloor hole;


    void Start()
    {
        hole = FindObjectOfType<HoleFloor>(true); //inactive objects will be included
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

        talkAction.Enable();
        talkAction.performed += FindFriend;

        hasKey = false;
    }


    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
        if (isNotHealing)
        {
            healingCooldown -= Time.deltaTime;
            if (healingCooldown < 0)
            {
                isNotHealing = false;
            }
        }

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        //Animations
        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Dead");
        }
        else
        {
            animator.SetBool("Dead", false);
        }

        if(hasKey)
        {
            hole.ShowHole();
        }
    }


    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth (int amount, bool isHealingZone)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeIncincible;
        }
        else if (amount > 0 && isHealingZone)
        {
            if (isNotHealing)
            {
                return;
            }
            isNotHealing = true;
            healingCooldown = timeNotHealing;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void FindFriend(InputAction.CallbackContext context)
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            NPC character = hit.collider.GetComponent<NPC>();
            if (character != null)
            {
                UIHandler.instance.DisplayDialog(character, this);
                Debug.Log("Hit");
            }
        }
    }
}