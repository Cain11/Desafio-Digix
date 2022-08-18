using DesafioDigixx.DataBase;
using DesafioDigixx.Model;
using Microsoft.AspNetCore.Mvc;
using static DesafioDigixx.Model.Enum;

namespace DesafioDigixx.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SelecaoController : ControllerBase
    {

        [HttpPost]
        [Route("/familia/adicionar")]
        [Produces("application/json")]
        public ObjectResult AdicionarFamilia(Familia familia)
        {
            if (familia.Status == ((int)StatusFamilia.CADASTRO_VALIDO).ToString())
            {
                try
                {
                    FamiliaDB familiaDB = new FamiliaDB();
                    PessoaDB pessoaDB = new PessoaDB();
                    RendaDB rendaDB = new RendaDB();

                    familiaDB.adicionarFamilia(familia);

                    foreach (Pessoa pessoa in familia.Pessoas)
                    {
                        pessoaDB.adicionarPessoa(pessoa, familia.Id);
                    }

                    foreach (Renda renda in familia.Rendas)
                    {
                        rendaDB.adicionarRenda(renda);
                    }

                    return Ok(new { mensagem = "Familia adicionada" });
                }
                catch (Exception e)
                {
                    return Problem(e.Message, null, 500, "Exceção ao adicionar familia");
                }
            }
            else
            {
                return Problem("Familia está com um status não permitido", null, 400, "Familia não pode receber o beneficio!");
            }
        }


        [HttpPost]
        [Route("/familia/calcularPontuacao")]
        [Produces("application/json")]
        public ObjectResult CalcularPontuacao(Familia familia)
        {
            try
            {
                familia.calculaPontuacaoTotal();

                dynamic resultadoFamilia = new
                {
                    pontosPorQtdDependentes = familia.calculaPontosPorQtdDependentes(),
                    pontosPorRendaTotal = familia.calculaPontosPorRendaTotal(),
                    pontuacaoTotal = familia.Pontuacao
                };

                return Ok(resultadoFamilia);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, 500, "Exceção ao calcular pontuação da Familia");
            }
        }
    }
}

