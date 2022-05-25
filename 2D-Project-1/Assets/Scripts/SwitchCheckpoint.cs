using UnityEngine;

public class SwitchCheckpoint : MonoBehaviour {
    private Checkpoint checkpoint;

    void Start() {
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.CompareTag("Player")) {
            checkpoint.lastCheckpoint = new Vector2(8, 54); // Checkpoint for scene 2
            Debug.Log(checkpoint.lastCheckpoint);
        }
    }
}
