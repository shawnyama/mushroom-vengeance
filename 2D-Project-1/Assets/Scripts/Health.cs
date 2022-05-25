using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    Animator animator;
    public Animator fadeAnimation;

    private Collider2D col;
    private Rigidbody2D rb;
    private Checkpoint checkpoint;

    public int health;
    public int numOfHearts;

    public int windMagic;
    public int numOfWindMagic;

    public Image[] hearts;
    public Image[] tornadoes;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Sprite fullTornado;
    public Sprite emptyTornado;

    private void Start() {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();
        transform.position = checkpoint.lastCheckpoint;
    }

    void Update() {

        if (health > numOfHearts) health = numOfHearts;

        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) hearts[i].sprite = fullHeart;
            else hearts[i].sprite = emptyHeart;

            if (i < numOfHearts) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }

        if (health == 0) {
            col.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            animator.SetBool("IsDead", true);
        }

        if (windMagic > numOfWindMagic) windMagic = numOfWindMagic;

        for (int i = 0; i < tornadoes.Length; i++) {
            if (i < windMagic)tornadoes[i].sprite = fullTornado;
            else tornadoes[i].sprite = emptyTornado;

            if (i < numOfWindMagic) tornadoes[i].enabled = true;
            else tornadoes[i].enabled = false;
        }
    }

    public void DeathFadeEvent() {
        fadeAnimation.SetTrigger("Transition");
        SoundManager.PlaySound("flyAway");
    }

    public void RespawnAtCheckpoint() {
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.enabled = true;
        health = numOfHearts;
        windMagic = 0;
        animator.SetBool("IsDead", false);
        //gameObject.transform.position = checkpoint.lastCheckpoint;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
