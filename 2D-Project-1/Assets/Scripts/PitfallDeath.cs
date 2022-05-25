using UnityEngine;

public class PitfallDeath : MonoBehaviour {

    [SerializeField] Transform checkpoint;

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.transform.CompareTag("Player")) {
            col.gameObject.GetComponent<Health>().DeathFadeEvent();
        }
    }
}
