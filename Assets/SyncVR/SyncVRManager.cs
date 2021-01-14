using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SyncVR.Analytics;
using SyncVR.Services;

public class SyncVRManager : MonoBehaviour
{
    public static SyncVRManager Instance { get; private set; }

    [SerializeField]
    private SyncVRAppKeys appKeys;
    [SerializeField]
    private bool useAnalytics;
    [SerializeField]
    private bool useDevEnvironment;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        AnalyticsService.Instance.SetAnalyticsEnabled(useAnalytics);

        if (appKeys != null)
        {
            if (appKeys.SyncVRAppKey != "")
            {
                FirebaseAuthService.SetUseDevelopmentEnvironment(useDevEnvironment);
                StartCoroutine(FirebaseAuthService.Login(appKeys.SyncVRAppKey));
            }
        }
    }

    public bool GetUseDevEnvironment()
    {
        return useDevEnvironment;
    }
}
