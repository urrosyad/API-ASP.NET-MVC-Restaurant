using System.Data;
using MySql.Data.MySqlClient;

public class DbWaitress
{
    private readonly string connectionString;
    private readonly MySqlConnection _connection;

    public DbWaitress(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new MySqlConnection(connectionString);
    }

public List<Waitress> GetAllWaitress()
{
    List<Waitress> waitressList = new List<Waitress>();
    try
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM waitress";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Waitress waitress = new Waitress
                    {
                        id_waitress = Convert.ToInt32(reader["id_waitress"]),
                        nama_waitress = reader["nama_waitress"].ToString(),
                        user_waitress = reader["user_waitress"].ToString(),
                        pass_waitress = reader["pass_waitress"].ToString(),
                    };
                    waitressList.Add(waitress);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return waitressList;
}

// METHOD CREATE WAITRESS
public int CreateWaitress(Waitress waitress)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "INSERT INTO waitress (nama_waitress, user_waitress, pass_waitress) VALUES (@nama_waitress, @user_waitress, @pass_waitress)";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nama_waitress", waitress.nama_waitress);
            command.Parameters.AddWithValue("@user_waitress", waitress.user_waitress);
            command.Parameters.AddWithValue("@pass_waitress", waitress.pass_waitress);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}

// METHOD UPDATE WAITRESS
public int UpdateWaitress(int id_waitress, Waitress waitress)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "UPDATE waitress SET nama_waitress = @nama_waitress, user_waitress = @user_waitress, pass_waitress = @pass_waitress WHERE id_waitress = @id_waitress";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nama_waitress", waitress.nama_waitress);
            command.Parameters.AddWithValue("@user_waitress", waitress.user_waitress);
            command.Parameters.AddWithValue("@pass_waitress", waitress.pass_waitress);
            command.Parameters.AddWithValue("@Id_waitress", id_waitress);

            connection.Open();
            return command.ExecuteNonQuery();

        }
    }
}

// METHOD DELETE WAITRESS
public int DeleteWaitress(int id_waitress)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "DELETE FROM waitress WHERE id_waitress = @id_waitress";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id_waitress", id_waitress);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
}