using UnityEngine;

public class Patrol : MonoBehaviour {

    [HideInInspector] 
    public bool mustPatrol;
    private bool mustTurn;

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D col;
    private Collider2D col2;
    public Transform groundCheckPos;
    public LayerMask groundLayer;

    public bool facingRight;
    public float walkSpeed;
    public float knockbackForce = 10f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        col2 = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        mustPatrol = true;
        facingRight = true;
    }


    void Update() {
        animator.SetFloat("speed", rb.velocity.sqrMagnitude);
        if (mustPatrol) PatrolArea();
    }

    private void FixedUpdate() {
        if (mustPatrol) {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    public void StopPatrol() {
        col.enabled = false;
        col2.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        mustPatrol = false;
    }

    void PatrolArea() {
        if (mustTurn || col.IsTouchingLayers(groundLayer)) {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    
    void Flip() {
        facingRight = !facingRight;
        mustPatrol = false;
        transform.Rotate(0f, 180f, 0f);
        walkSpeed *= -1;
        mustPatrol = true;
    }


}
