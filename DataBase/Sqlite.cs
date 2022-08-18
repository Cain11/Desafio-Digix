using Microsoft.Data.Sqlite;
namespace DesafioDigixx.DataBase
{
    public class Sqlite : IDataBase
    {

        public Sqlite() { }

        public dynamic conexao()
        {
            return new SqliteConnection("Data Source=selecao.db; Version = 3;");
        }

        public dynamic executarQuery(dynamic conexao, dynamic comando)
        {
            conexao.Open();
            int result = comando.ExecuteNonQuery();
            conexao.Close();

            return result;
        }
    }

}
