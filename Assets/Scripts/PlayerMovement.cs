using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public LayerMask layerMask;
    
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;
    
    private bool doubleJumped = false;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PolygonCollider2D polygonCollider2D;
    [SerializeField] private Animator animator;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        ManageMovement();
        ManageAttacks();
    }

    private void ManageAttacks()
    {
        if (IsGrounded() && Input.GetButtonDown("Attack"))
        {
            animator.SetBool("isAttacking", true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) 
        {
            animator.SetBool("isAttacking", false);
        }
        
    }

    private void ManageMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
        if (horizontal != 0 && IsGrounded())
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (IsGrounded())
        {
            doubleJumped = false;
            animator.SetBool("isJumping", false);
        }
        if (!IsGrounded())
        {
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            else if (!doubleJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                doubleJumped = true;
            }
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
        Flip();
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D =
            Physics2D.BoxCast(polygonCollider2D.bounds.center, polygonCollider2D.bounds.size, 0f, Vector2.down, 0.1f, layerMask);
        return raycastHit2D.collider != null;
    }
    
    private void Flip()
        {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                Vector3 localScale = transform.localScale;
                isFacingRight = !isFacingRight;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
}
