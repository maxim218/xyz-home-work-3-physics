using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeSound {
    Music_Sound,
    Sfx_Sound
}

[Serializable] public struct ElementOfList {
    public GameObject soundGameObject;
    public TypeSound typeSound;
}

public class GameSoundInit : MonoBehaviour {
    private static void SetDictionaryContent(Dictionary<string, int> dictionary, StoreStructure storeStructure) {
        foreach (ElementStructure element in storeStructure.Arr) {
            string key = element.key;
            int value = element.value;
            dictionary[key] = value;
        }
    }
    
    private void Start() {
        StoreStructure storeStructure = LocalStorageControl.GetStoreStructure();
        Dictionary<string, int> dictionary = new Dictionary <string, int> ();
        SetDictionaryContent(dictionary, storeStructure);
        LocalStorageControl.RenderDictionary(dictionary);

        const int defaultInt = 50;
        
        foreach (ElementOfList elementOfList in arrayElementsList) {
            int volume = GetMusicVolume(elementOfList, dictionary, defaultInt);
            elementOfList.soundGameObject.GetComponent<SoundControl>().VolumeSet(volume);
        }
    }

    [SerializeField] private ElementOfList [] arrayElementsList = null;
    
    private static int GetByKey(Dictionary<string, int> dictionary, string key, int defaultInt) {
        if (dictionary == null) {
            return defaultInt;
        }
        try {
            return dictionary[key];
        } catch {
            return defaultInt;
        }
    }

    private static int GetMusicVolume(ElementOfList element, Dictionary<string, int> dictionary, int defaultInt) {
        switch (element.typeSound) {
            case TypeSound.Music_Sound: {
                const string key = "musicVolume";
                return GetByKey(dictionary, key, defaultInt);
            }
            case TypeSound.Sfx_Sound: {
                const string key = "sfxVolume";
                return GetByKey(dictionary, key, defaultInt);
            }
            default:
                return defaultInt;
        }
    }
}
