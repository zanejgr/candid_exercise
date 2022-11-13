using candid_exercise.DTOs;
using candid_exercise.Options;
using candid_exercise.QueryModels;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace candid_exercise.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ConnectionStringOptions _connectionStringOptions;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="connectionStringOptions">Injected from app settings</param>
    public CustomerController(ConnectionStringOptions connectionStringOptions)
    {
        _connectionStringOptions = connectionStringOptions;
    }

    /// <summary>
    /// update the first and last name of the given customer 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customer"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(long id, [FromBody] CreateCustomerDTO customer)
    {
        var sql = @"UPDATE customers
                SET first_name=@FirstName, last_name=@LastName
                WHERE customer_id = @Id;";
        using(var conn=_connectionStringOptions.GetDbConnection()){
            var ct = await conn.ExecuteScalarAsync<int>(
                @$"SELECT COUNT(*) FROM customers WHERE customer_id={id};"
            );
            if(ct==0){return NotFound();}
            ct=await conn.ExecuteAsync(sql,new Customer(id,customer));
            if(ct==0){return BadRequest();}
        }
        return Ok();
    }

    /// <summary>
    /// create customer with the given first and last name
    /// </summary>
    /// <param name="customer"><see cref="CreateCustomerDTO"/>
    /// </param>
    /// <returns>
    /// 201 status code with uri and value of new customer 
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerDTO customer){
        var sql=@"INSERT INTO customers (first_name, last_name)
                VALUES (@FirstName, @LastName);
                SELECT last_insert_rowid();";
        using(var conn = _connectionStringOptions.GetDbConnection()){
            long id = await conn.QuerySingleAsync<long>(sql, customer);
            return Created($"/customer/{id}", new Customer(id,customer));
        }
    }

    /// <summary>
    /// list the customers
    /// </summary>
    /// <param name="query">determines the ordering of the results,
    /// eg '/customer?query[0].Field=id&query[0].Desc=true'
    /// </param>
    /// <returns>
    /// 400 status code if the query is not valid
    /// 200 status code otherwise
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> ListCustomers([FromQuery] ListCustomersQueryModel[] query)
    {
        //validation
        if (query.Count() > 3)
        {
            return BadRequest("Cannot sort by more than 3 fields");
        }
        string sql = "SELECT * FROM customers ";
        if (query.Any())
        {
            sql += "ORDER BY ";
        }
        // build order-by statements
        for (int i = 0; i < query.Count(); i++)
        {
            for (int j = i + 1; j < query.Count(); j++)
            {
                if (query[i].Field == query[j].Field)
                {
                    return BadRequest("Cannot sort by the same field multiple times");
                }
            }
            sql += query[i].Field switch
            {
                ListCustomersSortField.Id => "customer_id ",
                ListCustomersSortField.FirstName => "first_name ",
                ListCustomersSortField.LastName => "last_name ",
                _ => throw new ArgumentOutOfRangeException($"Unexpected sort field {query[i].Field}")
            };
            if (query[i].Desc)
            {
                sql += "DESC, ";
            }
            else
            {
                sql += ", ";
            }
        }
        //finalize query
        int lastComma = sql.LastIndexOf(',');
        if (lastComma != -1)
        {
            sql = sql.Remove(lastComma);
        }
        sql += ";";
        using (var conn = _connectionStringOptions.GetDbConnection())
        {
            var res = await conn.QueryAsync<Customer>(sql);
            return Ok(res);
        }
    }

    /// <summary>
    /// get the customer with the given ID 
    /// </summary>
    /// <param name="id">
    /// primary key of the <see cref="Customer"/>
    /// </param>
    /// <returns>
    /// 200 status code if success, 
    /// 404 status code if the customer does not exist
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(long id)
    {
        string sql = @$"SELECT * FROM customers WHERE customer_id = {id}";
        using (var conn = _connectionStringOptions.GetDbConnection())
        {
            var res = await conn.QueryAsync<Customer>(sql);
            if (res.Count() == 0)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }

    /// <summary>
    /// delete the customer with the given ID.
    /// Any attached addresses are deleted by the database.
    /// </summary>
    /// <param name="id">
    /// the id of the <see cref="Customer"/> to delete
    /// /// </param>
    /// <returns>
    /// 200 status code if success
    /// 404 status code if not found
    /// /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
        string sql = @$"DELETE FROM customers WHERE customer_id = {id};";
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
