using System;
using UnityEngine;

[Serializable]
public class OneWord
{
    public string key = string.Empty;
    public string translate = string.Empty;
}

[Serializable]
public class WordsStorage
{
    public OneWord [] contentArray = null;
}

public static class LocaleManager
{
    private static string _localeType = string.Empty;

    private static void SetLocaleType(string type)
    {
        _localeType = type;
    }

    private static string GetAssetTextFromFile() {
        try {
            bool isRusFlag = ("RUS" == _localeType);
            string path = isRusFlag ? "russian" : "english";
            TextAsset asset = Resources.Load<TextAsset>(path);
            return asset.text.Trim();
        } catch {
            return string.Empty;
        }
    }

    private static WordsStorage CreateWordsStorage(string jsonText)
    {
        WordsStorage wordsStorage = JsonUtility.FromJson<WordsStorage>(jsonText);
        return wordsStorage;
    }

    public static WordsStorage GetLocaleStorage(string type)
    {
        SetLocaleType(type);
        string jsonText = GetAssetTextFromFile();
        WordsStorage wordsStorage = CreateWordsStorage(jsonText);
        return wordsStorage;
    }
    
    public static void RenderWordsStorage(WordsStorage wordsStorage)
    {
        foreach (OneWord oneWord in wordsStorage.contentArray)
        {
            const string middleSeparator = " : ";
            string message = oneWord.key + middleSeparator + oneWord.translate;
            Debug.Log(message);
        }
    }

    public static string TranslateByKey(WordsStorage wordsStorage, string key) {
        foreach (OneWord oneWord in wordsStorage.contentArray) {
            bool condition = ( oneWord.key.Trim() == key.Trim() );
            if (condition) return oneWord.translate;
        }
        return string.Empty;
    }
}
