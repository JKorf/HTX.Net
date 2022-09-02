using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Financial record type
    /// </summary>
    public enum FinancialRecordType
    {
        /// <summary>
        /// Close long
        /// </summary>
        [Map("3")]
        CloseLong,
        /// <summary>
        /// Close short
        /// </summary>
        [Map("4")]
        CloseShort,
        /// <summary>
        /// Fees for open position - taker
        /// </summary>
        [Map("5")]
        FeesForOpenPositionsTaker,
        /// <summary>
        /// Fess for open position - maker
        /// </summary>
        [Map("6")]
        FeesForOpenPositionMaker,
        /// <summary>
        /// Fees for close positon - taker
        /// </summary>
        [Map("7")]
        FeesForClosePositionTaker,
        /// <summary>
        /// Fees for close potion - maker
        /// </summary>
        [Map("8")]
        FeesForClosePositionMaker,
        /// <summary>
        /// Close long for delivery
        /// </summary>
        [Map("9")]
        CloseLongForDelivery,
        /// <summary>
        /// Close short for delibery
        /// </summary>
        [Map("10")]
        CloseShortForDelivery,
        /// <summary>
        /// Delivery fee
        /// </summary>
        [Map("11")]
        DeliveryFee,
        /// <summary>
        /// Close long for liquidation
        /// </summary>
        [Map("12")]
        CloseLongForLiquidation,
        /// <summary>
        /// Close short for liquidation
        /// </summary>
        [Map("13")]
        CloseShortForLiquidation,
        /// <summary>
        /// Transfer spot to contract
        /// </summary>
        [Map("14")]
        TransferSpotToContract,
        /// <summary>
        /// Transfer contract to spot
        /// </summary>
        [Map("15")]
        TransferContractToSpot,
        /// <summary>
        /// Settle unrealized long
        /// </summary>
        [Map("16")]
        SettleUnrealizedLong,
        /// <summary>
        /// Settle unrealized short
        /// </summary>
        [Map("17")]
        SettleUnrealizedShort,
        /// <summary>
        /// Clawback
        /// </summary>
        [Map("19")]
        Clawback,
        /// <summary>
        /// System
        /// </summary>
        [Map("26")]
        System,
        /// <summary>
        /// Activity price rewards
        /// </summary>
        [Map("28")]
        ActivityPriceRewards,
        /// <summary>
        /// Rebate
        /// </summary>
        [Map("29")]
        Rebate,
        /// <summary>
        /// Funding fee income
        /// </summary>
        [Map("30")]
        FundingFeeIncome,
        /// <summary>
        /// Funding fee expenditure
        /// </summary>
        [Map("31")]
        FundingFeeExpenditure,
        /// <summary>
        /// Transfer to sub account
        /// </summary>
        [Map("34")]
        TransferToSub,
        /// <summary>
        /// Transfer from sub account
        /// </summary>
        [Map("35")]
        TransferFromSub,
        /// <summary>
        /// Transfer to master account
        /// </summary>
        [Map("36")]
        TransferToMaster,
        /// <summary>
        /// Transfer from master account
        /// </summary>
        [Map("37")]
        TransferFromMaster,
        /// <summary>
        /// Transfer from other margin account
        /// </summary>
        [Map("38")]
        TransferFromOtherMargin,
        /// <summary>
        /// Transfer to other margin account
        /// </summary>
        [Map("39")]
        TransferToOtherMargin,
        /// <summary>
        /// Adl close long
        /// </summary>
        [Map("46")]
        AdlCloseLong,
        /// <summary>
        /// Adl close short
        /// </summary>
        [Map("47")]
        AdlCloseShort
    }
}
