using DesafioDigixx.Model;

namespace DesafioDigixx.DataBase
{
    public class PessoaDB
    {
        private IDataBase Db = new Sqlite();

        public int adicionarPessoa(Pessoa pessoa, string familiaId)
        {
            try
            {
                var conexao = Db.conexao();
                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO pessoa(id, nome, tipo, data_nascimento, familia_id) 
                                        VALUES (@id, @nome, @tipo, @data_nascimento, @familia_id)";

                    cmd.Parameters.AddWithValue("@Id", pessoa.Id);
                    cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@tipo", pessoa.Tipo);
                    cmd.Parameters.AddWithValue("@data_nascimento", pessoa.DataDeNascimento);
                    cmd.Parameters.AddWithValue("@familia_id", familiaId);

                    return Db.executarQuery(conexao, cmd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }


        public Pessoa[] buscarPessoaPorIdFamilia(string idFamilia)
        {
            var conexao = Db.conexao();
            using (var cmd = conexao.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM familia where(id_familia = @idFamilia)";
                cmd.Parameters.AddWithValue("@idFamilia", idFamilia);

                return Db.executarQuery(conexao, cmd);
            }
        }

        public Pessoa buscarPessoaPorId(string id)
        {
            var conexao = Db.conexao();
            using (var cmd = conexao.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM familia where(id = @id)";
                cmd.Parameters.AddWithValue("@id", id);

                return Db.executarQuery(conexao, cmd);
            }
        }
    }
}
