using UnityEngine;

public class DeathFade : MonoBehaviour {
    public Health healthScript;

    public void RespawnEvent() {
        healthScript.RespawnAtCheckpoint();
    }
    
}
