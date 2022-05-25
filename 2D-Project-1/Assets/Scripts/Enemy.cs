using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public Animator animator;
    public GameObject[] drop;

    public int maxHealth = 6;
    int currentHealth;

    public EnemyHealthBar enemyHealthBar;

    private Checkpoint checkpoint;
    [SerializeField] private GameObject victoryMessage;

    void Start() {
        if (victoryMessage != null) victoryMessage.SetActive(false);
        currentHealth = maxHealth;
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        enemyHealthBar.SetHealth(currentHealth);

        animator.SetTrigger("TakeHit");
        SoundManager.PlaySound("hit");

        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        SoundManager.PlaySound("cut");
        if (gameObject.CompareTag("Enemy")) gameObject.GetComponent<Patrol>().StopPatrol();
        if (gameObject.CompareTag("Boss")) gameObject.GetComponent<BossMovement>().RemoveAttributes();
        animator.SetBool("IsDead", true);

        Destroy(gameObject, 1.2f);

        if (gameObject.CompareTag("Enemy")) {
            int r = Random.Range(0, drop.Length);
            if (r < 2) { // 2 out of 3 chance of getting an item
                Instantiate(drop[r], transform.position, drop[r].transform.rotation);
            }
        } else if (gameObject.CompareTag("Boss") && (victoryMessage != null)) {
            checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();
            checkpoint.lastCheckpoint = new Vector2(-13, -2); // Prepare for scene 1
            victoryMessage.SetActive(true); // Show victory message once boss is dead
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Beam")) {
            Beam beam = collision.gameObject.GetComponent<Beam>();
            TakeDamage(beam.beamDamage);
        }
    }
}
