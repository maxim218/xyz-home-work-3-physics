using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LampHeroControl : MonoBehaviour
{
    [SerializeField] private Light2D lightComponent = null;
    [SerializeField] private bool isLampActive = false;
    [SerializeField] private float energyNumber = 0;

    public void SetEnergyNumber(float value)
    {
        energyNumber = value;
        lightComponent.intensity = energyNumber;
    }
    
    public void ChangeLampState()
    {
        isLampActive = !isLampActive;
        lightComponent.gameObject.SetActive(isLampActive);
    }

    private IEnumerator AsyncCounting()
    {
        while (true)
        {
            const float waitSeconds = 1.0f;
            yield return new WaitForSeconds(waitSeconds);

            if (isLampActive)
            {
                const float deltaEnergy = 0.1f;
                energyNumber -= deltaEnergy;
                if (energyNumber < deltaEnergy)
                    energyNumber = deltaEnergy;
            }

            lightComponent.intensity = energyNumber;
        }
    }

    private void Start()
    {
        StartCoroutine( AsyncCounting() );
    }
}
