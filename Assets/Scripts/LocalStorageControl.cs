using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable] public struct ElementStructure {
    public string key;
    public int value;
}

[Serializable] public struct StoreStructure {
    public ElementStructure [] Arr;
}

public class LocalStorageControl : MonoBehaviour {
    public static LocalStorageControl GetScriptStorage() {
        Type type = typeof(LocalStorageControl);
        LocalStorageControl script = (LocalStorageControl) FindObjectOfType(type);
        return script;
    }
    
    [SerializeField] private string [] arrayKeys = null;

    private const string storeDictionary = "store_dictionary_xyz";

    private static string GetJsonDefault() {
        const string leftBracket = "{";
        const string rightBracket = "}";
        const char doubleQuote = '"';
        string jsonString = leftBracket + doubleQuote + "Arr" + doubleQuote + ":" + "[]" + rightBracket;
        return jsonString;
    }
    
    public static StoreStructure GetStoreStructure() {
        const string key = storeDictionary;
        string defaultValue = GetJsonDefault();
        string jsonString = PlayerPrefs.GetString(key, defaultValue);
        StoreStructure storeStructure = JsonUtility.FromJson<StoreStructure>(jsonString);
        return storeStructure;
    }
    
    private static void SaveStoreStructure(string jsonString) {
        const string key = storeDictionary;
        PlayerPrefs.SetString(key, jsonString);
        PlayerPrefs.Save();
    }

    [SerializeField] private bool renderFlag = true;
    
    private void RenderStoreStructure(StoreStructure storeStructure) {
        if (!renderFlag) return;
        int length = storeStructure.Arr.Length;
        string message = "Structure array length: " + length;
        Debug.Log(message);
        foreach (ElementStructure element in storeStructure.Arr) {
            string msg = element.key + " : " + element.value;
            Debug.Log(msg);
        }
        const string lineSeparateString = "------------------------------";
        Debug.Log(lineSeparateString);
    }

    [SerializeField] private int defaultInt = 50;
    
    private Dictionary<string, int> CreateDictionary(StoreStructure storeStructure) {
        Dictionary<string, int> dictionary = new Dictionary <string, int> ();
        foreach (string key in arrayKeys) dictionary[key] = defaultInt;
        foreach (ElementStructure element in storeStructure.Arr) dictionary[element.key] = element.value;
        return dictionary;
    }

    public static void RenderDictionary(Dictionary<string, int> dictionary) {
        foreach (KeyValuePair<string, int> keyValue in dictionary) {
            const string middlePart = " : ";
            string msg = keyValue.Key + middlePart + keyValue.Value;
            Debug.Log(msg);
        }
        const string lineSeparateString = "------------------------------";
        Debug.Log(lineSeparateString);
    }
    
    private void Start() {
        StoreStructure storeStructure = GetStoreStructure();
        RenderStoreStructure(storeStructure);

        Dictionary<string, int> dictionary = CreateDictionary(storeStructure);
        RenderDictionary(dictionary);
        _dictionary = dictionary;
    }

    private Dictionary<string, int> _dictionary = null;
    
    public int GetByKey(string key, int defaultInt) {
        if (_dictionary == null) {
            return defaultInt;
        }
        try {
            return _dictionary[key];
        } catch {
            return defaultInt;
        }
    }

    public void SaveToDictionary(string key, int value) {
        if (_dictionary == null) return;
        _dictionary[key] = value;
    }

    public void SaveToDisk() {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (KeyValuePair<string, int> keyValue in _dictionary) {
            string info = FragmentGenerate(keyValue.Key, keyValue.Value);
            stringBuilder.Append(info);
        }
        string concatAll = stringBuilder.ToString();
        
        const string strOld = "}{";
        const string strNew = "},{";
        string replacePart = concatAll.Replace(strOld, strNew);
        
        const char doubleQuote = '"';
        string jsonString = "{" + doubleQuote + "Arr" + doubleQuote + ":" + "[" + replacePart + "]" + "}";

        string message = "Generated JSON - " + jsonString;
        Debug.Log(message);
        SaveStoreStructure(jsonString);
    }

    private static string FragmentGenerate(string key, int value) {
        const char doubleQuote = '"';
        string partKey = doubleQuote + "key" + doubleQuote + ":" + doubleQuote + key + doubleQuote;
        string partValue = doubleQuote + "value" + doubleQuote + ":" + value;
        return "{" + partKey + "," + partValue + "}";
    }

    
    [ContextMenu("Store -- Delete -- All -- Keys")]
    public void StoreDeleteAllKeys() {
        const string msg = "Store -- Delete -- All -- Keys";
        Debug.Log(msg);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
