﻿using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Block.Models;

/// <summary>
/// OKX Block Leg Executed
/// </summary>
public class OkxBlockLegExecuted
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// The direction of the leg from the Takers perspective. Valid value can be buy or sell.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide Side { get; set; }
    
    /// <summary>
    /// Size of the leg in contracts or spot.
    /// </summary>
    [JsonProperty("sz")]
    public decimal Size { get; set; }

    /// <summary>
    /// The price the leg executed
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Last traded ID.
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }
}
