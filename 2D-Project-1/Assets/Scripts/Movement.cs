using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 10f;

    private Rigidbody2D rb;
    public Animator animator;
    private Health healthScript;
    private Renderer rend;
    Color color;


    private Vector2 movement;
    private bool facingRight;

    [Header("Layer Mask")]
    private bool isGrounded;
    public Transform feet;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("Jump")]
    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    [Header("Fall")]
    public float fallMultiplier;
    public float lowJumpMultiplier;

    public float knockBackCount;
    public float knockBackLength = 0.2f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthScript = GetComponent<Health>();
        rend = GetComponent<Renderer>();
        color = rend.material.color;
        facingRight = true;
    }

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Xspeed", movement.sqrMagnitude);
        animator.SetFloat("Yspeed", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);

        // Flip character sprite
        if ((movement.x > 0 && !facingRight) || (movement.x < 0 && facingRight)) {
            transform.Rotate(0f, 180f, 0f);
            facingRight = !facingRight;
        }

        // Jumping

        isGrounded = Physics2D.OverlapCircle(feet.position, checkRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump")) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping) {
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } 
            else isJumping = false;
        }

        if(Input.GetButtonUp("Jump")) isJumping = false;

        // Jump fall
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate() {
        if (knockBackCount <= 0 && rb.bodyType == RigidbodyType2D.Dynamic) {
            rb.velocity = new Vector2(movement.x * movementSpeed, rb.velocity.y);
        } else {
            knockBackCount -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Patrol enemy = other.gameObject.GetComponent<Patrol>();

            knockBackCount = knockBackLength;

            animator.SetTrigger("TakeHit");
            healthScript.health--;

            rb.velocity = new Vector2(
                enemy.knockbackForce * (other.transform.position.x < transform.position.x ? 1 : -1),
                enemy.knockbackForce);

            SoundManager.PlaySound("takeHit");

            StartCoroutine(TempInvincible());
        } else if (other.gameObject.CompareTag("Boss")) {
            BossMovement boss = other.gameObject.GetComponent<BossMovement>();

            knockBackCount = knockBackLength;

            animator.SetTrigger("TakeHit");
            healthScript.health--;

            rb.velocity = new Vector2(
                boss.knockbackForce * (other.transform.position.x < transform.position.x ? 1 : -1),
                boss.knockbackForce);

            SoundManager.PlaySound("takeHit");

            StartCoroutine(TempInvincible());
        } 
        else if (other.gameObject.CompareTag("WizardBeam")) {
            knockBackCount = knockBackLength;

            animator.SetTrigger("TakeHit");
            healthScript.health--;

            rb.velocity = new Vector2(5 * (other.transform.position.x < transform.position.x ? 1 : -1), 5);

            SoundManager.PlaySound("takeHit");

            StartCoroutine(TempInvincible());
        } 
        else if (other.gameObject.CompareTag("HeartItem")) {
            Destroy(other.gameObject);
            healthScript.health++;
            SoundManager.PlaySound("getHealth");
        } else if (other.gameObject.CompareTag("WindItem")) {
            Destroy(other.gameObject);
            healthScript.windMagic++;
            SoundManager.PlaySound("getHealth");
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("WindPower") && healthScript.windMagic < 5) {
            StartCoroutine(GainWindPower());
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("WindPower")) {
            StopCoroutine(GainWindPower());
        }
    }

    private IEnumerator TempInvincible() {
        Physics2D.IgnoreLayerCollision(6, 9, true);
        color.a = 0.5f;
        rend.material.color = color;
        yield return new WaitForSeconds(0.6f);
        Physics2D.IgnoreLayerCollision(6, 9, false);
        color.a = 1f;
        rend.material.color = color;
    }

    private IEnumerator GainWindPower() {
        for (int w = healthScript.windMagic; w < 5; w++) {
            healthScript.windMagic = w;
            yield return new WaitForSeconds(0.4f);
        }
        healthScript.windMagic = 5;
    }
}
