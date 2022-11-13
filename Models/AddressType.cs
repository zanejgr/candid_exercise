namespace Models;
public class AddressType{
	public string TypeId { get;set;}
	public string TypeDescription { get;set;} 
	public AddressType(string type_id, string type_description){
		TypeId=type_id;
		TypeDescription=type_description;
	}
}
