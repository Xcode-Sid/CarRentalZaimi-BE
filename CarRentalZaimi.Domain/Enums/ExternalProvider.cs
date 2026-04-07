using System.ComponentModel;

namespace CarRentalZaimi.Domain.Enums;

public enum ExternalProvider
{
    [Description("Google")]
    Google = 1,

    [Description("Facebook")]
    Facebook = 2,

    [Description("Microsoft")]
    Microsoft = 3,

    [Description("Yahoo")]
    Yahoo = 4,

    [Description("Apple")]
    Apple = 5,

    [Description("Twitter")]
    Twitter = 6,

    [Description("GitHub")]
    GitHub = 7,

    [Description("LinkedIn")]
    LinkedIn = 8,
}
