using DesafioDigixx.Model;

namespace DesafioDigixx.DataBase
{
    public class FamiliaDB
    {
        private IDataBase Db = new Sqlite();

        public int adicionarFamilia(Familia familia)
        {
            try
            {
                var conexao = Db.conexao();
                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO familia(id, status) VALUES (@id, @status)";
                    cmd.Parameters.AddWithValue("@Id", familia.Id);
                    cmd.Parameters.AddWithValue("@status", familia.Status);

                    return Db.executarQuery(conexao, cmd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public dynamic buscarFamiliaPorId(string id)
        {
            var conexao = Db.conexao();
            using (var cmd = conexao.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM familia where(id = @id)";
                cmd.Parameters.AddWithValue("@Id", id);

                return Db.executarQuery(conexao, cmd);
            }
        }
    }
}
