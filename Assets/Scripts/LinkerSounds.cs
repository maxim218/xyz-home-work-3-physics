using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LinkerSounds {
    private static Dictionary <string, SoundControl> _dictionary = null;

    private static void InitDictionary() {
        _dictionary = new Dictionary <string, SoundControl> ();
    }
    
    private static Dictionary <string, SoundControl> GetDictionary() {
        return _dictionary;
    }

    public static void AddToDictionary(string key, SoundControl soundControl) {
        if (GetDictionary() == null) InitDictionary();
        _dictionary[key] = soundControl;
    }

    public static SoundControl DictionaryGetByKey(string key) {
        try {
            return _dictionary[key];
        } catch {
            return null;
        }
    }
}
