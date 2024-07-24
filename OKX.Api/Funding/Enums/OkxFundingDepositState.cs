﻿namespace OKX.Api.Funding.Enums;

/// <summary>
/// OKX Deposit State
/// </summary>
public enum OkxFundingDepositState
{
    /// <summary>
    /// WaitingForConfirmation
    /// </summary>
    WaitingForConfirmation,

    /// <summary>
    /// Credited
    /// </summary>
    Credited,

    /// <summary>
    /// Successful
    /// </summary>
    Successful,

    /// <summary>
    /// Pending
    /// </summary>
    Pending,
}