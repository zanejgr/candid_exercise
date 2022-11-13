using candid_exercise.DTOs;

namespace Models;
public class Customer{
	public long Id { get;set;}
	public string FirstName { get;set;} 
	public string LastName { get;set;} 
	public Customer(long customer_id, string first_name, string last_name){
		Id = customer_id;
		FirstName=first_name;
		LastName=last_name;
	}
	public Customer(long customer_id, CreateCustomerDTO dto){
		Id=customer_id;
		FirstName=dto.FirstName;
		LastName=dto.LastName;
	}
}
