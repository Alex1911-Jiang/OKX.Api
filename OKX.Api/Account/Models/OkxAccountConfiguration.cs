﻿using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.CopyTrading.Converters;
using OKX.Api.CopyTrading.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Net;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxAccountConfiguration
/// </summary>
public class OkxAccountConfiguration
{
    /// <summary>
    /// Account ID of current request.
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Main Account ID of current request.
    /// The current request account is main account if uid = mainUid.
    /// The current request account is sub-account if uid != mainUid.
    /// </summary>
    [JsonProperty("mainUid")]
    public long MainUserId { get; set; }

    /// <summary>
    /// Account level
    /// </summary>
    [JsonProperty("acctLv"), JsonConverter(typeof(OkxAccountLevelConverter))]
    public OkxAccountLevel AccountLevel { get; set; }

    /// <summary>
    /// Account self-trade prevention mode
    /// cancel_maker
    /// cancel_taker
    /// cancel_both
    /// Users can log in to the webpage through the master account to modify this configuration
    /// </summary>
    [JsonProperty("acctStpMode"), JsonConverter(typeof(OkxSelfTradePreventionModeConverter))]
    public OkxSelfTradePreventionMode  SelfTradePreventionMode { get; set; }
    
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode"), JsonConverter(typeof(OkxTradePositionModeConverter))]
    public OkxTradePositionMode PositionMode { get; set; }

    /// <summary>
    /// Whether to borrow coins automatically
    /// </summary>
    [JsonProperty("autoLoan"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool AutoLoan { get; set; }

    /// <summary>
    /// Current display type of Greeks
    /// </summary>
    [JsonProperty("greeksType"), JsonConverter(typeof(OkxAccountGreeksTypeConverter))]
    public Enums.OkxAccountGreeksType GreeksType { get; set; }

    /// <summary>
    /// The user level of the current real trading volume on the platform, e.g lv1
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }

    /// <summary>
    /// Temporary experience user level of special users, e.g lv3
    /// </summary>
    [JsonProperty("levelTmp")]
    public string LevelTemporary { get; set; }

    /// <summary>
    /// Contract isolated margin trading settings
    /// </summary>
    [JsonProperty("ctIsoMode"), JsonConverter(typeof(OkxAccountIsolatedMarginModeConverter))]
    public OkxAccountIsolatedMarginMode ContractIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// Margin isolated margin trading settings
    /// </summary>
    [JsonProperty("mgnIsoMode"), JsonConverter(typeof(OkxAccountIsolatedMarginModeConverter))]
    public OkxAccountIsolatedMarginMode MarginIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// Risk offset type
    /// Only applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotOffsetType"), JsonConverter(typeof(OkxAccountRiskOffsetTypeConverter))]
    public OkxAccountRiskOffsetType? RiskOffsetType { get; set; }

    /// <summary>
    /// Role type.
    /// </summary>
    [JsonProperty("roleType"), JsonConverter(typeof(OkxAccountRoleTypeConverter))]
    public OkxAccountRoleType? AccountRoleType { get; set; }

    /// <summary>
    /// Leading trade instruments, only applicable to Leading trader
    /// </summary>
    [JsonProperty("traderInsts")]
    public List<string> LeadingTraderInstruments { get; set; }

    /// <summary>
    /// SPOT copy trading role type
    /// 0: General user；1：Leading trader；2：Copy trader
    /// </summary>
    [JsonProperty("spotRoleType"), JsonConverter(typeof(OkxCopyTradingRoleConverter))]
    public OkxCopyTradingRole? SpotCopyTradingRole { get; set; }

    /// <summary>
    /// Spot lead trading instruments, only applicable to Leanding trader
    /// </summary>
    [JsonProperty("spotTraderInsts")]
    public List<string> SpotLeadingTraderInstruments { get; set; }

    /// <summary>
    /// Whether the optional trading was activated
    /// </summary>
    [JsonProperty("opAuth"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool OptionalTradingActivated { get; set; }

    /// <summary>
    /// Main account KYC level
    /// 0: No verification 1: level 1 completed, 2: level 2 completed, 3: level 3 completed.
    /// If the request originates from a subaccount, kycLv is the KYC level of the main account.
    /// If the request originates from the main account, kycLv is the KYC level of the current account.
    /// </summary>
    [JsonProperty("kycLv"), JsonConverter(typeof(OkxKycLevelConverter))]
    public OkxKycLevel KycLevel { get; set; }

    /// <summary>
    /// API key note of current request API key. No more than 50 letters (case sensitive) or numbers, which can be pure letters or pure numbers.
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; }

    /// <summary>
    /// IP addresses that linked with current API key, separate with commas if more than one, e.g. 117.37.203.58,117.37.203.57. It is an empty string "" if there is no IP bonded.
    /// </summary>
    [JsonProperty("ip")]
    public string IpAddresses { get; set; }

    /// <summary>
    /// The permission of the urrent request API Key. read_only：Read only；trade ：Trade; withdraw: Withdraw
    /// </summary>
    [JsonProperty("perm"), JsonConverter(typeof(OkxAccountApiKeyPermissionConverter))]
    public OkxAccountApiKeyPermission ApiKeyPermission { get; set; }
    
    /// <summary>
    /// Discount rule type for current account
    /// 0: Original discount rate rules, the default value
    /// 1: New discount rules
    /// After new discount rate rules are effective completely, this parameter will be removed from the endpoint. Advice you to prepare in advance.
    /// </summary>
    [JsonProperty("discountType"), JsonConverter(typeof(OkxAccountDiscountTypeConverter))]
    public OkxAccountDiscountType DiscountType { get; set; }
    
    /// <summary>
    /// The margin ratio level of liquidation alert
    /// 3 means that you will get hourly liquidation alerts on app and channel "Position risk warning" when your margin level drops to or below 300%
    /// 0 means that there is not alert
    /// </summary>
    [JsonProperty("liquidationGear")]
    public int LiquidationGear { get; set; }
}
