using UnityEngine;

public class CameraPlayer : MonoBehaviour {
    public Transform target;
    public Vector3 offset;

    [Range(1, 10)]
    public float smoothFactor;

    private void FixedUpdate() {
        Follow();
    }

    void Follow() {
        if (target.position.y > -10) { // Avoids going beyond the pitfall
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
        
    }

}
