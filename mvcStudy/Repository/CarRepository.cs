using mvcStudy.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace mvcStudy.Repository
{
    // execute nonquery = insert
    //execute escalar = um registro
    //execute reader = varios registros
    public class CarRepository
    {
        public string ConnectionString { get; set; }
        public CarRepository()
        {
            this.ConnectionString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build().GetConnectionString("mvcStudy");
        }

        public Car InsertCar(Car newCar)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    var query = "insert into car(year,brand,model,value) values(@year, @brand, @model, @value)";
                    SqlCommand command = new SqlCommand(query, connection);
                    AddParameters(command, newCar);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return newCar;
        }

        public List<Car> GetCars()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                List<Car> list = new List<Car>();
                var query = "select * from car";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Car newcar = new Car();
                    newcar.Brand = (string)dr["brand"];
                    newcar.Value = (double)dr["value"];
                    newcar.Year = (int)dr["year"];
                    newcar.Model = (string)dr["model"];
                    newcar.Id = (int)dr["id"];
                    list.Add(newcar);
                }
                return list;
            }
        }

        public void DeleteCar(int id)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    var query = "delete from car where id = @id ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public Car FindCarById(int id)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                var query = $"select * from car where id = {id}";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                dr.Read();
                Car newcar = new Car();
                newcar.Brand = (string)dr["brand"];
                newcar.Value = (double)dr["value"];
                newcar.Year = (int)dr["year"];
                newcar.Model = (string)dr["model"];
                newcar.Id = (int)dr["id"];

                connection.Close();
                return newcar;
            }
        }

        public void UpdateCar(Car newData)
        {

            var query = @"UPDATE car
SET brand = @brand, model = @model, year = @year, value = @value
WHERE id = @id";

            using(SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                AddParameters(command, newData);
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        public void AddParameters(SqlCommand command, Car car)
        {
            command.Parameters.Add("@brand", SqlDbType.VarChar).Value = car.Brand;
            command.Parameters.Add("@model", SqlDbType.VarChar).Value = car.Model;
            command.Parameters.Add("@year", SqlDbType.Int).Value = car.Year;
            command.Parameters.Add("@value", SqlDbType.Float).Value = car.Value;
            command.Parameters.Add("@id", SqlDbType.Int).Value = car.Id;


        }
    }
}

