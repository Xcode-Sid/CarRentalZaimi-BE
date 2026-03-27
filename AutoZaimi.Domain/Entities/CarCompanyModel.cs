using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class CarCompanyModel : GeneralData
{
    public required virtual CarCompanyName? CarCompanyName { get; set; }
}
