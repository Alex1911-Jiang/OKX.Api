﻿namespace OKX.Api.Algo;

/// <summary>
/// OKX Rest Api Algo Trading Client
/// </summary>
public class OkxAlgoRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    private const string v5TradeOrderAlgo = "api/v5/trade/order-algo";
    private const string v5TradeCancelAlgos = "api/v5/trade/cancel-algos";
    private const string v5TradeAmendAlgos = "api/v5/trade/amend-algos";
    private const string v5TradeCancelAdvanceAlgos = "api/v5/trade/cancel-advance-algos";
    private const string v5TradeOrderAlgoGet = "api/v5/trade/order-algo";
    private const string v5TradeOrdersAlgoPending = "api/v5/trade/orders-algo-pending";
    private const string v5TradeOrdersAlgoHistory = "api/v5/trade/orders-algo-history";

    /// <summary>
    /// The algo order includes trigger order, oco order, conditional order,iceberg order and twap order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="orderSide">Order Side</param>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="size">Size</param>
    /// <param name="quantityType">Quantity Type</param>
    /// <param name="clientOrderId">Client-supplied Algo ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="closeFraction">Fraction of position to be closed when the algo order is triggered.
    /// 
    /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
    /// <param name="tpTriggerPriceType">Take-profit trigger price type</param>
    /// <param name="tpOrderPrice">Take Profit Order Price</param>
    /// <param name="tpOrderKind">Take Profit Order Kind</param>
    /// <param name="slTriggerPrice">Stop Loss Trigger Price</param>
    /// <param name="slTriggerPriceType">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slOrderPrice">Stop Loss Order Price</param>
    /// <param name="cancelOnClosePosition">Whether the TP/SL order placed by the user is associated with the corresponding position of the instrument. If it is associated, the TP/SL order will be cancelled when the position is fully closed; if it is not, the TP/SL order will not be affected when the position is fully closed.
    /// Valid values:
    /// true: Place a TP/SL order associated with the position
    /// false: Place a TP/SL order that is not associated with the position
    /// The default value is false. If true is passed in, users must pass reduceOnly = true as well, indicating that when placing a TP/SL order associated with a position, it must be a reduceOnly order.
    /// Only applicable to Single-currency margin and Multi-currency margin.</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// manual, auto_borrow, auto_repay
    /// The default value is manual</param>
    /// 
    /// <param name="triggerPrice">Trigger Price</param>
    /// <param name="orderPrice">Order Price</param>
    /// <param name="triggerPriceType">Trigger Price Type</param>
    /// <param name="attachedAlgoOrders">Attached SL/TP orders info. Applicable to Spot and futures mode/Multi-currency margin/Portfolio margin</param>
    /// 
    /// <param name="callbackRatio">Callback price ratio , e.g. 0.01</param>
    /// <param name="callbackSpread">Callback price variance</param>
    /// <param name="activePrice">Active price</param>
    /// 
    /// <param name="priceVariance">Price Variance</param>
    /// <param name="priceSpread">Price Spread</param>
    /// <param name="sizeLimit">Size Limit</param>
    /// <param name="priceLimit">Price Limit</param>
    /// 
    /// <param name="timeInterval">Time Interval
    /// Currently the system supports fully closing the position only so the only accepted value is 1.
    /// This is only applicable to FUTURES or SWAP instruments.
    /// This is only applicable if posSide is net.
    /// This is only applicable if reduceOnly is true.
    /// This is only applicable if ordType is conditional or oco.
    /// This is only applicable if the stop loss and take profit order is executed as market order
    /// Either sz or closeFraction is required.</param>
    /// 
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAlgoOrderResponse>> PlaceOrderAsync(

        // Common
        string instrumentId,
        OkxTradeMode tradeMode,
        OkxTradeOrderSide orderSide,
        OkxAlgoOrderType algoOrderType,
        string? currency = null,
        OkxTradePositionSide? positionSide = null,
        decimal? size = null,
        OkxTradeQuantityType? quantityType = null,
        string? clientOrderId = null,
        decimal? closeFraction = null,

        // Take Profit / Stop Loss
        decimal? tpTriggerPrice = null,
        OkxAlgoPriceType? tpTriggerPriceType = null,
        decimal? tpOrderPrice = null,
        OkxAlgoOrderKind? tpOrderKind = null,
        decimal? slTriggerPrice = null,
        OkxAlgoPriceType? slTriggerPriceType = null,
        decimal? slOrderPrice = null,
        bool? cancelOnClosePosition = null,
        bool? reduceOnly = null,

        // Trigger Order
        decimal? triggerPrice = null,
        decimal? orderPrice = null,
        OkxAlgoPriceType? triggerPriceType = null,
        IEnumerable<OkxAlgoAttachedAlgoPlaceRequest>? attachedAlgoOrders = null,

        // Trailing Stop Order
        decimal? callbackRatio = null,
        decimal? callbackSpread = null,
        decimal? activePrice = null,

        // TWAP Order
        OkxPriceVariance? priceVariance = null,
        decimal? priceSpread = null,
        decimal? sizeLimit = null,
        decimal? priceLimit = null,
        long? timeInterval = null,

        // Cancellation Token
        CancellationToken ct = default)
    {
        // Common
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new OkxTradeModeConverter(false)) },
            {"side", JsonConvert.SerializeObject(orderSide, new OkxTradeOrderSideConverter(false)) },
            {"ordType", JsonConvert.SerializeObject(algoOrderType, new OkxAlgoOrderTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new OkxTradePositionSideConverter(false)));
        parameters.AddOptionalParameter("sz", size?.ToOkxString());
        parameters.AddOptionalParameter("tgtCcy", JsonConvert.SerializeObject(quantityType, new OkxTradeQuantityTypeConverter(false)));
        parameters.AddOptionalParameter("algoClOrdId", clientOrderId);
        parameters.AddOptionalParameter("closeFraction", closeFraction?.ToOkxString());

        // Take Profit / Stop Loss
        parameters.AddOptionalParameter("tpTriggerPx", tpTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("tpTriggerPxType", JsonConvert.SerializeObject(tpTriggerPriceType, new OkxAlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("tpOrdPx", tpOrderPrice?.ToOkxString());
        parameters.AddOptionalParameter("tpOrdKind", JsonConvert.SerializeObject(tpOrderKind, new OkxAlgoOrderKindConverter(false)));
        parameters.AddOptionalParameter("slTriggerPx", slTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("slTriggerPxType", JsonConvert.SerializeObject(slTriggerPriceType, new OkxAlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("slOrdPx", slOrderPrice?.ToOkxString());
        parameters.AddOptionalParameter("cxlOnClosePos", cancelOnClosePosition);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        // Trigger Order
        parameters.AddOptionalParameter("triggerPx", triggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("orderPx", orderPrice?.ToOkxString());
        parameters.AddOptionalParameter("triggerPxType", JsonConvert.SerializeObject(triggerPriceType, new OkxAlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("attachAlgoOrds", attachedAlgoOrders);

        // Trailing Stop Order
        parameters.AddOptionalParameter("callbackRatio", callbackRatio?.ToOkxString());
        parameters.AddOptionalParameter("callbackSpread", callbackSpread?.ToOkxString());
        parameters.AddOptionalParameter("activePx", activePrice?.ToOkxString());

        // TWAP Order
        parameters.AddOptionalParameter("pxVar", JsonConvert.SerializeObject(priceVariance, new OkxPriceVarianceConverter(false)));
        parameters.AddOptionalParameter("pxSpread", priceSpread?.ToOkxString());
        parameters.AddOptionalParameter("szLimit", sizeLimit?.ToOkxString());
        parameters.AddOptionalParameter("pxLimit", priceLimit?.ToOkxString());
        parameters.AddOptionalParameter("timeInterval", timeInterval?.ToOkxString());

        // Broker Id
        parameters.AddOptionalParameter("tag", Options.BrokerId);

        // Reequest
        return ProcessOneRequestAsync<OkxAlgoOrderResponse>(GetUri(v5TradeOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="instrumentId">	Instrument ID, e.g. BTC-USDT</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoCancelOrderResponse>>> CancelOrderAsync(long algoOrderId, string instrumentId, CancellationToken ct = default)
        => CancelOrdersAsync([new() { AlgoOrderId = algoOrderId.ToOkxString(), InstrumentId = instrumentId }], ct);
    
    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders to Cancel</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoCancelOrderResponse>>> CancelOrdersAsync(IEnumerable<OkxAlgoCancelOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { Options.RequestBodyParameterKey, orders },
        };

        return ProcessListRequestAsync<OkxAlgoCancelOrderResponse>(GetUri(v5TradeCancelAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Amend unfilled algo orders (Support stop order only, not including Move_order_stop order, Trigger order, Iceberg order, TWAP order, Trailing Stop order).
    /// Only applicable to Futures and Perpetual swap.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="algoOrderId">Algo ID. Either algoId or algoClOrdId is required. If both are passed, algoId will be used.</param>
    /// <param name="algoClientOrderId">Client-supplied Algo ID. Either algoId or algoClOrdId is required. If both are passed, algoId will be used.</param>
    /// <param name="cancelOnFail">Whether the order needs to be automatically canceled when the order amendment fails. Valid options: false or true, the default is false.</param>
    /// <param name="clientRequestId">Client Request ID as assigned by the client for order amendment. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.The response will include the corresponding reqId to help you identify the request if you provide it in the request.</param>
    /// <param name="newSize">New quantity after amendment.</param>
    /// <param name="newTakeProfitTriggerPriceType">Take-profit trigger price type</param>
    /// <param name="newTakeProfitTriggerPrice">Take-profit trigger price. Either the take-profit trigger price or order price is 0, it means that the take-profit is deleted</param>
    /// <param name="newTakeProfitOrderPrice">Take-profit order price. If the price is -1, take-profit will be executed at the market price.</param>
    /// <param name="newStopLossTriggerPriceType">Stop-loss trigger price type</param>
    /// <param name="newStopLossTriggerPrice">Stop-loss trigger price. Either the stop-loss trigger price or order price is 0, it means that the stop-loss is deleted</param>
    /// <param name="newStopLossOrderPrice">Stop-loss order price. If the price is -1, stop-loss will be executed at the market price.</param>
    /// <param name="newTriggerPrice">New trigger price after amendment</param>
    /// <param name="newOrderPrice">New order price after amendment. If the price is -1, the order will be executed at the market price.</param>
    /// <param name="newTriggerPriceType">New trigger price type after amendment</param>
    /// <param name="attachedAlgoOrders">Attached SL/TP orders info. Applicable to Spot and futures mode/Multi-currency margin/Portfolio margin</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAlgoAmendOrderResponse>> AmendOrderAsync(

        // Common
        string instrumentId,
        long? algoOrderId = null,
        string? algoClientOrderId = null,
        bool? cancelOnFail = null,

        string? clientRequestId = null,
        decimal? newSize = null,

        // Take Profit
        OkxAlgoPriceType? newTakeProfitTriggerPriceType = null,
        decimal? newTakeProfitTriggerPrice = null,
        decimal? newTakeProfitOrderPrice = null,

        // Stop Loss
        OkxAlgoPriceType? newStopLossTriggerPriceType = null,
        decimal? newStopLossTriggerPrice = null,
        decimal? newStopLossOrderPrice = null,

        // Trigger Order
        decimal? newTriggerPrice = null,
        decimal? newOrderPrice = null,
        OkxAlgoPriceType? newTriggerPriceType = null,
        IEnumerable<OkxAlgoAttachedAlgoAmendRequest>? attachedAlgoOrders = null,

        // Cancellation Token
        CancellationToken ct = default)
    {
        // Common
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
        };
        parameters.AddOptionalParameter("algoId", algoOrderId);
        parameters.AddOptionalParameter("algoClOrdId", algoClientOrderId);
        parameters.AddOptionalParameter("cxlOnFail", cancelOnFail);
        parameters.AddOptionalParameter("reqId", clientRequestId);
        parameters.AddOptionalParameter("newSz", newSize);

        // Take Profit
        parameters.AddOptionalParameter("newTpTriggerPxType", JsonConvert.SerializeObject(newTakeProfitTriggerPriceType, new OkxAlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("newTpTriggerPx", newTakeProfitTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("newTpOrdPx", newTakeProfitOrderPrice?.ToOkxString());

        // Stop Loss
        parameters.AddOptionalParameter("newSlTriggerPxType", JsonConvert.SerializeObject(newStopLossTriggerPriceType, new OkxAlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("newSlTriggerPx", newStopLossTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("newSlOrdPx", newStopLossOrderPrice?.ToOkxString());
        
        // Trigger Order
        parameters.AddOptionalParameter("newTriggerPx", newTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("newOrdPx", newOrderPrice?.ToOkxString());
        parameters.AddOptionalParameter("newTriggerPxType", JsonConvert.SerializeObject(newTriggerPriceType, new OkxAlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("attachAlgoOrds", attachedAlgoOrders);

        // Reequest
        return ProcessOneRequestAsync<OkxAlgoAmendOrderResponse>(GetUri(v5TradeAmendAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoCancelOrderResponse>>> CancelAdvanceAsync(IEnumerable<OkxAlgoCancelOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { Options.RequestBodyParameterKey, orders },
        };

        return ProcessListRequestAsync<OkxAlgoCancelOrderResponse>(GetUri(v5TradeCancelAdvanceAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get Algo order details
    /// </summary>
    /// <param name="algoOrderId">Algo ID. Either algoId or algoClOrdId is required.If both are passed, algoId will be used.</param>
    /// <param name="algoClientOrderId">Client-supplied Algo ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAlgoOrder>> GetOrderAsync(long? algoOrderId = null, string? algoClientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptionalParameter("algoClOrdId", algoClientOrderId);

        return ProcessOneRequestAsync<OkxAlgoOrder>(GetUri(v5TradeOrderAlgoGet), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve a list of untriggered Algo orders under the current account.
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="algoClientOrderId">Client-supplied Algo ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoOrder>>> GetOpenOrdersAsync(
        OkxAlgoOrderType algoOrderType,
        long? algoId = null,
        string? algoClientOrderId = null,
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "ordType", JsonConvert.SerializeObject(algoOrderType, new OkxAlgoOrderTypeConverter(false)) }
        };
        parameters.AddOptionalParameter("algoId", algoId?.ToOkxString());
        parameters.AddOptionalParameter("algoClOrdId", algoClientOrderId);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAlgoOrder>(GetUri(v5TradeOrdersAlgoPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve a list of untriggered Algo orders under the current account.
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoOrderState">Algo Order State</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoOrder>>> GetOrderHistoryAsync(
        OkxAlgoOrderType algoOrderType,
        OkxAlgoOrderState? algoOrderState = null,
        long? algoId = null,
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            {"ordType",   JsonConvert.SerializeObject(algoOrderType, new OkxAlgoOrderTypeConverter(false))}
        };
        parameters.AddOptionalParameter("algoId", algoId?.ToOkxString());
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(algoOrderState, new OkxAlgoOrderStateConverter(false)));
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxAlgoOrder>(GetUri(v5TradeOrdersAlgoHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}
