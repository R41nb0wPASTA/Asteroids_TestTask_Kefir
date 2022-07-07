using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource smallExplosionSfx;
    public AudioSource bigExplosionSfx;
    public AudioSource fireSfx;
    public AudioSource laserSfx;
    public AudioSource thrustSfx;
    public AudioSource ufoSfx;

    public void PlaySmallExsplosionSfx()
    {
        smallExplosionSfx.Play();
    }
    
    public void PlayBigExsplosionSfx()
    {
        bigExplosionSfx.Play();
    }
    
    public void PlayFireSfx()
    {
        fireSfx.Play();
    }
    
    public void PlayLaserSfx()
    {
        laserSfx.Play();
    }
    
    public void PlayThrustSfx()
    {
        thrustSfx.Play();
    }
    
    public void PlayUfoSfx()
    {
        ufoSfx.Play();
    }

    public void StopThrustSfx()
    {
        thrustSfx.Stop();
    }

    public void StopUfoSfx()
    {
        ufoSfx.Stop();
    }
}
