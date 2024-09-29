﻿namespace OKX.Api.Common;

/// <summary>
/// OKX Self Trade Prevention Mode
/// </summary>
public enum OkxSelfTradePreventionMode
{
    /// <summary>
    /// CancelMaker
    /// </summary>
    CancelMaker,

    /// <summary>
    /// CancelTaker
    /// </summary>
    CancelTaker,

    /// <summary>
    /// CancelBoth
    /// </summary>
    CancelBoth
}