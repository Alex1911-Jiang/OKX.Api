﻿using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;
using OKX.Api.Funding.Models;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Funding.Clients;

/// <summary>
/// OKX Rest Api Funding Account Client
/// </summary>
public class OkxFundingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5AssetCurrencies = "api/v5/asset/currencies";
    private const string v5AssetBalances = "api/v5/asset/balances";
    private const string v5AssetNonTradableAssets = "api/v5/asset/non-tradable-assets";
    private const string v5AssetAssetValuation = "api/v5/asset/asset-valuation";
    private const string v5AssetTransfer = "api/v5/asset/transfer";
    private const string v5AssetTransferState = "api/v5/asset/transfer-state";
    private const string v5AssetBills = "api/v5/asset/bills";
    private const string v5AssetDepositAddress = "api/v5/asset/deposit-address";
    private const string v5AssetDepositHistory = "api/v5/asset/deposit-history";
    private const string v5AssetWithdrawal = "api/v5/asset/withdrawal";
    private const string v5AssetWithdrawalCancel = "api/v5/asset/cancel-withdrawal";
    private const string v5AssetWithdrawalHistory = "api/v5/asset/withdrawal-history";
    private const string v5AssetDepositWithdrawStatus = "api/v5/asset/deposit-withdraw-status";
    private const string v5AssetConvertDustAssets = "api/v5/asset/convert-dust-assets";
    private const string v5AssetExchangeList = "api/v5/asset/exchange-list";
    private const string v5AssetMonthlyStatement = "api/v5/asset/monthly-statement";
    private const string v5AssetConvertCurrencies = "api/v5/asset/convert/currencies";
    private const string v5AssetConvertCurrencyPair = "api/v5/asset/convert/currency-pair";
    private const string v5AssetConvertEstimateQuote = "api/v5/asset/convert/estimate-quote";
    private const string v5AssetConvertTrade = "api/v5/asset/convert/trade";
    private const string v5AssetConvertHistory = "api/v5/asset/convert/history";


    /// <summary>
    /// Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFundingCurrency>(GetUri(v5AssetCurrencies), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Retrieve the balances of all the assets, and the amount that is available or on hold.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingBalance>>> GetBalancesAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxFundingBalance>(GetUri(v5AssetBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get non-tradable asset balances
    /// </summary>
    /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingNonTradableAssetBalance>>> GetNonTradableBalancesAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxFundingNonTradableAssetBalance>(GetUri(v5AssetNonTradableAssets), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// View account asset valuation
    /// </summary>
    /// <param name="currency">Asset valuation calculation unit</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingAssetValuation>> GetAssetValuationAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessOneRequestAsync<OkxFundingAssetValuation>(GetUri(v5AssetAssetValuation), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts. Direct transfers between sub-accounts are not allowed.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="type">Transfer type</param>
    /// <param name="fromAccount">The remitting account</param>
    /// <param name="toAccount">The beneficiary account</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="loanTransfer">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin the default is false</param>
    /// <param name="omitPositionRisk">Ignore position risk</param>
    /// <param name="clientOrderId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingTransferResponse>> FundTransferAsync(
        OkxFundingTransferType type,
        string currency,
        decimal amount,
        OkxAccount fromAccount,
        OkxAccount toAccount,
        string subAccountName = null,
        bool? loanTransfer = null,
        bool? omitPositionRisk = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "type", JsonConvert.SerializeObject(type, new OkxFundingTransferTypeConverter(false)) },
            { "ccy",currency},
            { "amt",amount.ToOkxString()},
            { "from", JsonConvert.SerializeObject(fromAccount, new OkxAccountConverter(false)) },
            { "to", JsonConvert.SerializeObject(toAccount, new OkxAccountConverter(false)) },
        };
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("loanTrans", loanTransfer);
        parameters.AddOptionalParameter("clientId", clientOrderId);
        parameters.AddOptionalParameter("omitPosRisk", omitPositionRisk);

        return ProcessOneRequestAsync<OkxFundingTransferResponse>(GetUri(v5AssetTransfer), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve the transfer state data of the last 2 weeks.
    /// </summary>
    /// <param name="transferId">Transfer ID. Either transId or clientId is required. If both are passed, transId will be used.</param>
    /// <param name="clientOrderId">Client-supplied ID</param>
    /// <param name="type">Transfer type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingTransferStateResponse>> FundTransferStateAsync(
        long? transferId = null,
        string clientOrderId = null,
        OkxFundingTransferType? type = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("transId", transferId?.ToOkxString());
        parameters.AddOptionalParameter("clientId", clientOrderId);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxFundingTransferTypeConverter(false)));

        return ProcessOneRequestAsync<OkxFundingTransferStateResponse>(GetUri(v5AssetTransferState), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Query the billing record, you can get the latest 1 month historical data
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="type">Bill type</param>
    /// <param name="clientOrderId">Client-supplied ID for transfer or withdrawal. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingBill>>> GetFundingBillDetailsAsync(
        string currency = null,
        OkxFundingBillType? type = null,
        string clientOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxFundingBillTypeConverter(false)));
        parameters.AddOptionalParameter("clientId", clientOrderId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingBill>(GetUri(v5AssetBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the deposit addresses of currencies, including previously-used addresses.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingDepositAddress>>> GetDepositAddressAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency },
        };

        return ProcessListRequestAsync<OkxFundingDepositAddress>(GetUri(v5AssetDepositAddress), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the deposit history of all currencies, up to 100 recent records in a year.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="depositId">Deposit Id</param>
    /// <param name="fromWithdrawalId">Internal transfer initiator's withdrawal ID. If the deposit comes from internal transfer, this field displays the withdrawal ID of the internal transfer initiator</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="type">Deposit Type</param>
    /// <param name="state">Status of deposit</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingDepositHistory>>> GetDepositHistoryAsync(
        string currency = null,
        string depositId = null,
        long? fromWithdrawalId = null,
        string transactionId = null,
        OkxFundingDepositType? type = null,
        OkxFundingDepositState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("depId", depositId);
        parameters.AddOptionalParameter("fromWdId", fromWithdrawalId);
        parameters.AddOptionalParameter("txId", transactionId);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxFundingDepositTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxFundingDepositStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingDepositHistory>(GetUri(v5AssetDepositHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Withdrawal of tokens.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="destination">Withdrawal destination address</param>
    /// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'</param>
    /// <param name="fee">Transaction fee</param>
    /// <param name="chain">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni. If this parameter is not filled in because it is not available, it will default to the main chain.</param>
    /// <param name="areaCode">Area code for the phone number. If toAddr is a phone number, this parameter is required.</param>
    /// <param name="receiverInfo">Recipient information. For the specific entity users to do on-chain withdrawal/lightning withdrawal, this information is required.</param>
    /// <param name="clientOrderId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingWithdrawalResponse>> WithdrawAsync(
        string currency,
        decimal amount,
        OkxFundingWithdrawalDestination destination,
        string toAddress,
        decimal fee,
        string chain = null,
        string areaCode = null,
        OkxFundingWithdrawalReceiver receiverInfo = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",currency},
            { "amt",amount.ToOkxString()},
            { "dest", JsonConvert.SerializeObject(destination, new OkxFundingWithdrawalDestinationConverter(false)) },
            { "toAddr",toAddress},
            { "fee",fee   .ToOkxString()},
        };
        parameters.AddOptionalParameter("chain", chain);
        parameters.AddOptionalParameter("areaCode", areaCode);
        parameters.AddOptionalParameter("rcvrInfo", receiverInfo);
        parameters.AddOptionalParameter("clientId", clientOrderId);

        return ProcessOneRequestAsync<OkxFundingWithdrawalResponse>(GetUri(v5AssetWithdrawal), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The maximum withdrawal amount is 0.1 BTC per request, and 1 BTC in 24 hours. The minimum withdrawal amount is approximately 0.000001 BTC. Sub-account does not support withdrawal.
    /// </summary>
    /// <param name="currency">Token symbol. Currently only BTC is supported.</param>
    /// <param name="invoice">Invoice text</param>
    /// <param name="memo">Lightning withdrawal memo</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingLightningWithdrawal>> GetLightningWithdrawalsAsync(
        string currency,
        string invoice,
        string memo = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "invoice", invoice },
        };
        parameters.AddOptionalParameter("memo", memo);

        return ProcessOneRequestAsync<OkxFundingLightningWithdrawal>(GetUri(v5AssetWithdrawalLightning), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Cancel withdrawal
    /// You can cancel normal withdrawal requests, but you cannot cancel withdrawal requests on Lightning.
    /// Rate Limit: 6 requests per second
    /// </summary>
    /// <param name="withdrawalId">Withdrawal ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingWithdrawalId>> CancelWithdrawalAsync(long withdrawalId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "wdId", withdrawalId},
        };

        return ProcessOneRequestAsync<OkxFundingWithdrawalId>(GetUri(v5AssetWithdrawalCancel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="withdrawalId">Withdrawal ID</param>
    /// <param name="clientOrderId">Client Order Id</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="type">Type</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingWithdrawalHistory>>> GetWithdrawalHistoryAsync(
        string currency = null,
        long? withdrawalId = null,
        string clientOrderId = null,
        string transactionId = null,
        OkxFundingWithdrawalType? type = null,
        OkxFundingWithdrawalState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("wdId", withdrawalId);
        parameters.AddOptionalParameter("clientId", clientOrderId);
        parameters.AddOptionalParameter("txId", transactionId);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxFundingWithdrawalTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxFundingWithdrawalStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingWithdrawalHistory>(GetUri(v5AssetWithdrawalHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxFundingDepositStatus>> GetDepositStatusAsync(
        string currency,
        string txId,
        string to,
        string chain,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "ccy", currency },
            { "txId", txId },
            { "to", to },
            { "chain", chain }
        };

        return ProcessOneRequestAsync<OkxFundingDepositStatus>(GetUri(v5AssetDepositWithdrawStatus), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxFundingWithdrawalStatus>> GetWithdrawalStatusAsync(
        long withdrawalId,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "wdId", withdrawalId.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFundingWithdrawalStatus>(GetUri(v5AssetDepositWithdrawStatus), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxFundingConvertDustAssetsResponse>> ConvertDustAssetsAsync(
        IEnumerable<string> currencies,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "ccy", currencies },
        };

        return ProcessOneRequestAsync<OkxFundingConvertDustAssetsResponse>(GetUri(v5AssetConvertDustAssets), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFundingExchangeList>>> GetExchangeListAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFundingExchangeList>(GetUri(v5AssetExchangeList), HttpMethod.Get, ct, signed: false);
    }

    public Task<RestCallResult<OkxTimestamp>> ApplyForMonthlyStatementAsync(
        string month = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("month", month);

        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5AssetMonthlyStatement), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxDownloadLink>> GetMonthlyStatementAsync(
        string month,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("month", month);

        return ProcessOneRequestAsync<OkxDownloadLink>(GetUri(v5AssetMonthlyStatement), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFundingConvertCurrency>>> GetConvertCurrenciesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFundingConvertCurrency>(GetUri(v5AssetConvertCurrencies), HttpMethod.Get, ct, signed: true);
    }

    public Task<RestCallResult<OkxFundingConvertCurrencyPair>> GetConvertCurrencyPairAsync(
        string fromCurrency,
        string toCurrency,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("fromCcy", fromCurrency);
        parameters.AddOptionalParameter("toCcy", toCurrency);

        return ProcessOneRequestAsync<OkxFundingConvertCurrencyPair>(GetUri(v5AssetConvertCurrencyPair), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxFundingConvertEstimateQuote>> EstimateQuoteAsync(
        string baseCurrency,
        string quoteCurrency,
        OkxTradeOrderSide side,
        decimal rfqAmount,
        string rfqCurrency,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "baseCcy", baseCurrency },
            { "quoteCcy", quoteCurrency },
            { "side", JsonConvert.SerializeObject(side, new OkxTradeOrderSideConverter(false)) },
            { "rfqSz", rfqAmount.ToOkxString() },
            { "rfqSzCcy", rfqCurrency },
        };
        parameters.AddOptionalParameter("clQReqId", clientOrderId);
        parameters.AddOptionalParameter("tag", Options.BrokerId);

        return ProcessOneRequestAsync<OkxFundingConvertEstimateQuote>(GetUri(v5AssetConvertEstimateQuote), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxFundingConvertOrder>> PlaceConvertOrderAsync(
        string quoteId,
        string baseCurrency,
        string quoteCurrency,
        OkxTradeOrderSide side,
        decimal amount,
        string amountCurrency,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "quoteId", quoteId },
            { "baseCcy", baseCurrency },
            { "quoteCcy", quoteCurrency },
            { "side", JsonConvert.SerializeObject(side, new OkxTradeOrderSideConverter(false)) },
            { "sz", amount.ToOkxString() },
            { "szCcy", amountCurrency },
        };
        parameters.AddOptionalParameter("clQReqId", clientOrderId);
        parameters.AddOptionalParameter("tag", Options.BrokerId);

        return ProcessOneRequestAsync<OkxFundingConvertOrder>(GetUri(v5AssetConvertTrade), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFundingConvertOrderHistory>>> GetConvertHistoryAsync(
        string clientOrderId = null,
        string tag = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("clTReqId", clientOrderId);
        parameters.AddOptionalParameter("tag", tag);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingConvertOrderHistory>(GetUri(v5AssetConvertHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

}