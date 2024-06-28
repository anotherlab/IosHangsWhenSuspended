# IosHangsWhenSuspended
With .NET 8.0.300, when the app is running on iOS hangs when the app is suspended and resumed. This is a minimal reproducible example.

This happens when debugging the app from Visual Studio 2022. Running the app normally does not cause the issue.

When the app is suspended, OnSleep is called the first time.  In the console, the following message is displayed every second:
```
2024-06-28 09:53:08.715450-0400 IosHangsWhenSuspended[23749:38857205] App: OnSleep
2024-06-28 09:53:08.999710-0400 IosHangsWhenSuspended[23749:38857205] [Snapshotting] Rendering a view (0x103c82550, UIWindow) that is not in a visible window is not supported (no context).
2024-06-28 09:53:09.655177-0400 IosHangsWhenSuspended[23749:38857205] [Snapshotting] Rendering a view (0x103c82550, UIWindow) that is not in a visible window is not supported (no context).
2024-06-28 09:53:10.628142-0400 IosHangsWhenSuspended[23749:38857205] [Snapshotting] Rendering a view (0x103c82550, UIWindow) that is not in a visible window is not supported (no context).
2024-06-28 09:53:12.042025-0400 IosHangsWhenSuspended[23749:38857205] [Snapshotting] Rendering a view (0x103c82550, UIWindow) that is not in a visible window is not supported (no context).
2024-06-28 09:53:13.380112-0400 IosHangsWhenSuspended[23749:38857205] [Snapshotting] Rendering a view (0x103c82550, UIWindow) that is not in a visible window is not supported (no context).

repeats....
```

Resuming the app brings up a blackscreen and the OnResume event does not fire.  Suspended the app a second time and OnSleep does not trigger, but the following will be in the console log

```
2024-06-28 09:55:10.652296-0400 IosHangsWhenSuspended[23749:38857521] [Common] Snapshot request 0x600000cd70c0 complete with error: <NSError: 0x600000d22c70; domain: FBSSceneSnapshotErrorDomain; code: 4; "an unrelated condition or state was not satisfied"> {
    NSLocalizedDescription = an error occurred during a scene snapshotting operation;
}
2024-06-28 09:55:10.672821-0400 IosHangsWhenSuspended[23749:38857521] [Common] Snapshot request 0x600000d1c570 complete with error: <NSError: 0x600000cd70c0; domain: FBSSceneSnapshotErrorDomain; code: 4; "an unrelated condition or state was not satisfied"> {
    NSLocalizedDescription = an error occurred during a scene snapshotting operation;
}
2024-06-28 09:55:10.963124-0400 IosHangsWhenSuspended[23749:38857205] [Snapshotting] Rendering a view (0x103c82550, UIWindow) that is not in a visible window is not supported (no context).
2024-06-28 09:55:11.776580-0400 IosHangsWhenSuspended[23749:38857205] [Snapshotting] Rendering a view (0x103c82550, UIWindow) that is not in a visible window is not supported (no context).
```

Tested with the iPhone 17 Pro Max simulator for iOS 17.2, Visual Studio 2022 17.11.0 Preview 2.1, and .NET 8.0.300.

Works as expected on Android.

On Windows, when the app is minimized, it calls OnResume repeatedly.

