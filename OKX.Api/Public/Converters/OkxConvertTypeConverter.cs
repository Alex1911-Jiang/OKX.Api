﻿namespace OKX.Api.Public;

internal class OkxConvertTypeConverter(bool quotes) : BaseConverter<OkxConvertType>(quotes)
{
    public OkxConvertTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxConvertType, string>> Mapping =>
    [
        new(OkxConvertType.CurrencyToContract, "1"),
        new(OkxConvertType.ContractToCurrency, "2"),
    ];
}