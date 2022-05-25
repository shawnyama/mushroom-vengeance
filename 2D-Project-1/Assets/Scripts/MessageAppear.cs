using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageAppear : MonoBehaviour {
    private Image messageBox;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool useAsCheckpoint;

    private Checkpoint checkpoint;

    void Awake() {
        if (messageBox == null)
            messageBox = GameObject.FindGameObjectWithTag("Message").GetComponent<Image>();
    }

    void Start() {
        messageBox = GameObject.FindGameObjectWithTag("Message").GetComponent<Image>();
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();
        messageBox.enabled = false;
        text.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && messageBox != null && useAsCheckpoint) {
           checkpoint.lastCheckpoint = transform.position;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player") && messageBox != null) {
            messageBox.enabled = true;
            text.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && messageBox != null) {
            messageBox.enabled = false;
            text.enabled = false;
        }
    }

}
