// Guids.cs
// MUST match guids.h
using System;

namespace NativeViewerPackage10
{
    static class GuidList
    {
        public const string guidNativeViewerPackagePkgString = "de71cb4d-8e69-49c9-a4f5-541751816942";
        public const string guidNativeViewerPackageCmdSetString = "c2c4e0a6-b288-45a5-8519-6013024a1ff7";

        public static readonly Guid guidNativeViewerPackageCmdSet = new Guid(guidNativeViewerPackageCmdSetString);
    };
}