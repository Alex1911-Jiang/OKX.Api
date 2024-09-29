﻿namespace OKX.Api.Common;

internal class OkxInstrumentTypeConverter(bool quotes) : BaseConverter<OkxInstrumentType>(quotes)
{
    public OkxInstrumentTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxInstrumentType, string>> Mapping =>
    [
        new(OkxInstrumentType.Any, "ANY"),
        new(OkxInstrumentType.Spot, "SPOT"),
        new(OkxInstrumentType.Margin, "MARGIN"),
        new(OkxInstrumentType.Swap, "SWAP"),
        new(OkxInstrumentType.Futures, "FUTURES"),
        new(OkxInstrumentType.Option, "OPTION"),
        new(OkxInstrumentType.Contracts, "CONTRACTS"),
    ];
}