using System.ComponentModel.DataAnnotations;

namespace candid_exercise.DTOs;
public class CreateAddressDTO : UpdateAddressDTO
{
    /// <summary>
    /// the id of the corresponding <see cref="Customer"/> 
    /// </summary>
    [Range(1,long.MaxValue)] 
    public long CustomerID { get; set; } = -1;
}