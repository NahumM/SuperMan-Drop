## Versions

## 4.1.2
    * Add support for embedding the Swift Standard Library (for iOS < 12.2) if a network requires it.
    * Fix failure to parse doubles when dealing with exponential notation by returning -1.
    * Fix build when targeting platforms other than Android or iOS.
## 4.1.1
    * Add API to retrieve country code, via `MaxSdk.GetSdkConfiguration().CountryCode`.
## 4.1.0
    * Add API to retrieve revenue via `MaxSdk.GetAdInfo(adUnitId).Revenue`.
    * Add API to retrieve SDK configuration via `MaxSdk.GetSdkConfiguration()`.
    * Persist fullscreen ad stubs through scene changes in Unity Editor mode.
## 4.0.1
    * Remove Terms of Service URL from being a consent flow requirement.
    * Update Quality Service Plugin maven repository.
## 4.0.0
    * Initial support for MAX consent flow. Please see our documentation for instructions on enabling it.
    * Add Cross Promo Advertisement API.
    * Add `GetAdValue` API to get arbitrary ad value for a given ad unit identifier with key.
    * Add support for Swift (swift file and build properties) if a network requires it.
    * Deprecate `MaxSdk.UserService.ShowConsentDialog()` for iOS builds. This version of the iOS consent flow has been deprecated as of MAX Unity Plugin v4.0.0 + iOS SDK v7.0.0, please refer to our documentation for enabling the new consent flow.
    * Rename ad info button to creative debugger.
    * Android banners are now shown within the safe area (not shown under notches).
    * Updated the External Dependency Manager version to 1.2.164.
    * Update to support set extra parameters for MRECs.
## 3.2.5
    * Fix hard coded path separator causing issues on Windows.
## 3.2.4
    * Fix iOS build errors on Unity 2019.3+ when embedding framework libraries if Podfile contains `Unity-iPhone` target.
    * Enable verbose logging at run time when enabling the setting via Integration Manager (previously only enabled build time logging).
    * Fix deprecation warnings in Unity versions 2020.1+ for use of obsolete `UnityWebRequest` APIs.
    * Fix `UnauthorizedAccessException` when adding labels to plugin assets on Windows machines.
    * Minor Integration Manager optimizations.
## 3.2.3
    * Add iOS Snap SDK as an embedded framework for adapter versions 1.0.7.2+.
## 3.2.2
    * Fix bug causing extra parameter key and value to be swapped on Android.
    * Fix few issues when moving the `MaxSdk` plugin directory.
    * Fix compile issue with .Net 3.5 (using `File.WriteAllLines(string, IEnumerable<string>)` not available in .Net 3.5).
## 3.2.1
    * Fix crash when ad placement or creative id is `null`.
## 3.2.0
    * Add `AdInfo.Placement` to retrieve the placement of the loaded ad.
    * Add `AdInfo.CreativeId` to retrieve the creative id of the loaded ad.
    * Add ability to move the MaxSdk plugin directory within the Unity project.
    * Add namespace to all Editor scripts.
    * Add memory optimization.
    * Fix first banner of the session not using background color on iOS.
    * Fix edge case where banners are detached from the view controller (e.g. multiple windows) and not show.
## 3.1.18
    * Add `GetBannerLayout` and `GetMRecLayout` APIs to get the ad view absolute position on screen.
## 3.1.17
    * Fix crash when setting the banner width.
## 3.1.16
    * Remove unused import `UnityEditor` from MaxSdkLogger.cs.
    * Update `MaxPostProcessEmbedDynamicLibraries` to set runpathSearchPaths with `GetBuildPropertyForAnyConfig` on UNITY_2018_2_OR_NEWER, else use `$(inherited)`.
## 3.1.15
    * Add support for adaptive banner ad size.
    * Add support for setting banner width.
    * Add support for setting custom banner position as (x,y) coordinates.
    * Add support for linking dynamic libraries (like HyprMX SDK).
    * Add support for logging `MaxSdkCallbacks` subscriptions and removals.
    * Fix `KeyNotFoundException` in `MaxPostProcessBuildiOS` when looking up property list elements.
    * Video player optimizations.
## 3.1.14
    * Add support for `MaxUserService` and the SDK consent dialog.
    * Update to make the plugin compilable when platforms other than Android or iOS are selected.
    * Remove Mintegral from obsolete networks.
## 3.1.13
    * Add dedicated logger class, `MaxSdkLogger`, may be controlled via SDK's verbose logging setting.
    * Add support for disabling attachment of an exception handler in the SDK.
    * Fix banner ad format not being respected in some cases.
    * Fix vertical smart banners spanning too tall in landscape mode.
    * Fix banner safe area background shown on iOS before `MaxSdk.ShowBanner` is called. 
## 3.1.12
    * Add support for user segments.
    * Add minimum compatible adapter versions to the alert when incompatible adapter versions are detected.
## 3.1.11
    * Fix `MaxSdk.SetBannerExtraParameter()` method to allow extra parameters to be set before creating banners.
## 3.1.10
    * Update Integration Manager to show alert when incompatible network adapters are detected.
    * Add HyprMX to networks that require `NSAppTransportSecurity` disabled.
## 3.1.9
    * Fix compile issue with .Net 3.5 (using `string.Join(string, IEnumerable)` not available in .Net 3.5).
## 3.1.8
    * Add native vertical banner support.
    * Update behavior change for method calls to `MaxSdk` before the initialization completes.
## 3.1.7
    * Add `MaxSdk.SetAdInfoButtonEnabled()` API to enable the ad info button.
    * Fix an issue where `MaxSdkUtils.ParseColor()` improperly casts `(int)`.
## 3.1.6
    * Fix banner always being positioned on the top left on Android.
## 3.1.5
    * Fix an issue where Android ad loads are failing with `NoSuchMethodError`.
## 3.1.4
    * Fix an issue where `MaxSdkUtils.ParseColor()` could overflow.
    * Fix crash when accessing certain methods before initializing the SDK.
    * Add Mingetral to list of obsolete networks and automatically delete from project.
## 3.1.3
    * Add extra logs for running Ad Review script.
## 3.1.2
    * Fix Integration Manager overwriting existing `SKAdNetworkItems` entries.
## 3.1.1
    * Add Google and Google Ad Manager to list of networks that require `NSAppTransportSecurity`. 
## 3.1.0
    * Add support for rewarded interstitials.
    * Add support for adding `NSAppTransportSecurity` when a network that requires it is present.
    * Add support for removing `NSAllowsArbitraryLoadsInWebContent` if `NSAllowsArbitraryLoads` is present.
    * Add support for adding `SKAdNetworkItems` to `Info.plist`.
    * Fix an issue where migrating to Unity 2019.3 or newer version causes the Ad Review plugin to be applied to both application and library modules.
## 3.0.2
    * Fix AdReview not being correctly applied to a gradle file with multiple `buildscript` blocks.
## 3.0.1
    * Add optional parameter for initializing the SDK with ad unit ids whose credentials will be used for the session.
    * Show whether or not Gradle Template is enabled in Integration Manager's Environment Details.
## 3.0.0
    * Added Support for AppLovin Quality Service Plugin integration.
## 2.4.16
    * Remove tvOS from supported platforms for iOS Plugin files.
## 2.4.15
    * Added Assembly Definitions to the plugin to optimize compilation time. 
    * Fix vertical banners not correctly aligned with the app display's edge on Android devices with notches.
    * Added ability to keep state of banner show/hide before it is created and will used when it is eventually created.
    * Fix Integration Manager not showing all networks on Unity 2017 or older when Google Adapter is installed.
## 2.4.14
    * Delete obsolete post processing script that could break iOS builds. 
## 2.4.13
    * Fix iOS linkage of some 3rd-party SDKs.
## 2.4.12
    * Fix launch errors introduced in 2.4.11. 
## 2.4.11
    * Add assembly definition `AppLovinMaxSdk` to module to optimize script compilation.
    * Fix `$(inherited)` flag not on 2019.3.3+.
## 2.4.10
    * Fix AppLovin Settings resource file getting reset on fresh builds (like build servers).
    * Fix empty banner (no-fill) clicks passing clicks to random views on screen.
    * Removed restriction on Play Services Ads Identifier version which is no longer needed.
## 2.4.9
    * Deprecate `loadVariables()` related APIs. Please use AppLovin SDK's initialization callback to retrieve variables instead.
    * Fix `.meta` files being updated for each version upgrade.
    * Fix backwards compatibility for Unity Editor stub ads prefabs.
    * Fix Unity Editor stub ads buttons not working if no EventSystem is present.
## 2.4.8
    * Fix Mediation Debugger not showing on iOS.
## 2.4.7
    * Fix bug where stub banners wouldn't display if shown in a different scene than they were created.
    * Provide warning if SDK is not initialized when calling `MaxSdk.ShowMediationDebugger()`.
    * Fix stub ads showing behind in-game UI.
    * Resize stub banners to better reflect actual size.
    * Added informative warnings for when `MaxSdk.ShowMediationDebugger()` is called from the Unity Editor.
    * Refresh assets and log during migration only when files are changed.
## 2.4.6
    * Fix Mediation Debugger crashing due to resource bundle not being linked correctly in Unity versions 2019_3 or newer.
## 2.4.5
    * Fix bug where stub banners in the Unity Editor were disappearing after loading a new scene.
## 2.4.4
    * Fix AppLovin Settings asset being bundled with the package.
    * Fix crash when calling `MaxSdk.SetTestDeviceAdvertisingIdentifiers()` before SDK is initialized.
## 2.4.3
    * Show Unity Environment Details in the Integration Manager.
## 2.4.2
    * Updated the External Dependency Manager version to 1.2.149.
## 2.4.1
    * Updated the External Dependency Manager version to 1.2.147.
    * Fix some assets not bundling with v2.4.0.
## 2.4.0
    * Add support for MREC advertisements. Introduces AdViewPosition to eventually replace BannerPosition. Same API as banners without set background color and set extra parameter options.
    * Add support for enabling and disabling verbose logging.
    * Fix an issue where the area on the sides of banners were not user-interactable on iOS. If the publisher does not set a background color, it means they likely want those areas to be user-interactable.
    * Fix `TargetGuidByName("Unity-iPhone")` deprecation in Unity 2019.3.0+.
    * Add ability to uninstall a mediation adapter plugin.
    * Add ability to enter AdMob credentials through integration manager.
    * Add AppLovinSdk tag prefix to logging.
    * Add support for enabling test devices with `MaxSdk.SetTestDeviceAdvertisingIdentifiers(String[])`.
## 2.3.5
    * Updated the Jar Resolver version to 1.2.143.
## 2.3.4
    * Fix bad push resulting in iOS compilation errors.
## 2.3.3
    * Fix Unity Editor exception related to initializing empty `AdInfo` objects.
## 2.3.2
    * Fix iOS build due to missing symbols.
## 2.3.1
    * Add `MaxSdk.GetAdInfo()` method.
## 2.3.0
    * Add "Do Not Sell" APIs.
    * Add `MaxSdkUtils.GetScreenDensity()` convenience method.
## 2.2.0
    * Add support for passing in extra parameters.
## 2.1.0
    * Deletes Android and iOS adapter CHANGELOGS.
    * Add support for muting ads from certain networks via the new `MaxSdk.setMuted(bool)` API.
## 2.0.0
    * Integration manager UI - easily update adapters and SDKs with a click of a button!
## 1.5.7
    * Add support for setting user id before initializing the plugin.
## 1.5.6
    * Add `MaxSdkUtils.IsTablet()` convenience method.
    * Fix `safeAreaBackground` not in same view hierarchy as the adview.
## 1.5.5
    * Fix ANRs when ad load fails by moving callback off main thread (Android).
    * Stretch banners to the edge of the screen while in landscape.
    * If banner background color is set, fill in the area behind the home indicator bar (iOS).
    * Fix setting banner background color affecting entire screen edge case (Android).
    * Ensure Android SDK dependency on `com.google.android.gms:play-services-ads-identifier` is restricted to `16.0.0` due to later versions depending on AndroidX (Android).
## 1.5.4
    * Fix ProGuard rules.
## 1.5.3
    * Fix setting of mediation provider.
## 1.5.2
    * Fix MAX init script always setting iOS deployment target to 9.0 and log error instead.
    * Set mediation provider to MAX.
## 1.5.1
    * Minor banner optimizations.
    * Stretch banners the width of the screen on iOS 9 devices as well.
## 1.5.0
    * This plugin bundle's Google's automatic dependency manager. It includes an initialization script that automatically removes legacy AppLovin directories as new adapter Unity Packages are imported into your project. Please refer to our docs for more details - https://dash.applovin.com/documentation/mediation/unity/getting-started.
    * Stretch "centered" banner the width of the screen for banners to be fully functional.
    * New API for setting banner background color via `MaxSdk.SetBannerBackgroundColor(...)`
## 1.4.0
    * Support for explicitly loading variables.
    * Support for passing in a Dictionary of String value and parameters for analytics event tracking.
## 1.3.2
    * Fix race condition of publisher setting privacy setting before plugin initializes via `MaxSdk.SetSdkKey(...)`
## 1.3.1
    * Guard iOS PostProcessing script with `#if UNITY_2017_1_OR_NEWER`.
    * Add support for `@executable_path/Frameworks` in Run Search Paths for MoPub's Embedded Binaries.
## 1.3.0
    * Support for showing ad with placements to tie events to.
    * Add support for `*no_compile` files in post-processing script for MoPub's mraid.js.
    * Do not auto-refresh banners that have not yet been shown via `MaxSdk.ShowBanner(string adUnitIdentifier)`.
    * Add support for integrations that set SDK key programmatically and not in AndroidManifest. (Android only)
    * Fix `MaxVariableServiceiOS` compiling for L2CPP.
    * Wrap iOS PostProcessing script in `#if UNITY_IOS` ... `#endif` pre-processor macros.
    * Automatically add `MoPub.framework` to "Embedded Binaries" when exporting to Xcode. (iOS only)
    * Do not re-create VariableService(iOS|Android|UnityEditor) on every `MaxSdk.VariableService`.
## 1.2.0
    * Add support for setting user id.
## 1.1.2
    * Fix empty banners due to no Internet causing touch input issues. (iOS only)
## 1.1.1
    * Fix some 3rd-party ad networks (e.g. Amazon) not sizing correctly on first banner impression. (iOS only)
## 1.1.0
    * Added APIs for retrieving booleans and strings via variable service.
    * Guard iOS code around preprocessor so it does not get compiled on Android via IL2CPP.
## 1.0.1
    * Explicitly check for ad formats for each SDK callback.
    * Fix banner positioning on iOS 10. (iOS only)
    * Do not set initial AdView size if failure to load. (iOS only)
## 1.0.0
    * Initial commit.
