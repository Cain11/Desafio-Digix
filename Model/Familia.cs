namespace DesafioDigixx.Model
{
    public class Familia
    {
        public string Id { get; set; }
        public List<Pessoa> Pessoas { get; set; }
        public List<Renda> Rendas { get; set; }
        public string Status { get; set; }
        public int Pontuacao { get; set; }

        Dictionary<int[], int> CriteriosPorRenda = new Dictionary<int[], int>();
        Dictionary<int[], int> CriteriosPorDependente = new Dictionary<int[], int>();

        public Familia(string id, List<Pessoa> pessoas, List<Renda> rendas, string status, int pontuacao) : this()
        {
            Id = id;
            Pessoas = pessoas;
            Rendas = rendas;
            Status = status;
            Pontuacao = pontuacao;
        }

        public Familia()
        {
            configuraCriterios();
        }

        public void configuraCriterios()
        {
            //adiciona os critérios de avaliação por limite de renda
            //quanto menor a renda, maior os pontos
            CriteriosPorRenda.Add(new int[2] { 0, 900 }, 5);
            CriteriosPorRenda.Add(new int[2] { 901, 1500 }, 3);
            CriteriosPorRenda.Add(new int[2] { 1501, int.MaxValue }, 1);

            //adiciona os critérios de avaliação de acordo com a quantidade de dependentes
            //quanto maior a quantidade de dependentes, maior os pontos
            CriteriosPorDependente.Add(new int[2] { 1, 2 }, 2);
            CriteriosPorDependente.Add(new int[2] { 3, int.MaxValue }, 3);
        }

        public void calculaPontuacaoTotal()
        {
            Pontuacao += calculaPontosPorRendaTotal();
            Pontuacao += calculaPontosPorQtdDependentes();
        }

        public int calculaPontosPorRendaTotal()
        {
            float rendaTotal = calculaRendaTotal();
            foreach (KeyValuePair<int[], int> criterio in CriteriosPorRenda)
            {
                int valorMinimo = criterio.Key[0];
                int valorMaximo = criterio.Key[1];
                if (valorMinimo <= rendaTotal && valorMaximo >= rendaTotal) return criterio.Value;
            }

            //valor default caso não se adeque em nenhum do dicionario de PontosPorRenda
            return 0;
        }

        public int calculaPontosPorQtdDependentes()
        {
            int numeroDeDependentes = getQuantidadeDependentes();

            foreach (KeyValuePair<int[], int> criterio in CriteriosPorDependente)
            {
                int valorMinimo = criterio.Key[0];
                int valorMaximo = criterio.Key[1];
                if (valorMinimo <= numeroDeDependentes && valorMaximo >= numeroDeDependentes) return criterio.Value;
            }

            //valor default caso não se adeque em nenhum do dicionario de PontosPorDependente
            return 0;
        }

        public int getQuantidadeDependentes()
        {
            List<Pessoa> dependentes = Pessoas.FindAll(x => x.Tipo == "Dependente" && dependenteValido(x));
            return dependentes.Count();
        }

        public bool dependenteValido(Pessoa pessoa)
        {
            return pessoa.getIdade() >= 18;
        }

        public float calculaRendaTotal()
        {
            float rendaTotal = 0;
            foreach (Renda r in Rendas) { rendaTotal += r.Valor; }

            return rendaTotal;
        }
    }
}
