using AchadosPerdidos.Domain.Entities;

namespace AchadosPerdidos.Infrastructure.Persistence
{
    public static class InMemoryRepository
    {
        public static List<ObjetoEncontrado> Objetos { get; } = new();
        public static List<Pessoa> Pessoas { get; } = new();
        public static List<SolicitacaoRetirada> Solicitacoes { get; } = new();

        static InMemoryRepository()
        {
            if (!Pessoas.Any())
            {
                Pessoas.Add(new Pessoa("Alice Silva", "12345678900", "Aluno") { Id = 1 });
                Pessoas.Add(new Pessoa("Bruno Souza", "98765432100", "Professor") { Id = 2 });
                Pessoas.Add(new Pessoa("Carla Lima", "55544433322", "Funcionário") { Id = 3 });
            }
        }

        public static void AdicionarObjeto(ObjetoEncontrado objeto)
        {
            objeto.Id = Objetos.Count + 1;
            Objetos.Add(objeto);
        }

        public static void AdicionarSolicitacao(SolicitacaoRetirada solicitacao)
        {
            solicitacao.Id = Solicitacoes.Count + 1;
            Solicitacoes.Add(solicitacao);
        }

        public static ObjetoEncontrado? ObterObjetoPorId(int id)
        {
            return Objetos.FirstOrDefault(x => x.Id == id);
        }

        public static Pessoa? ObterPessoaPorId(int id)
        {
            return Pessoas.FirstOrDefault(x => x.Id == id);
        }
    }
}
