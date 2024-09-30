﻿namespace OKX.Api.Public;

/// <summary>
/// OkxOptionTrade
/// </summary>
public class OkxPublicOptionTradeByInstrumentFamily
{
    /// <summary>
    /// 24h trading volume, with a unit of contract.
    /// </summary>
    [JsonProperty("vol24h")]
    public decimal Volume24H { get; set; }

    /// <summary>
    /// Option type, C: Call P: Put
    /// </summary>
    [JsonProperty("optType"), JsonConverter(typeof(OkxOptionTypeConverter))]
    public OkxOptionType OptionType { get; set; }

    /// <summary>
    /// The list trade data
    /// </summary>
    [JsonProperty("tradeInfo")]
    public List<OkxPublicOptionTradeInfo> TradeInfo { get; set; } = [];
}