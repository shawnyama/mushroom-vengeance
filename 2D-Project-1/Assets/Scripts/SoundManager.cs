using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip brickBreak;
    public static AudioClip cut;
    public static AudioClip error;
    public static AudioClip flyAway;
    public static AudioClip getHealth;
    public static AudioClip hit;
    public static AudioClip magic;
    public static AudioClip takeHit;
    static AudioSource audioSource;

    void Start() {
        brickBreak = Resources.Load<AudioClip>("brickBreak");
        cut = Resources.Load<AudioClip>("cut");
        error = Resources.Load<AudioClip>("error");
        flyAway = Resources.Load<AudioClip>("flyAway");
        getHealth = Resources.Load<AudioClip>("getHealth");
        hit = Resources.Load<AudioClip>("hit");
        magic = Resources.Load<AudioClip>("magic");
        takeHit = Resources.Load<AudioClip>("takeHit");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip) {
        switch (clip) {
            case "brickBreak":
                audioSource.PlayOneShot(brickBreak);
                break;
            case "cut":
                audioSource.PlayOneShot(cut);
                break;
            case "error":
                audioSource.PlayOneShot(error);
                break;
            case "flyAway":
                audioSource.PlayOneShot(flyAway);
                break;
            case "getHealth":
                audioSource.PlayOneShot(getHealth);
                break;
            case "hit":
                audioSource.PlayOneShot(hit);
                break;
            case "magic":
                audioSource.PlayOneShot(magic);
                break;
            case "takeHit":
                audioSource.PlayOneShot(takeHit);
                break;
        }
    }
}
