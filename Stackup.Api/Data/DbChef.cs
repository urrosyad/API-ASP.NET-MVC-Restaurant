using System.Data;
using MySql.Data.MySqlClient;

public class DbChef
{
    private readonly string connectionString;
    private readonly MySqlConnection _connection;

    public DbChef(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new MySqlConnection(connectionString);
    }

public List<Chef> GetAllChef()
{
    List<Chef> chefList = new List<Chef>();
    try
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM chef";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Chef chef = new Chef
                    {
                        id_chef = Convert.ToInt32(reader["id_chef"]),
                        nama_chef = reader["nama_chef"].ToString(),
                        user_chef = reader["user_chef"].ToString(),
                        pass_chef = reader["pass_chef"].ToString(),
                    };
                    chefList.Add(chef);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return chefList;
}

// METHOD CREATE CHEF
public int CreateChef(Chef chef)
{  
    using (MySqlConnection connection = _connection)
    {
        string query = "INSERT INTO chef (nama_chef, user_chef, pass_chef) VALUES (@nama_chef, @user_chef, @pass_chef)";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nama_chef", chef.nama_chef);
            command.Parameters.AddWithValue("@user_chef", chef.user_chef);
            command.Parameters.AddWithValue("@pass_chef", chef.pass_chef);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}

// METHOD UPDATE CHEF
public int UpdateChef(int id_chef, Chef chef)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "UPDATE chef SET nama_chef = @nama_chef, user_chef = @user_chef, pass_chef = @pass_chef WHERE id_chef = @id_chef";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nama_chef", chef.nama_chef);
            command.Parameters.AddWithValue("@user_chef", chef.user_chef);
            command.Parameters.AddWithValue("@pass_chef", chef.pass_chef);
            command.Parameters.AddWithValue("@Id_chef", id_chef);

            connection.Open();
            return command.ExecuteNonQuery();

        }
    }
}

// METHOD DELETE CHEF
public int DeleteChef(int id_chef)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "DELETE FROM chef WHERE id_chef = @id_chef";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id_chef", id_chef);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
}