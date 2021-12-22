using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsSender : MonoBehaviour
{
    [SerializeField] private string screenNameStats = string.Empty;
    
    private void Start()
    {
        Debug.Log("Screen visit: " + screenNameStats);
        AnalyticsEvent.ScreenVisit(screenNameStats);
    }
}
