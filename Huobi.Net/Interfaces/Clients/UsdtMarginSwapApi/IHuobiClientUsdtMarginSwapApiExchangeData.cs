using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Huobi usdt margin swap exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapApiExchangeData
    {
        Task<WebCallResult<IEnumerable<HuobiBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiSwapBestOffer>>> GetBestOfferAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiContractInfo>>> GetContractInfoAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiCrossSwapAdjustFactorInfo>>> GetCrossAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? type = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? marginAccount = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiFundingRate>>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiSwapAdjustFactorInfo>>> GetIsolatedAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiContractStatus>>> GetIsolatedStatusAsync(string? contractCode = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiTieredMarginInfo>>> GetIsolatedTieredMarginInfoAsync(string? contractCode = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiLiquidationOrderPage>> GetLiquidationOrdersAsync(int createDate, LiquidationTradeType tradeType, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiMarketData>> GetMarketDataAsync(string contractCode, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiMarketData>>> GetMarketDatasAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiOpenInterestValue>> GetOpenInterestAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default);
        Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string contractCode, string step, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default);
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<HuobiSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
    }
}