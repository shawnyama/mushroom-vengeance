using System.Collections;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Transform beamPoint;
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private GameObject wizardBeam;
    public Animator animator;
    

    [HideInInspector] 
    public bool mustFight;

    private Collider2D col;

    public bool facingRight;
    public float knockbackForce = 20f;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    [SerializeField] private float minAttackInterval = 4f;
    [SerializeField] private float maxAttackInterval = 7f;

    void Start() {
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        facingRight = false;
        StartCoroutine(Shoot(minAttackInterval, wizardBeam, beamPoint, "Attack1"));
        StartCoroutine(Shoot(maxAttackInterval, enemyProjectile, enemySpawnPoint, "Attack2"));
    }

    public void RemoveAttributes() {
        col.enabled = false;
    }

    private IEnumerator Shoot(float interval, GameObject projectile, Transform attackPoint, string attackTrigger) {
        yield return new WaitForSeconds(interval);
        animator.SetTrigger(attackTrigger);

        if (attackTrigger == "Attack1")
            SoundManager.PlaySound("magic");

        Instantiate(projectile, attackPoint.transform.position, attackPoint.transform.rotation);
        StartCoroutine(Shoot(interval, projectile, attackPoint, attackTrigger));
    }

    private void OnDrawGizmosSelected() {
        if (enemySpawnPoint == null)
            return;
        Gizmos.DrawWireSphere(enemySpawnPoint.position, attackRange);
        if (beamPoint == null)
            return;
        Gizmos.DrawWireSphere(beamPoint.position, attackRange);
    }
}
