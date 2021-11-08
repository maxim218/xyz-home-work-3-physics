using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CheckPoints
{
    public class EnemyContol : MonoBehaviour
    {
        [SerializeField] private ControlHealth controlHealthComponent = null;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject == controlHealthComponent.gameObject)
            {
                controlHealthComponent.ZeroHealth(); 
                StartCoroutine(RestartLevelAsync());
            }
        }

        private static IEnumerator RestartLevelAsync()
        {
            const float waitBeforeLoading = 1.7f;
            yield return new WaitForSeconds(waitBeforeLoading);
            
            const string sceneName = "CheckPointScene";
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while(!operation.isDone) yield return new WaitForSeconds(1);
        }
    }
}
