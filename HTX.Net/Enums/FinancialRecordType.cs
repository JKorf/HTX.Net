using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Financial record type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FinancialRecordType>))]
    public enum FinancialRecordType
    {
        /// <summary>
        /// ["<c>3</c>"] Close long
        /// </summary>
        [Map("3")]
        CloseLong,
        /// <summary>
        /// ["<c>4</c>"] Close short
        /// </summary>
        [Map("4")]
        CloseShort,
        /// <summary>
        /// ["<c>5</c>"] Fees for open position - taker
        /// </summary>
        [Map("5")]
        FeesForOpenPositionsTaker,
        /// <summary>
        /// ["<c>6</c>"] Fess for open position - maker
        /// </summary>
        [Map("6")]
        FeesForOpenPositionMaker,
        /// <summary>
        /// ["<c>7</c>"] Fees for close positon - taker
        /// </summary>
        [Map("7")]
        FeesForClosePositionTaker,
        /// <summary>
        /// ["<c>8</c>"] Fees for close potion - maker
        /// </summary>
        [Map("8")]
        FeesForClosePositionMaker,
        /// <summary>
        /// ["<c>9</c>"] Close long for delivery
        /// </summary>
        [Map("9")]
        CloseLongForDelivery,
        /// <summary>
        /// ["<c>10</c>"] Close short for delibery
        /// </summary>
        [Map("10")]
        CloseShortForDelivery,
        /// <summary>
        /// ["<c>11</c>"] Delivery fee
        /// </summary>
        [Map("11")]
        DeliveryFee,
        /// <summary>
        /// ["<c>12</c>"] Close long for liquidation
        /// </summary>
        [Map("12")]
        CloseLongForLiquidation,
        /// <summary>
        /// ["<c>13</c>"] Close short for liquidation
        /// </summary>
        [Map("13")]
        CloseShortForLiquidation,
        /// <summary>
        /// ["<c>14</c>"] Transfer spot to contract
        /// </summary>
        [Map("14")]
        TransferSpotToContract,
        /// <summary>
        /// ["<c>15</c>"] Transfer contract to spot
        /// </summary>
        [Map("15")]
        TransferContractToSpot,
        /// <summary>
        /// ["<c>16</c>"] Settle unrealized long
        /// </summary>
        [Map("16")]
        SettleUnrealizedLong,
        /// <summary>
        /// ["<c>17</c>"] Settle unrealized short
        /// </summary>
        [Map("17")]
        SettleUnrealizedShort,
        /// <summary>
        /// ["<c>19</c>"] Clawback
        /// </summary>
        [Map("19")]
        Clawback,
        /// <summary>
        /// ["<c>26</c>"] System
        /// </summary>
        [Map("26")]
        System,
        /// <summary>
        /// ["<c>28</c>"] Activity price rewards
        /// </summary>
        [Map("28")]
        ActivityPriceRewards,
        /// <summary>
        /// ["<c>29</c>"] Rebate
        /// </summary>
        [Map("29")]
        Rebate,
        /// <summary>
        /// ["<c>30</c>"] Funding fee income
        /// </summary>
        [Map("30")]
        FundingFeeIncome,
        /// <summary>
        /// ["<c>31</c>"] Funding fee expenditure
        /// </summary>
        [Map("31")]
        FundingFeeExpenditure,
        /// <summary>
        /// ["<c>34</c>"] Transfer to sub account
        /// </summary>
        [Map("34")]
        TransferToSub,
        /// <summary>
        /// ["<c>35</c>"] Transfer from sub account
        /// </summary>
        [Map("35")]
        TransferFromSub,
        /// <summary>
        /// ["<c>36</c>"] Transfer to master account
        /// </summary>
        [Map("36")]
        TransferToMaster,
        /// <summary>
        /// ["<c>37</c>"] Transfer from master account
        /// </summary>
        [Map("37")]
        TransferFromMaster,
        /// <summary>
        /// ["<c>38</c>"] Transfer from other margin account
        /// </summary>
        [Map("38")]
        TransferFromOtherMargin,
        /// <summary>
        /// ["<c>39</c>"] Transfer to other margin account
        /// </summary>
        [Map("39")]
        TransferToOtherMargin,
        /// <summary>
        /// ["<c>46</c>"] Adl close long
        /// </summary>
        [Map("46")]
        AdlCloseLong,
        /// <summary>
        /// ["<c>47</c>"] Adl close short
        /// </summary>
        [Map("47")]
        AdlCloseShort
    }
}
