using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 inputPlayer = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 movement = inputPlayer.normalized * speed;
        rigidbody.linearVelocity = movement;
        if (inputPlayer.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(inputPlayer.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (inputPlayer != Vector2.zero)
        {
            animator.SetBool("isRun", true);

        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
}
