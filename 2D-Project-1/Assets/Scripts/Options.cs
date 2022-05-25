using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
