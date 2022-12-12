using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication1.Entities;

namespace WebApplication1.Repository
{
    public class Repository
    {
        private readonly string _connectionstring;
        public Repository()
        {
            this._connectionstring = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build().GetConnectionString("teste");
        }

        public void InserirTeste(Teste newTeste)
        {
            using SqlConnection connection = new SqlConnection(this._connectionstring);

            var query = @"
            USE teste1
            INSERT INTO teste(nome, senha)
                VALUES(@nome, @senha)
              ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@nome", SqlDbType.VarChar).Value = newTeste.Nome;
            command.Parameters.Add("@senha", SqlDbType.VarChar).Value = newTeste.Senha;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Teste> ListarTodos()
        {
            var query = @"USE teste1 SELECT * FROM teste";
            using var connection = new SqlConnection(this._connectionstring);

            List<Teste> teste = connection.Query<Teste>(query, connection).AsList<Teste>();

            return teste;
        }

    }
}
