using System;
using System.Collections.Generic;

namespace CarDeliveryNetwork.Types.Interfaces
{
    /// <summary>
    /// Interface describing a Car Delivery Network user.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Id
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Username
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Full name
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Summary
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// Email
        /// </summary>
        string Email { get; }

        /// <summary>
        /// LastLogin
        /// </summary>
        DateTime? LastLogin { get; }

        /// <summary>
        /// Home Fleet
        /// </summary>
        IFleet HomeFleet { get; }

        /// <summary>
        /// AccessLevel 
        /// </summary>
        int AccessLevel { get; }

        /// <summary>
        /// ApiKey 
        /// </summary>
        Guid ApiKey { get; set; }

        /// <summary>
        /// Feeling
        /// </summary>
        UserFeeling? Feeling { get; set; }

        /// <summary>
        /// PrefTimeZone
        /// </summary>
        string PrefTimeZone { get; set; }

        /// <summary>
        /// PrefTimeZoneInfo
        /// </summary>
        TimeZoneInfo PrefTimeZoneInfo { get; }

        /// <summary>
        /// Rights
        /// </summary>
        List<string> Rights { get; }

        /// <summary>
        /// HasRight
        /// </summary>
        /// <param name="rightName"></param>
        /// <returns></returns>
        bool HasRight(string rightName);
    }
}
