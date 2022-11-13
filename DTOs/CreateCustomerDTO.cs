using System.ComponentModel.DataAnnotations;

namespace candid_exercise.DTOs;
public class CreateCustomerDTO{
    /// <summary>
    /// first name of the customer 
    /// </summary>
 [Required]
 [MaxLength(64)]
 [MinLength(1)]
	public string FirstName { get;set;} = default!;
    /// <summary>
    /// last name of the customer
    /// </summary>
 [Required]
 [MaxLength(64)]
 [MinLength(1)]
	public string LastName { get;set;} = default!;
}