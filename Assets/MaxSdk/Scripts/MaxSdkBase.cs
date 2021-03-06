using System;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public abstract class MaxSdkBase
{
    // Shared Properties
    protected static readonly MaxUserSegment SharedUserSegment = new MaxUserSegment();

    /// <summary>
    /// This enum represents whether or not the consent dialog should be shown for this user.
    /// The state where no such determination could be made is represented by <see cref="ConsentDialogState.Unknown"/>.
    ///
    /// NOTE: This version of the iOS consent flow has been deprecated and is only available on UNITY_ANDROID as of MAX Unity Plugin v4.0.0 + iOS SDK v7.0.0, please refer to our documentation for enabling the new consent flow.
    /// </summary>
    public enum ConsentDialogState
    {
        /// <summary>
        /// The consent dialog state could not be determined. This is likely due to SDK failing to initialize.
        /// </summary>
        Unknown,

        /// <summary>
        /// This user should be shown a consent dialog.
        /// </summary>
        Applies,

        /// <summary>
        /// This user should not be shown a consent dialog.
        /// </summary>
        DoesNotApply
    }

#if UNITY_EDITOR || UNITY_IPHONE || UNITY_IOS
    /// <summary>
    /// App tracking status values. Primarily used in conjunction with iOS14's AppTrackingTransparency.framework.
    /// </summary>
    public enum AppTrackingStatus
    {
        /// <summary>
        /// Device is on < iOS14, AppTrackingTransparency.framework is not available.
        /// </summary>
        Unavailable,

        /// <summary>
        /// The value returned if a user has not yet received an authorization request to authorize access to app-related data that can be used for tracking the user or the device.
        /// </summary>
        NotDetermined,

        /// <summary>
        /// The value returned if authorization to access app-related data that can be used for tracking the user or the device is restricted.
        /// </summary>
        Restricted,

        /// <summary>
        /// The value returned if the user denies authorization to access app-related data that can be used for tracking the user or the device.
        /// </summary>
        Denied,

        /// <summary>
        /// The value returned if the user authorizes access to app-related data that can be used for tracking the user or the device.
        /// </summary>
        Authorized,
    }
#endif

    public enum AdViewPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        Centered,
        CenterLeft,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public enum BannerPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        Centered,
        CenterLeft,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public class SdkConfiguration
    {
        /// <summary>
        /// Get the consent dialog state for this user. If no such determination could be made, `ALConsentDialogStateUnknown` will be returned.
        /// </summary>
        public ConsentDialogState ConsentDialogState;

        /// <summary>
        /// Get the country code for this user.
        /// </summary>
        public string CountryCode;

#if UNITY_EDITOR || UNITY_IPHONE || UNITY_IOS
        /// <summary>
        /// App tracking status values. Primarily used in conjunction with iOS14's AppTrackingTransparency.framework.
        /// </summary>
        public AppTrackingStatus AppTrackingStatus;
#endif

        public static SdkConfiguration Create(IDictionary<string, string> eventProps)
        {
            var sdkConfiguration = new SdkConfiguration();

            string countryCode = eventProps.TryGetValue("countryCode", out countryCode) ? countryCode : "";
            sdkConfiguration.CountryCode = countryCode;

            string consentDialogStateStr = eventProps.TryGetValue("consentDialogState", out consentDialogStateStr) ? consentDialogStateStr : "";
            if ("1".Equals(consentDialogStateStr))
            {
                sdkConfiguration.ConsentDialogState = MaxSdkBase.ConsentDialogState.Applies;
            }
            else if ("2".Equals(consentDialogStateStr))
            {
                sdkConfiguration.ConsentDialogState = MaxSdkBase.ConsentDialogState.DoesNotApply;
            }
            else
            {
                sdkConfiguration.ConsentDialogState = MaxSdkBase.ConsentDialogState.Unknown;
            }

#if UNITY_IPHONE || UNITY_IOS
            string appTrackingStatusStr = eventProps.TryGetValue("appTrackingStatus", out appTrackingStatusStr) ? appTrackingStatusStr : "-1";
            if ("-1".Equals(appTrackingStatusStr))
            {
                sdkConfiguration.AppTrackingStatus = MaxSdkBase.AppTrackingStatus.Unavailable;
            }
            else if ("0".Equals(appTrackingStatusStr))
            {
                sdkConfiguration.AppTrackingStatus = MaxSdkBase.AppTrackingStatus.NotDetermined;
            }
            else if ("1".Equals(appTrackingStatusStr))
            {
                sdkConfiguration.AppTrackingStatus = MaxSdkBase.AppTrackingStatus.Restricted;
            }
            else if ("2".Equals(appTrackingStatusStr))
            {
                sdkConfiguration.AppTrackingStatus = MaxSdkBase.AppTrackingStatus.Denied;
            }
            else // "3" is authorized
            {
                sdkConfiguration.AppTrackingStatus = MaxSdkBase.AppTrackingStatus.Authorized;
            }

#endif

            return sdkConfiguration;
        }
    }

    public struct Reward
    {
        public string Label;
        public int Amount;

        public override string ToString()
        {
            return "Reward: " + Amount + " " + Label;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Label) && Amount > 0;
        }
    }

    public class AdInfo
    {
        public string AdUnitIdentifier { get; private set; }
        public string NetworkName { get; private set; }
        public string Placement { get; private set; }
        public string CreativeIdentifier { get; private set; }
        public double Revenue { get; private set; }

        public AdInfo(string adInfoString)
        {
            string adUnitIdentifier;
            string networkName;
            string creativeIdentifier;
            string placement;
            string revenue;

            // NOTE: Unity Editor creates empty string
            var adInfoObject = MaxSdkUtils.PropsStringToDict(adInfoString);
            AdUnitIdentifier = adInfoObject.TryGetValue("adUnitId", out adUnitIdentifier) ? adUnitIdentifier : "";
            NetworkName = adInfoObject.TryGetValue("networkName", out networkName) ? networkName : "";
            CreativeIdentifier = adInfoObject.TryGetValue("creativeId", out creativeIdentifier) ? creativeIdentifier : "";
            Placement = adInfoObject.TryGetValue("placement", out placement) ? placement : "";

            if (adInfoObject.TryGetValue("revenue", out revenue))
            {
                try
                {
                    // InvariantCulture guarantees the decimal is used for the separator even in regions that use commas as the separator
                    Revenue = double.Parse(revenue, NumberStyles.Any, CultureInfo.InvariantCulture);
                }
                catch (Exception exception)
                {
                    MaxSdkLogger.E("Failed to parse double (" + revenue + ") with exception: " + exception);
                    Revenue = -1;
                }
            }
            else
            {
                Revenue = -1;
            }
        }

        public override string ToString()
        {
            return "[AdInfo adUnitIdentifier: " + AdUnitIdentifier +
                   ", networkName: " + NetworkName +
                   ", creativeIdentifier: " + CreativeIdentifier +
                   ", placement: " + Placement +
                   ", revenue: " + Revenue + "]";
        }
    }

    public class MediatedNetworkInfo
    {
        public string Name { get; private set; }
        public string AdapterClassName { get; private set; }
        public string AdapterVersion { get; private set; }
        public string SdkVersion { get; private set; }

        public MediatedNetworkInfo(string networkInfoString)
        {
            string name;
            string adapterClassName;
            string adapterVersion;
            string sdkVersion;

            // NOTE: Unity Editor creates empty string
            var mediatedNetworkObject = MaxSdkUtils.PropsStringToDict(networkInfoString);
            Name = mediatedNetworkObject.TryGetValue("name", out name) ? name : "";
            AdapterClassName = mediatedNetworkObject.TryGetValue("adapterClassName", out adapterClassName) ? adapterClassName : "";
            AdapterVersion = mediatedNetworkObject.TryGetValue("adapterVersion", out adapterVersion) ? adapterVersion : "";
            SdkVersion = mediatedNetworkObject.TryGetValue("sdkVersion", out sdkVersion) ? sdkVersion : "";
        }

        public override string ToString()
        {
            return "[MediatedNetworkInfo name: " + Name +
                   ", adapterClassName: " + AdapterClassName +
                   ", adapterVersion: " + AdapterVersion +
                   ", sdkVersion: " + SdkVersion + "]";
        }
    }

    protected static void ValidateAdUnitIdentifier(string adUnitIdentifier, string debugPurpose)
    {
        if (string.IsNullOrEmpty(adUnitIdentifier))
        {
            MaxSdkLogger.UserError("No MAX Ads Ad Unit ID specified for: " + debugPurpose);
        }
    }

    // Allocate the MaxSdkCallbacks singleton, which receives all callback events from the native SDKs.
    protected static void InitCallbacks()
    {
        var type = typeof(MaxSdkCallbacks);
        var mgr = new GameObject("MaxSdkCallbacks", type)
            .GetComponent<MaxSdkCallbacks>(); // Its Awake() method sets Instance.
        if (MaxSdkCallbacks.Instance != mgr)
        {
            MaxSdkLogger.UserWarning("It looks like you have the " + type.Name + " on a GameObject in your scene. Please remove the script from your scene.");
        }
    }

    /// <summary>
    /// Generates serialized Unity meta data to be passed to the SDK.
    /// </summary>
    /// <returns>Serialized Unity meta data.</returns>
    protected static string GenerateMetaData()
    {
        var metaData = new Dictionary<string, string>(2);
        metaData.Add("UnityVersion", Application.unityVersion);

        var graphicsMemorySize = SystemInfo.graphicsMemorySize;
        metaData.Add("GraphicsMemorySizeMegabytes", graphicsMemorySize.ToString());

        return MaxSdkUtils.DictToPropsString(metaData);
    }

    /// <summary>
    /// Parses the prop string provided to a <see cref="Rect"/>.
    /// </summary>
    /// <param name="rectPropString">A prop string representing a Rect</param>
    /// <returns>A <see cref="Rect"/> the prop string represents.</returns>
    protected static Rect GetRectFromString(string rectPropString)
    {
        var rectDict = MaxSdkUtils.PropsStringToDict(rectPropString);
        float originX;
        float originY;
        float width;
        float height;
        string output;

        rectDict.TryGetValue("origin_x", out output);
        float.TryParse(output, out originX);

        rectDict.TryGetValue("origin_y", out output);
        float.TryParse(output, out originY);

        rectDict.TryGetValue("width", out output);
        float.TryParse(output, out width);

        rectDict.TryGetValue("height", out output);
        float.TryParse(output, out height);

        return new Rect(originX, originY, width, height);
    }
}

/// <summary>
/// An extension class for <see cref="MaxSdkBase.BannerPosition"/> and <see cref="MaxSdkBase.AdViewPosition"/> enums.
/// </summary>
internal static class AdPositionExtenstion
{
    public static string ToSnakeCaseString(this MaxSdkBase.BannerPosition position)
    {
        if (position == MaxSdkBase.BannerPosition.TopLeft)
        {
            return "top_left";
        }
        else if (position == MaxSdkBase.BannerPosition.TopCenter)
        {
            return "top_center";
        }
        else if (position == MaxSdkBase.BannerPosition.TopRight)
        {
            return "top_right";
        }
        else if (position == MaxSdkBase.BannerPosition.Centered)
        {
            return "centered";
        }
        else if (position == MaxSdkBase.BannerPosition.CenterLeft)
        {
            return "center_left";
        }
        else if (position == MaxSdkBase.BannerPosition.CenterRight)
        {
            return "center_right";
        }
        else if (position == MaxSdkBase.BannerPosition.BottomLeft)
        {
            return "bottom_left";
        }
        else if (position == MaxSdkBase.BannerPosition.BottomCenter)
        {
            return "bottom_center";
        }
        else // position == MaxSdkBase.BannerPosition.BottomRight
        {
            return "bottom_right";
        }
    }

    public static string ToSnakeCaseString(this MaxSdkBase.AdViewPosition position)
    {
        if (position == MaxSdkBase.AdViewPosition.TopLeft)
        {
            return "top_left";
        }
        else if (position == MaxSdkBase.AdViewPosition.TopCenter)
        {
            return "top_center";
        }
        else if (position == MaxSdkBase.AdViewPosition.TopRight)
        {
            return "top_right";
        }
        else if (position == MaxSdkBase.AdViewPosition.Centered)
        {
            return "centered";
        }
        else if (position == MaxSdkBase.AdViewPosition.CenterLeft)
        {
            return "center_left";
        }
        else if (position == MaxSdkBase.AdViewPosition.CenterRight)
        {
            return "center_right";
        }
        else if (position == MaxSdkBase.AdViewPosition.BottomLeft)
        {
            return "bottom_left";
        }
        else if (position == MaxSdkBase.AdViewPosition.BottomCenter)
        {
            return "bottom_center";
        }
        else // position == MaxSdkBase.AdViewPosition.BottomRight
        {
            return "bottom_right";
        }
    }
}
