using System.Data;
using MySql.Data.MySqlClient;

public class DbMenu
{
    private readonly string connectionString;
    private readonly MySqlConnection _connection;

    public DbMenu(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new MySqlConnection(connectionString);
    }

public List<Menu> GetAllMenu()
{
    List<Menu> menuList = new List<Menu>();
    try
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM menu";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Menu menu = new Menu
                    {
                        id_menu = Convert.ToInt32(reader["id_menu"]),
                        nama_menu = reader["nama_menu"].ToString(),
                        stok_menu = Convert.ToInt32(reader["stok_menu"]),
                        harga_menu = Convert.ToInt32(reader["harga_menu"]),
                    };
                    menuList.Add(menu);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return menuList;
}

// METHOD CREATE MENU
public int CreateMenu(Menu menu)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "INSERT INTO menu (nama_menu, stok_menu, harga_menu) VALUES (@nama_menu, @stok_menu, @harga_menu)";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nama_menu", menu.nama_menu);
            command.Parameters.AddWithValue("@stok_menu", menu.stok_menu);
            command.Parameters.AddWithValue("@harga_menu", menu.harga_menu);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}

// METHOD UPDATE MENU
public int UpdateMenu(int id_menu, Menu menu)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "UPDATE menu SET nama_menu = @nama_menu, stok_menu = @stok_menu, harga_menu = @harga_menu  WHERE id_menu = @id_menu";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nama_menu", menu.nama_menu);
            command.Parameters.AddWithValue("@stok_menu", menu.stok_menu);
            command.Parameters.AddWithValue("@harga_menu", menu.harga_menu);
            command.Parameters.AddWithValue("@Id_menu", id_menu);

            connection.Open();
            return command.ExecuteNonQuery();

        }
    }
}

// METHOD DELETE MENU
public int DeleteMenu(int id_menu)
{
    using (MySqlConnection connection = _connection)
    {
        string query = "DELETE FROM menu WHERE id_menu = @id_menu";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id_menu", id_menu);

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
}