using DesafioDigixx.Model;
using System.Data.SqlTypes;

namespace DesafioDigixx.DataBase
{
    public class RendaDB
    {
        private IDataBase Db = new Sqlite();

        public int adicionarRenda(Renda renda)
        {
            try
            {
                var conexao = Db.conexao();
                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO renda(id_pessoa, valor) VALUES (@id_pessoa, @valor)";
                    cmd.Parameters.AddWithValue("@id_pessoa", renda.PessoaId);
                    cmd.Parameters.AddWithValue("@valor", renda.Valor);

                    return Db.executarQuery(conexao, cmd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public Renda[] buscarRendaPorIdPessoa(string idPessoa)
        {
            var conexao = Db.conexao();
            using (var cmd = conexao.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM familia where(id_pessoa = @idPessoa)";
                cmd.Parameters.AddWithValue("@idPessoa", idPessoa);

                return Db.executarQuery(conexao, cmd);
            }
        }
    }
}
