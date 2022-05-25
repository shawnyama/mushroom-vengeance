using UnityEngine;

public class Checkpoint : MonoBehaviour {
    private static Checkpoint checkpoint;
    public Vector2 lastCheckpoint;

   void Awake() {
        if (checkpoint == null) {
            checkpoint = this;
            DontDestroyOnLoad(checkpoint);
        } else {
            Destroy(gameObject);
        }
    }
}
