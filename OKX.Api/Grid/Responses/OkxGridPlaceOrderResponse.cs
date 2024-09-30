﻿namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Order Response
/// </summary>
public class OkxGridPlaceOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Algo Client Order Id
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = string.Empty;
}