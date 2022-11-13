using System.Data;
using Microsoft.Data.Sqlite;

namespace candid_exercise.Options;
public class ConnectionStringOptions{
    public ConnectionStringOptions(string connectionString){
        ConnectionString=connectionString;
    }
    public string ConnectionString { get; }
    public IDbConnection GetDbConnection(){
        return new SqliteConnection(ConnectionString);
    }
}