namespace candid_exercise.QueryModels
{
    public enum ListCustomersSortField
    {
        Id, FirstName, LastName
    }
    public class ListCustomersQueryModel{
        public ListCustomersSortField Field {get;set;} = ListCustomersSortField.Id;
        public bool Desc {get;set;}= false;
    }
}