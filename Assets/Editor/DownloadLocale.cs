using System.Collections;
using System.IO;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEditor;

namespace Editor
{
    public class DownloadLocale : EditorWindow
    {
        private static string EnglishUrl => "http://195.19.40.118/XYZ/english.json";
        private static string RussianUrl => "http://195.19.40.118/XYZ/russian.json";
        
        private const string BtnEngText = "English";
        private const string BtnRusText = "Russian";
        private const string PrintEnglish = "Print English";
        private const string PrintRussian = "Print Russian";
        
        [MenuItem("Window/Download Locale")]
        public static void ShowWindow()
        {
            const string title = "Download Locale";
            GetWindow<DownloadLocale>(title);
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button(BtnEngText))
                EnglishGet();
            
            if (GUILayout.Button(BtnRusText))
                RussianGet();
            
            GUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button(PrintEnglish))
                PrintEnglishStore();
            
            if (GUILayout.Button(PrintRussian))
                PrintRussianStore();

            GUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        private static void PrintEnglishStore()
        {
            const string eng = "ENG";
            WordsStorage wordsStorage = LocaleManager.GetLocaleStorage(eng);
            LocaleManager.RenderWordsStorage(wordsStorage);
        }

        private static void PrintRussianStore()
        {
            const string rus = "RUS";
            WordsStorage wordsStorage = LocaleManager.GetLocaleStorage(rus);
            LocaleManager.RenderWordsStorage(wordsStorage);
        }

        private void EnglishGet() {
            QueryGet(EnglishUrl, delegate (string answerText) {
                const string fileName = "english.txt";
                ControlLoadedContent(answerText, fileName);
            });
        }

        private void RussianGet() {
            QueryGet(RussianUrl, delegate (string answerText) {
                const string fileName = "russian.txt";
                ControlLoadedContent(answerText, fileName);
            });
        }

        private static void ControlLoadedContent(string answerText, string fileName) {
            if (string.IsNullOrEmpty(answerText)) {
                const string warningMessage = "--- Resources are not loaded ---";
                Debug.LogWarning(warningMessage);
            } else {
                string path = Application.dataPath + "/Resources/" + fileName;
                string content = answerText.Trim();
                SaveToFile(path, content);
            }
        }
        
        private static void SaveToFile(string path, string content)
        {
            StreamWriter f = File.CreateText(path);
            f.WriteLine(content);
            f.Close();
        }
        
        private delegate void Callback(string content);
        
        private string urlString = string.Empty;
        private Callback callback = null;

        private void QueryGet(string urlStringParam, Callback callbackParam) {
            urlString = urlStringParam;
            callback = callbackParam;
            EditorCoroutineUtility.StartCoroutine(QueryGetIEnumerator(), this);
        }

        private IEnumerator QueryGetIEnumerator() {
            WWW www = new WWW(urlString);
            yield return www;
            callback(www.text);
        }
    }
}
