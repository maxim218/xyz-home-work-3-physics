using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour {
    private AudioSource _audioSource = null;
    
    [SerializeField] private AudioClip clip = null;
    [SerializeField] private GameObject cameraObj = null;
    
    [ContextMenu("Sound --- Play --- Circle")]
    public void SoundPlayCircle() {
        if (!clip) return;
        _audioSource.clip = clip;
        _audioSource.loop = true;
        _audioSource.Play();
    }
    
    [ContextMenu("Sound --- Play --- Once")]
    public void SoundPlayOnce() {
        if (!clip) return;
        _audioSource.clip = clip;
        _audioSource.PlayOneShot(clip);
    }

    [ContextMenu("Stop --- Sound")]
    public void StopSound() {
        _audioSource.Stop();
    }

    private enum CommandType {
        Do_Nothing,
        Sound_Play_Circle,
        Sound_Play_Once,
    };

    [SerializeField] private CommandType commandType = CommandType.Do_Nothing;
    
    private void Start() {
        // init
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = clip;
        
        // save link to static class
        LinkerSounds.AddToDictionary(nameLink, this);

        // call command
        switch (commandType) {
            case CommandType.Sound_Play_Circle: 
                SoundPlayCircle(); 
                break;
            case CommandType.Sound_Play_Once: 
                SoundPlayOnce(); 
                break;
            case CommandType.Do_Nothing:
                break;
            default:
                return;
        }
    }

    [SerializeField] private string nameLink = string.Empty;
    
    private void LateUpdate() {
        if (cameraObj) transform.position = cameraObj.transform.position;
    }

    public void VolumeSet(int value) {
        float volume = value / 100f;
        _audioSource.volume = volume;
    }

    [SerializeField] private string type = string.Empty;

    public string TypeGet() {
        return type;
    }
}
