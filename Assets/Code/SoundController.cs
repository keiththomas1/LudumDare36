using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip gunShotSound;
    public AudioClip gunHitSound;
    public AudioClip gunMissSound;
    public AudioClip pickupSound;
    public AudioClip glassBreakSound;
    
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();

        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void PlayGunShotSound()
    {
        source.PlayOneShot(gunShotSound, .8f);
    }
    public void PlayGunHitSound()
    {
        source.PlayOneShot(gunHitSound, .8f);
    }
    public void PlayGunMissSound()
    {
        source.PlayOneShot(gunMissSound, .8f);
    }
    public void PlayPickupSound()
    {
        source.PlayOneShot(pickupSound, .8f);
    }
    public void PlayGlassBreak()
    {
        source.PlayOneShot(glassBreakSound, 1.0f);
    }
}

