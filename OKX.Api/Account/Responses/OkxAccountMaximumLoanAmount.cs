﻿namespace OKX.Api.Account;

/// <summary>
/// OkxMaximumLoanAmount
/// </summary>
public class OkxAccountMaximumLoanAmount
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("mgnCcy")]
    public string MarginCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Max loan
    /// </summary>
    [JsonProperty("maxLoan")]
    public decimal MaximumLoan { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide OrderSide { get; set; }
}