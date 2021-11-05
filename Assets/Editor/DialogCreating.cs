using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DialogCreating : EditorWindow
    {
        [MenuItem("Window/Dialog Creating")]
        public static void ShowWindow()
        {
            const string title = "Dialog Creating";
            GetWindow<DialogCreating>(title);
        }

        private string _dialogFileName = string.Empty;

        private string _messageForDialog = string.Empty;

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            _dialogFileName = EditorGUILayout.TextField("Dialog File Name", _dialogFileName);
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            _messageForDialog = EditorGUILayout.TextField("Message For Dialog", _messageForDialog);
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Add Message To Dialog"))
            {
                AddMessageToDialog(_dialogFileName, _messageForDialog);
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        private static void AddMessageToDialog(string dialogFileName, string messageForDialog)
        {
            if (string.IsNullOrEmpty(dialogFileName))
                return;

            if (string.IsNullOrEmpty(messageForDialog))
                return;

            string path = Application.dataPath + "/Resources/" + dialogFileName + ".txt";
            Debug.Log("Path: " + path);
            
            StreamWriter f = File.AppendText(path);
            f.WriteLine(messageForDialog);
            f.Close();
        }
    }
}
