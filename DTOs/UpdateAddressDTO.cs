using System.ComponentModel.DataAnnotations;

namespace candid_exercise.DTOs;
public class UpdateAddressDTO
{
    /// <summary>
    /// <see cref="AddressType"/> key
    /// </summary>
    [Required]
    [MaxLength(1)]
    [MinLength(1)]
    public string AddressTypeId { get; set; } = default!;
    /// <summary>
    /// first line of mailing address
    /// </summary>
    [Required]
    [RegularExpression(@"^([0-9a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$")]
    [MaxLength(128)]
    [MinLength(1)]
    public string AddressLine1 { get; set; } = default!;
    /// <summary>
    /// optional second line of mailing address
    /// </summary>
    [MaxLength(128)]
    [RegularExpression(@"^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$")]
    public string? AddressLine2 { get; set; }
    /// <summary>
    /// city of mailing address
    /// </summary>
    [Required]
    [RegularExpression(@"^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$")]
    [MaxLength(32)]
    public string City { get; set; } = default!;
    /// <summary>
    /// state of mailing address, 
    /// optional because some countries don't have this
    /// </summary>
    [MaxLength(32)]
    [RegularExpression(@"^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$")]
    public string? State { get; set; }
    /// <summary>
    /// postal code, optional because some countries don't have this
    /// </summary>
    [RegularExpression(@"^[-0-9.a-zA-Z]*$")]
    [MaxLength(10)]
    public string? Zip { get; set; }
    /// <summary>
    /// country
    /// </summary>
    [Required]
    [RegularExpression(@"^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$")]
    [MaxLength(56)]
    [MinLength(4)]
    public string Country { get; set; } = default!;
}