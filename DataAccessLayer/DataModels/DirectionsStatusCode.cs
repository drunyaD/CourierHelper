using System.Runtime.Serialization;

namespace DataAccessLayer.DataModels
{
    public enum DirectionsStatusCode
    {
        [EnumMember(Value = "OK")]
        OK,

        [EnumMember(Value = "NOT_FOUND")]
        NotFound,

        [EnumMember(Value = "ZERO_RESULTS")]
        ZeroResults,

        [EnumMember(Value = "MAX_WAYPOINTS_EXCEEDED")]
        MaxWaypointsExceeded,

        [EnumMember(Value = "MAX_ROUTE_LENGTH_EXCEEDED")]
        MaxRouteLengthExceed,

        [EnumMember(Value = "INVALID_REQUEST")]
        InvalidRequest,

        [EnumMember(Value = "OVER_DAILY_LIMIT")]
        OverDailyLimit,

        [EnumMember(Value = "OVER_QUERY_LIMIT")]
        OverQueryLimit,

        [EnumMember(Value = "REQUEST_DENIED")]
        RequestDenied,

        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError
    }
}
