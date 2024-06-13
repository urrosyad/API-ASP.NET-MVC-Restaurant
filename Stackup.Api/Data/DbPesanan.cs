using System.Data;
using MySql.Data.MySqlClient;

public class DbPesanan
{
    private readonly string connectionString;
    private readonly MySqlConnection _connection;

    public DbPesanan(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new MySqlConnection(connectionString);
    }

public List<Pesanan> GetAllPesanan()
{
    List<Pesanan> dataPesanan = new List<Pesanan>();
    try
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT meja.no_meja, customer.nama_customer, menu.nama_menu, menu.harga_menu, pesanan.jumlah_pesan, pesanan.total_harga FROM pesanan JOIN customer ON pesanan.id_customer = customer.id_customer JOIN meja ON customer.id_meja = meja.id_meja JOIN menu ON pesanan.id_menu = menu.id_menu";

            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Pesanan pesanan = new Pesanan
                    {
                        no_meja = reader["no_meja"].ToString(),
                        nama_customer = reader["nama_customer"].ToString(),
                        nama_menu = reader["nama_menu"].ToString(),
                        harga_menu = Convert.ToInt32(reader["harga_menu"]),
                        jumlah_pesan = Convert.ToInt32(reader["jumlah_pesan"]),
                        total_harga = Convert.ToInt32(reader["total_harga"]),
                    };
                    dataPesanan.Add(pesanan);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return dataPesanan;
}
}