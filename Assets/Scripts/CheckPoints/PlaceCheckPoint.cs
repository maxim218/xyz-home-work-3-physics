using UnityEngine;

namespace CheckPoints
{
    public class PlaceCheckPoint : MonoBehaviour
    {
        [SerializeField] private CheckPointsController checkPointsController = null;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (gameObject.activeSelf)
            {
                if (checkPointsController.GetHero() == col.gameObject)
                {
                    Vector3 position = transform.position;
                    float x = Mathf.Round(position.x);
                    float y = Mathf.Round(position.y);
                    gameObject.SetActive(false);
                    checkPointsController.SaveGameProgress(x, y);
                }
            }
        }
    }
}
