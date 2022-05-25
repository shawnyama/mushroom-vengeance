using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Transform attackPoint;
    public Transform beamPoint;
    public Animator animator;
    public GameObject beam;
    public LayerMask enemyLayers;

    Health healthScript;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public float attackRate = 3f;
    float nextAttackTime = 0f;

    void Start() {
        healthScript = GetComponent<Health>();
    }

    void Update() {
        if (Time.time >= nextAttackTime) {
            if (Input.GetButtonDown("Attack2") && healthScript.windMagic > 0) {
                Shoot();
            }
            if (Input.GetButtonDown("Attack2") && healthScript.windMagic == 0) {
                SoundManager.PlaySound("error");
            }
            if (Input.GetButtonDown("Attack1")) {
                Attack();
            }
        }
    }

    void Shoot() {
        Attack();
        healthScript.windMagic--;
        Instantiate(beam, beamPoint.position, beamPoint.rotation);
    }

    private void Attack() {
        animator.SetTrigger("Attack"); // Attack animation

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); // Detect enemies in range of attack

        foreach (Collider2D enemy in hitEnemies) {// Damage enemies
            if (enemy.CompareTag("Enemy") || enemy.CompareTag("Boss"))
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        nextAttackTime = Time.time + 1f / attackRate;
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        if (beamPoint == null)
            return;
        Gizmos.DrawWireSphere(beamPoint.position, attackRange);
    }
}
