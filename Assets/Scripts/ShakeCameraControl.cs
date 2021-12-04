using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class ShakeCameraControl : MonoBehaviour
{
    [SerializeField] private float shakeWaitTime = 0;
    
    [SerializeField] private CinemachineVirtualCamera cameraComponent = null;
    [SerializeField] private CinemachineBasicMultiChannelPerlin noiseComponent = null;

    [SerializeField] private UnityEvent beforeShakeEvent = null;
    [SerializeField] private UnityEvent afterShakeEvent = null;

    private const float Frequency = 2.1f;
    private const float Amplitude = 1.5f;

    private const int Zero = 0;
    
    private void Start()
    {
        if (cameraComponent)
            noiseComponent = cameraComponent.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void SetParamsOfShake(float frequency, float amplitude)
    {
        if(noiseComponent)
        {
            noiseComponent.m_FrequencyGain = frequency;
            noiseComponent.m_AmplitudeGain = amplitude;
        }
    }

    private void BeginShake() => SetParamsOfShake(Frequency, Amplitude);

    private void FinishShake() => SetParamsOfShake(Zero, Zero);

    private IEnumerator AsyncShakeOperation()
    {
        beforeShakeEvent.Invoke();
        BeginShake();
        yield return new WaitForSeconds(shakeWaitTime);
        FinishShake();
        afterShakeEvent.Invoke();
    }
    
    private void ShakeOnce()
    {
        StartCoroutine( AsyncShakeOperation() );
    }

    public static void CameraShake()
    {
        ShakeCameraControl script = FindObjectOfType<ShakeCameraControl>();
        if (script != null) 
            script.ShakeOnce();
    }
}
