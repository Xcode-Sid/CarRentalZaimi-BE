using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllBookings;

public class GetAllBookingsQuery : IQuery<PagedResponse<BookingDto>>
{
    public string? Search { get; set; }
    public int PageNr { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Status { get; set; }
    public string? PaymentType { get; set; } 
}
