// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Xbox.Services.Leaderboard
{
    using global::System;
    using global::System.Runtime.InteropServices;
    using Newtonsoft.Json;

    public class LeaderboardColumn
    {
        public LeaderboardStatType StatisticType { get; set; }
        
        public string StatisticName { get; set; }

        internal LeaderboardColumn(IntPtr leaderboardColumnPtr)
        {
            LEADERBOARD_COLUMN cColumn = MarshalingHelpers.PtrToStructure<LEADERBOARD_COLUMN>(leaderboardColumnPtr);

            StatisticType = cColumn.StatType;
            StatisticName = MarshalingHelpers.Utf8ToString(cColumn.StatName);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct LEADERBOARD_COLUMN
        {
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr StatName;

            [MarshalAs(UnmanagedType.I4)]
            public LeaderboardStatType StatType;
        }

        // Used for mock services
        internal LeaderboardColumn(LeaderboardStatType type, string name)
        {
            StatisticType = type;
            StatisticName = name;
        }
    }
}