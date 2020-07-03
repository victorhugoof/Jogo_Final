using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource audioComeuPeca;
    public AudioSource audioGameOver;

    public void PlayGameOver() {
        audioGameOver.Play();
    }

    public void PlayComeuPeca() {
        audioComeuPeca.Play();
    }
}