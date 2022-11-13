using candid_exercise.DTOs;

namespace Models;
/// <summary>
/// Corresponds to entries in the `addresses` table in the database
/// </summary>
public class Address{
	/// <summary>
	/// primary key
	/// </summary>
	public long Id { get;init;} 
	/// <summary>
	/// <see cref="Customer"/> id
	/// </summary>
	public long CustomerId { get;init;} 
	/// <summary>
	/// <see cref="AddressType"/> key
	/// </summary>
	public string AddressTypeId { get;set;}
	/// <summary>
	/// first line of mailing address
	/// </summary>
	public string AddressLine1 { get;set;} 
	/// <summary>
	/// optional second line of mailing address
	/// </summary>
	public string? AddressLine2 { get;set;}
	/// <summary>
	/// city of mailing address
	/// </summary>
	public string City { get;set;}
	/// <summary>
	/// state of mailing address, 
	/// optional because some countries don't have this
	/// </summary>
	public string? State { get;set;}
	/// <summary>
	/// postal code, optional because some countries don't have this
	/// </summary>
	public string? Zip { get;set;}
	/// <summary>
	/// country
	/// </summary>
	public string Country { get;set;}
	/// <summary>
	/// Constructor for Dapper to use
	/// </summary>
	/// <param name="address_id">value for <see cref="Id"/></param>
	/// <param name="customer_id">value for <see cref="CustomerId"/></param>
	/// <param name="address_type_id">value for <see cref="AddressTypeId"/></param>
	/// <param name="line_1">value for <see cref="AddressLine1"/></param>
	/// <param name="line_2">value for <see cref="AddressLine2"/></param>
	/// <param name="city">value for <see cref="City"/></param>
	/// <param name="state">value for <see cref="State"/></param>
	/// <param name="zip">value for <see cref="Zip"/></param>
	/// <param name="country">value for <see cref="Country"/></param>
	public Address(long address_id, long customer_id, string address_type_id, string line_1, string line_2, string city, string state, string zip, string country){
		Id = address_id;
		CustomerId = customer_id;
		AddressTypeId = address_type_id;
		AddressLine1 = line_1;
		AddressLine2 = line_2;
		City = city;
		State = state;
		Zip = zip;
		Country = country;
	}
	public Address(long id, CreateAddressDTO dto){
		Id = id;
		CustomerId = dto.CustomerID; 
		AddressTypeId = dto.AddressTypeId; 
		AddressLine1 = dto.AddressLine1; 
		AddressLine2 = dto.AddressLine2; 
		City = dto.City; 
		State = dto.State; 
		Zip = dto.Zip; 
		Country = dto.Country; 
	
	}
}
