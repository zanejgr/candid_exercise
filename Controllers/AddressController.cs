using candid_exercise.DTOs;
using candid_exercise.Options;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace candid_exercise.Controllers;

/// <summary>
/// Operations on the `addresses` table
/// </summary>
[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private readonly ConnectionStringOptions _connectionStringOptions;
    
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="connectionStringOptions">Injected from app settings</param>
    public AddressController(ConnectionStringOptions connectionStringOptions){
        _connectionStringOptions=connectionStringOptions;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressDTO dto){
        string sql = @"INSERT INTO addresses (customer_id, address_type_id,line_1,line_2,city,state,zip,country) VALUES(
                @CustomerID,
	            @AddressTypeId,
	            @AddressLine1,
	            @AddressLine2,
	            @City,
	            @State,
	            @Zip,
                @Country);
            SELECT last_insert_rowid();";
        using (var conn = _connectionStringOptions.GetDbConnection())
        {
            var ct = await conn.ExecuteScalarAsync<int>(
                @$"SELECT COUNT(*) FROM customers WHERE customer_id={dto.CustomerID};"
            );
            if(ct==0){return BadRequest($"customer with ID {dto.CustomerID} doesn't exist");}
            long id = await conn.QuerySingleAsync<long>(sql,dto);
            return Created($"/address/{id}",new Address(id,dto));
        }
    }

    /// <summary>
    /// Update the given address 
    /// </summary>
    /// <param name="id">
    /// the primary key of the <see cref="Address"/> to update
    /// </param>
    /// <param name="dto">
    /// new values for the address
    /// </param>
    /// <returns>
    /// 400 status if the new address data is not valid,
    /// 404 status if the address does not exist,
    /// 200 status if the update is successful    
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(long id, UpdateAddressDTO dto)
    {
        string sql = @"UPDATE addresses SET 
	            address_type_id=@AddressTypeId,
	            line_1=@AddressLine1,
	            line_2=@AddressLine2,
	            city=@City,
	            state=@State,
	            zip=@Zip,
               country=@Country
            WHERE address_id=@Id;";

        using (var conn = _connectionStringOptions.GetDbConnection())
        {
            var ct = await conn.ExecuteScalarAsync<int>(
                @$"SELECT COUNT(*) FROM addresses WHERE address_id = {id}");
            if (ct == 0) { return NotFound(); }
            ct = await conn.ExecuteAsync(sql, new
            {
                AddressTypeId = dto.AddressTypeId,
                Id = id,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                Zip = dto.Zip,
                Country = dto.Country
            });
            if (ct == 0) { return BadRequest(); }
        }
        return Ok();
    }

    /// <summary>
    /// Get a single address
    /// </summary>
    /// <param name="id">the primary key of the address</param>
    /// <returns><see cref="OkObjectResult"/> if address exists,
    /// <see cref="NotFoundResult"/> otherwise</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddress(long id)
    {
        string sql = @$"SELECT * FROM addresses WHERE address_id = {id}";
        using (var conn = _connectionStringOptions.GetDbConnection())
        {
            var res = await conn.QuerySingleOrDefaultAsync<Address>(sql);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }

    /// <summary>
    /// Get all of the given customer's addresses
    /// </summary>
    /// <param name="customerID">id of the <see cref="Customer"/></param>
    /// <returns>
    /// <see cref="OkObjectResult"/>
    ///  containing the addresses, if they exist.
    ///  </returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] long customerID)
    {
        string sql = @$"SELECT * FROM addresses WHERE customer_id = {customerID}";
        using (var conn = _connectionStringOptions.GetDbConnection())
        {
            var res = await conn.QueryAsync<Address>(sql);
            if (res == null || res.Count() == 0)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }

    /// <summary>
    /// delete address with the given ID 
    /// </summary>
    /// <param name="addressID">
    /// primary key of the <see cref="Address"/> to delete
    /// </param>
    /// <returns>
    /// 200 status code if successful
    /// 404 status code if the address is not found
    ///</returns>
    [HttpDelete("{addressID}")]
    public async Task<IActionResult> Delete(long addressID)
    {
        string sql = @$"DELETE FROM addresses WHERE address_id = {addressID}";
        using (var conn = _connectionStringOptions.GetDbConnection())
        {
            var res = await conn.ExecuteAsync(sql);
            if (res == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
