using CarRentalZaimi.Application.Interfaces.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;

public class DeleteCarCategoryCommand() : ICommand<bool>
{
    public string? Id { get; set; }
}
