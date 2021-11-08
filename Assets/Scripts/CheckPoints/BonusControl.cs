using UnityEngine;

namespace CheckPoints
{
    public class BonusControl : MonoBehaviour
    {
        [SerializeField] private CheckPointsController checkPointsController = null;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (gameObject.activeSelf)
            {
                if (checkPointsController.GetHero() == col.gameObject)
                {
                    checkPointsController.IncScore();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
