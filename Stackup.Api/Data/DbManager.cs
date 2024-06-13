using System.Data;
using MySql.Data.MySqlClient;

public class DbManager
{
    private readonly string connectionString;
    private readonly MySqlConnection _connection;

    public DbManager(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new MySqlConnection(connectionString);
    }

public List<Manager> GetAllManager()
{
    List<Manager> managerList = new List<Manager>();
    try
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM manager";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Manager manager = new Manager
                    {
                        id_manager = Convert.ToInt32(reader["id_manager"]),
                        nama_manager = reader["nama_manager"].ToString(),
                        user_manager = reader["user_manager"].ToString(),
                        pass_manager = reader["pass_manager"].ToString(),
                    };
                    managerList.Add(manager);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return managerList;
}

// METHOD CREATE MANAGER
public int CreateManager(Manager manager)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "INSERT INTO manager (nama_manager, user_manager, pass_manager) VALUES (@nama_manager, @user_manager, @pass_manager)";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nama_manager", manager.nama_manager);
            command.Parameters.AddWithValue("@user_manager", manager.user_manager);
            command.Parameters.AddWithValue("@pass_manager", manager.pass_manager);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}

// METHOD UPDATE MANAGER
public int UpdateManager(int id_manager, Manager manager)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "UPDATE manager SET nama_manager = @nama_manager, user_manager = @user_manager, pass_manager = @pass_manager WHERE id_manager = @id_manager";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nama_manager", manager.nama_manager);
            command.Parameters.AddWithValue("@user_manager", manager.user_manager);
            command.Parameters.AddWithValue("@pass_manager", manager.pass_manager);
            command.Parameters.AddWithValue("@Id_manager", id_manager);

            connection.Open();
            return command.ExecuteNonQuery();

        }
    }
}

// METHOD DELETE MANAGER
public int DeleteManager(int id_manager)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "DELETE FROM manager WHERE id_manager = @id_manager";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id_manager", id_manager);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
}