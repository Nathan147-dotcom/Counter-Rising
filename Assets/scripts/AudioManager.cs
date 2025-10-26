using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Source")]
    public AudioClip background;
    public AudioClip walk;
    public AudioClip shoot;
    public AudioClip lazerShoot;
    public AudioClip enemyDeath;

    private void Start(){
        musicSource.clip = background;
        musicSource.Play();
    }
}
