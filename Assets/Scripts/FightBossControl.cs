using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum BossState
{
    EmptyNotSet,
    IdleState,
    FirstState,
    SecondState
}

public class FightBossControl : MonoBehaviour
{
    [SerializeField] private WaterBossControl waterBossControlComponent = null;
    
    [SerializeField] private Animator animatorComponent = null;
    [SerializeField] private BossState bossState = BossState.EmptyNotSet;
    [SerializeField] private bool needChangeState = false;
    [SerializeField] private int lives = 3;
    [SerializeField] private Text victoryLabel = null;

    private void WaterBegin()
    {
        if (waterBossControlComponent != null)
            waterBossControlComponent.WaterAttack();
    }
    
    private void SetIdleAnim()
    {
        bossState = BossState.IdleState;
        const int zeroTime = 0;
        const string stateName = "IdleAnim";
        if (animatorComponent != null) 
            animatorComponent.CrossFade(stateName, zeroTime);
    }

    private void SetPatrulFirstAnim()
    {
        bossState = BossState.FirstState;
        const int zeroTime = 0;
        const string stateName = "PatrulFirstAnim";
        if (animatorComponent != null) 
            animatorComponent.CrossFade(stateName, zeroTime);
    }

    private void SetPatrulSecondAnim()
    {
        bossState = BossState.SecondState;
        const int zeroTime = 0;
        const string stateName = "PatrulSecondAnim";
        if (animatorComponent != null) 
            animatorComponent.CrossFade(stateName, zeroTime);
    }

    private void Start()
    {
        SetIdleAnim();
    }

    private void ControlNextState()
    {
        if(needChangeState)
        {
            needChangeState = false;
            switch (bossState)
            {
                case BossState.IdleState:
                    SetPatrulFirstAnim();
                    break;
                case BossState.FirstState:
                    SetPatrulSecondAnim();
                    break;
                default:
                    break;
            }
        }
    }

    public void NeedChangeYes()
    {
        needChangeState = true;
        WaterBegin();
        lives -= 1;
        if (lives <= 0)
        {
            GoMenuScript.MainMenuMove();
            victoryLabel.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
