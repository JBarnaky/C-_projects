using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace DAL
{
    public class VehiclesActiveRecord
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public string Owner { get; set; }
        public string Number { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }

        private static string connectionStr = @"Data Source=ИГОРЬ-ПК\SQL_EXPRESS;Initial Catalog=Obraz1;Integrated Security=True";

        static VehiclesActiveRecord() { }
         
        public static IEnumerable<VehiclesActiveRecord> GetAll()
        {
            SqlConnection sqlConnection = new SqlConnection(VehiclesActiveRecord.connectionStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Vehicles", sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<VehiclesActiveRecord> lst = new List<VehiclesActiveRecord>();
            while (sqlDataReader.Read())
            {
                int id = sqlDataReader.GetInt32(0);
                string model = sqlDataReader.GetString(1);
                string owner = sqlDataReader.GetString(2);
                string number = sqlDataReader.GetString(3);
                SqlString sqlStringColor = sqlDataReader.GetSqlString(4);
                string color = sqlStringColor.IsNull ? "" : sqlStringColor.Value;
                int year = sqlDataReader.GetInt32(5);

                VehiclesActiveRecord vehicle = new VehiclesActiveRecord()
                {
                    ID = id,
                    Model = model,
                    Owner = owner,
                    Number = number,
                    Color = color,
                    Year = year
                };

                lst.Add(vehicle);
            }
            sqlDataReader.Close();
            sqlConnection.Close();

            return lst;
        }

        public void Add()
        {
            SqlConnection sqlConnection = new SqlConnection(VehiclesActiveRecord.connectionStr);
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Vehicles VALUES (@m,@o,@n,@c,@y)", sqlConnection);
            insertCommand.Parameters.Add(new SqlParameter("@m", typeof(string))).Value = Model;
            insertCommand.Parameters.Add(new SqlParameter("@o", typeof(string))).Value = Owner;
            insertCommand.Parameters.Add(new SqlParameter("@n", typeof(string))).Value = Number;
            insertCommand.Parameters.Add(new SqlParameter("@c", typeof(string))).Value = Color;
            insertCommand.Parameters.Add(new SqlParameter("@y", typeof(int))).Value = Year;
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            insertCommand.Connection.Close();
            sqlConnection.Close();
        }

        public void Update()
        {
            SqlConnection sqlConnection = new SqlConnection(VehiclesActiveRecord.connectionStr);
            SqlCommand updateCommand = new SqlCommand("UPDATE Vehicles SET Model = @m, Owner = @o, Number = @n, Color = @c, Year = @y WHERE (ID = @id)", sqlConnection);
            updateCommand.Parameters.Add(new SqlParameter("@id", typeof(string))).Value = ID;
            updateCommand.Parameters.Add(new SqlParameter("@m", typeof(string))).Value = Model;
            updateCommand.Parameters.Add(new SqlParameter("@o", typeof(string))).Value = Owner;
            updateCommand.Parameters.Add(new SqlParameter("@n", typeof(string))).Value = Number;
            updateCommand.Parameters.Add(new SqlParameter("@c", typeof(string))).Value = Color;
            updateCommand.Parameters.Add(new SqlParameter("@y", typeof(int))).Value = Year;
            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            updateCommand.Connection.Close();
            sqlConnection.Close();
        }

        public static void Remove(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(VehiclesActiveRecord.connectionStr);
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Vehicles WHERE (ID = @id)", sqlConnection);
            deleteCommand.Parameters.Add(new SqlParameter("@id", typeof(int))).Value = id;
            sqlConnection.Open();
            deleteCommand.ExecuteNonQuery();
            deleteCommand.Connection.Close();
            sqlConnection.Close();
        }
    }
}
