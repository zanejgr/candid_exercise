using Microsoft.AspNetCore.Mvc;

namespace candid_exercise.QueryModels;
public class AddressQueryModel{
    [FromQuery] public string? CustomerId { get; set; }
}