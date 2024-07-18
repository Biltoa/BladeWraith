using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GACustomEvents : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
    }

    public void levelStart(string levelname, int levelindex = 0)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, levelname);
    }

    public void levelFail(string levelname, int value = 0)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, levelname, value);
    }

    public void levelSuccess(string levelname, int value = 0)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, levelname, value);
    }
}
