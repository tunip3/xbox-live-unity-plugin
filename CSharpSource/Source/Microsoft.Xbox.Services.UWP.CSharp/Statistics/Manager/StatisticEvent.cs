﻿// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Xbox.Services.Statistics.Manager
{
    using global::System;
    using global::System.Runtime.InteropServices;

    public partial class StatisticEvent
    {
        internal StatisticEvent(IntPtr statEventPtr)
        {
            STATISTIC_EVENT cStatEvent = Marshal.PtrToStructure<STATISTIC_EVENT>(statEventPtr);

            EventType = cStatEvent.EventType;

            try
            {
                EventArgs = new LeaderboardResultEventArgs(cStatEvent.EventArgs);
            }
            catch (Exception)
            {
                // not LeaderboardResultEventArgs
            }

            var manager = (StatisticManager)XboxLive.Instance.StatsManager;
            User = manager.GetUser(cStatEvent.LocalUser);

            ErrorCode = cStatEvent.ErrorCode;
            ErrorMessage = MarshalingHelpers.Utf8ToString(cStatEvent.ErrorMessage);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct STATISTIC_EVENT
        {
            [MarshalAs(UnmanagedType.U4)]
            public StatisticEventType EventType;

            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr EventArgs;

            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr LocalUser;

            [MarshalAs(UnmanagedType.I4)]
            public int ErrorCode;

            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr ErrorMessage;
        }
    }
}
