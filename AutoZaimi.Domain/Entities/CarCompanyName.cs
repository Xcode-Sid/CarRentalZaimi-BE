using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class CarCompanyName : GeneralData
{
    public virtual ICollection<CarCompanyModel>? CarCompanyModels { get; set; }
}
