using UnityEngine;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class ATTController : MonoBehaviour
{
    // We need this for App Tracking Transparency which is a new requirement as of iOS 14
    void Start()
    {
        #if UNITY_IOS
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            { 
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
        #endif
    }
}