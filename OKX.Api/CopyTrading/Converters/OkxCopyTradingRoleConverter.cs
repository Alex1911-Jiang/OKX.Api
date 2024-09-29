﻿namespace OKX.Api.CopyTrading;

internal class OkxCopyTradingRoleConverter(bool quotes) : BaseConverter<OkxCopyTradingRole>(quotes)
{
    public OkxCopyTradingRoleConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingRole, string>> Mapping =>
    [
        new(OkxCopyTradingRole.GeneralUser, "0"),
        new(OkxCopyTradingRole.LeadingTrader, "1"),
        new(OkxCopyTradingRole.CopyTrader, "2"),
    ];
}