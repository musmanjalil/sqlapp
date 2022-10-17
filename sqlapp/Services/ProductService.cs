using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {

        public static string db_source { get; set; } = "appservermssql.database.windows.net";
        public static string db_user { get; set; } = "saappserver";
        public static string db_password { get; set; } = "Admin@q8";
        public static string db_database { get; set; } = "appdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;

            return new SqlConnection(_builder.ConnectionString);
        }


        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> productsList = new List<Product>();

            string statement = "SELECT * FROM dbo.Products";
            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };

                    productsList.Add(product);
                }
            }

            conn.Close();
            return productsList;
        }


    }
}
