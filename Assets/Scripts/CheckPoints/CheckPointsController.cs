using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CheckPoints
{
    public class CheckPointsController : MonoBehaviour
    {
        [SerializeField] private GameObject hero = null;

        [SerializeField] private Text labelTextComponent = null; 
        
        [SerializeField] private BonusControl[] bonusesArray = null;

        [SerializeField] private PlaceCheckPoint[] placesArray = null;

        [SerializeField] private bool[] activeBonusesArray = null;

        [SerializeField] private bool[] activePlacesArray = null;
        
        private int _scoreValue = 0;

        private void RenderScore()
        {
            labelTextComponent.text = "" + _scoreValue;
        }

        private void Start()
        {
            string storedInfo = PlayerPrefs.GetString("CHECK_POINT_DATA", string.Empty);
            Debug.Log("Stored Info: " + storedInfo);

            if (string.IsNullOrEmpty(storedInfo))
            {
                _scoreValue = 0;
                RenderScore();
            }
            else
            {
                const char separator = '_';
                string[] partsArray = storedInfo.Split(separator);
                
                string bonuses = partsArray[0].Trim();
                string places = partsArray[1].Trim();

                float x = float.Parse(partsArray[2]);
                float y = float.Parse(partsArray[3]);

                Debug.Log("bonuses: " + bonuses);
                Debug.Log("places: " + places);
                
                Debug.Log("x: " + x);
                Debug.Log("y: " + y);

                InitGameObjectsAndFlagsArrays(bonuses, bonusesArray, activeBonusesArray);
                InitGameObjectsAndFlagsArrays(places, placesArray, activePlacesArray);

                const float z = -2;
                hero.transform.position = new Vector3(x, y, z);
                
                _scoreValue = 0;
                foreach(char c in bonuses)
                {
                    if ('N' == c) _scoreValue++;
                }
                RenderScore();
            }
        }

        [ContextMenu("Delete Store Action")]
        public void DeleteStore()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        
        private static void InitGameObjectsAndFlagsArrays(string content, IReadOnlyList<Component> componentsArr, IList<bool> flagsArr)
        {
            for (int k = 0; k < content.Length; k++)
            {
                char c = content[k];
                bool activeBool = ('Y' == c);
                componentsArr[k].gameObject.SetActive(activeBool);
                flagsArr[k] = activeBool;
            }
        }
        
        public void IncScore()
        {
            _scoreValue += 1;
            RenderScore();
        }
        
        public GameObject GetHero()
        {
            return hero;
        }
        
        public void SaveGameProgress(float x, float y)
        {
            for (int k = 0; k < bonusesArray.Length; k++)
                activeBonusesArray[k] = bonusesArray[k].gameObject.activeSelf;

            for (int k = 0; k < placesArray.Length; k++)
                activePlacesArray[k] = placesArray[k].gameObject.activeSelf;

            string bonuses = GetStatesActiveString(activeBonusesArray);
            string places = GetStatesActiveString(activePlacesArray);

            string saveString = bonuses + "_" + places + "_" + x + "_" + y;
            Debug.Log(saveString);
            
            PlayerPrefs.SetString("CHECK_POINT_DATA", saveString);
            PlayerPrefs.Save();
        }

        private static string GetStatesActiveString(bool [] arr)
        {
            string content = "";
            foreach (bool element in arr) content += GetFlagChar(element);
            return content.Trim();
        }
        
        private static char GetFlagChar(bool conditionActiveFlag)
        {
            char result = conditionActiveFlag ? 'Y' : 'N';
            return result;
        }
    }
}
