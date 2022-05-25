using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPortal : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.CompareTag("Player")) {
            SceneManager.LoadScene(2);
        }
    }
}
