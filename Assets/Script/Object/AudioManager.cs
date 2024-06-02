using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioClip MusicClip;
    private AudioSource _audioSource;
    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource = null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
    public void PlayMusic()
    {
        _audioSource.Play();
    }
    public void PauseMusic()
    {
        _audioSource.Pause();
    }
    public void StopMusic(){
        if(MusicClip == null || _audioSource == null )return;
        _audioSource.Stop();
    }
    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip,pos);
    }

}
