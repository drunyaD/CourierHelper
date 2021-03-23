using System.Runtime.Serialization;

namespace DataAccessLayer.DataModels
{
    public enum GeocodeStatusCode
    {
        [EnumMember(Value = "OK")]
        OK,

        [EnumMember(Value = "ZERO_RESULTS")]
        ZeroResults,

        [EnumMember(Value = "OVER_DAILY_LIMIT")]
        OverDailyLimit,

        [EnumMember(Value = "OVER_QUERY_LIMIT")]
        OverQueryLimit,

        [EnumMember(Value = "REQUEST_DENIED")]
        RequestDenied,

        [EnumMember(Value = "INVALID_REQUEST")]
        InvalidRequest,

        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError
    }
}
