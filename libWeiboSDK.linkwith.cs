using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libWeiboSDK.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true)]
