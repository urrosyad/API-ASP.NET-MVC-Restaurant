using System.Data;
using MySql.Data.MySqlClient;

public class DbMeja
{
    private readonly string connectionString;
    private readonly MySqlConnection _connection;

    public DbMeja(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new MySqlConnection(connectionString);
    }

public List<Meja> GetAllMeja()
{
    List<Meja> mejaList = new List<Meja>();
    try
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM meja";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Meja meja = new Meja
                    {
                        id_meja = Convert.ToInt32(reader["id_meja"]),
                        no_meja = reader["no_meja"].ToString(),
                    };
                    mejaList.Add(meja);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return mejaList;
}

// METHOD CREATE MEJA
public int CreateMeja(Meja meja)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "INSERT INTO meja (no_meja) VALUES (@no_meja)";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@no_meja", meja.no_meja);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}

// METHOD UPDATE MEJA
public int UpdateMeja(int id_meja, Meja meja)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "UPDATE meja SET no_meja = @no_meja WHERE id_meja = @id_meja";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@no_meja", meja.no_meja);
            command.Parameters.AddWithValue("@Id_meja", id_meja);

            connection.Open();
            return command.ExecuteNonQuery();

        }
    }
}

// METHOD DELETE MEJA
public int DeleteMeja(int id_meja)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "DELETE FROM meja WHERE id_meja = @id_meja";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id_meja", id_meja);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
}