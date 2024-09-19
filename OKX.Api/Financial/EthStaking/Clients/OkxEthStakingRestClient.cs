﻿using OKX.Api.Financial.EthStaking.Converters;
using OKX.Api.Financial.EthStaking.Enums;
using OKX.Api.Financial.EthStaking.Models;

namespace OKX.Api.Financial.EthStaking.Clients;

/// <summary>
/// OKX Rest Api Staking Client
/// </summary>
public class OkxEthStakingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceStakingDefiEthPurchase = "api/v5/finance/staking-defi/eth/purchase";
    private const string v5FinanceStakingDefiEthRedeem = "api/v5/finance/staking-defi/eth/redeem";
    private const string v5FinanceStakingDefiEthBalance = "api/v5/finance/staking-defi/eth/balance";
    private const string v5FinanceStakingDefiEthPurchaseRedeemHistory = "api/v5/finance/staking-defi/eth/purchase-redeem-history";
    private const string v5FinanceStakingDefiEthApyHistory = "api/v5/finance/staking-defi/eth/apy-history";

    /// <summary>
    /// Staking ETH for BETH
    /// Only the assets in the funding account can be used.
    /// </summary>
    /// <param name="amount">Investment amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialEthStakingPurchase>> PurchaseAsync(decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"amt", amount.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFinancialEthStakingPurchase>(GetUri(v5FinanceStakingDefiEthPurchase), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Only the assets in the funding account can be used. If your BETH is in your trading account, you can make funding transfer first.
    /// </summary>
    /// <param name="amount">Redeeming amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialEthStakingRedeem>> RedeemAsync(decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"amt", amount.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFinancialEthStakingRedeem>(GetUri(v5FinanceStakingDefiEthRedeem), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The balance is a snapshot summarized all BETH assets (including assets in redeeming) in account.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialEthStakingBalance>>> GetBalancesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFinancialEthStakingBalance>(GetUri(v5FinanceStakingDefiEthBalance), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// GET / Purchase&amp;Redeem history
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="status">Status</param>
    /// <param name="after">Pagination of data to return records earlier than the requestTime. The value passed is the corresponding timestamp</param>
    /// <param name="before">Pagination of data to return records newer than the requestTime. The value passed is the corresponding timestamp</param>
    /// <param name="limit">Number of results per request. The default is 100. The maximum is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialEthStakingHistory>>> GetHistoryAsync(
        OkxFinancialEthStakingType type,
        OkxFinancialEthStakingStatus? status = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"type", JsonConvert.SerializeObject(type, new OkxFinancialEthStakingTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new OkxFinancialEthStakingStatusConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialEthStakingHistory>(GetUri(v5FinanceStakingDefiEthPurchaseRedeemHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / APY history (Public)
    /// </summary>
    /// <param name="days">Get the days of APY(Annual percentage yield) history record in the past. No more than 365 days</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialEthStakingApyHistory>>> GetApyHistoryAsync(int days, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"days", days.ToOkxString() },
        };

        return ProcessListRequestAsync<OkxFinancialEthStakingApyHistory>(GetUri(v5FinanceStakingDefiEthApyHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
}