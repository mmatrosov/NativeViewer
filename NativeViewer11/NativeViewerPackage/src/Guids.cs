// Guids.cs
// MUST match guids.h
using System;

namespace NativeViewerPackage11
{
    static class GuidList
    {
        public const string guidNativeViewerPackagePkgString = "cc119ce6-70e8-4e92-a486-822d5a238a93";
        public const string guidNativeViewerPackageCmdSetString = "aff90b8a-ba93-405a-ab85-813770ea68fa";

        public static readonly Guid guidNativeViewerPackageCmdSet = new Guid(guidNativeViewerPackageCmdSetString);
    };
}