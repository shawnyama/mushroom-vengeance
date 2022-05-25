using UnityEngine;

public class Beam : MonoBehaviour {
    public float speed = 25f;
    public Rigidbody2D rb;

    public int beamDamage = 3;

    private void FixedUpdate() {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject, 0.1f);
        
    }
}
