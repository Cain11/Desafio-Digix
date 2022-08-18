namespace DesafioDigixx.DataBase
{
    public interface IDataBase
    {
        public dynamic conexao();
        public dynamic executarQuery(dynamic conexao, dynamic comando);
    }
}
