using AchadosPerdidos.Domain.Entities;
using AchadosPerdidos.Domain.Interfaces;

namespace AchadosPerdidos.Infrastructure.Persistence
{
    public class InMemoryRepository : IRepository
    {
        private readonly List<ObjetoEncontrado> _objetos = new();
        private readonly List<Pessoa> _pessoas = new();
        private readonly List<SolicitacaoRetirada> _solicitacoes = new();

        public IReadOnlyCollection<ObjetoEncontrado> Objetos => _objetos;
        public IReadOnlyCollection<Pessoa> Pessoas => _pessoas;
        public IReadOnlyCollection<SolicitacaoRetirada> Solicitacoes => _solicitacoes;

        public InMemoryRepository()
        {
            if (!_pessoas.Any())
            {
                _pessoas.Add(new Pessoa("Alice Silva", "12345678900", "Aluno") { Id = 1 });
                _pessoas.Add(new Pessoa("Bruno Souza", "98765432100", "Professor") { Id = 2 });
                _pessoas.Add(new Pessoa("Carla Lima", "55544433322", "Funcionário") { Id = 3 });
            }
        }

        public void AdicionarObjeto(ObjetoEncontrado objeto)
        {
            objeto.Id = _objetos.Count + 1;
            _objetos.Add(objeto);
        }

        public void AdicionarSolicitacao(SolicitacaoRetirada solicitacao)
        {
            solicitacao.Id = _solicitacoes.Count + 1;
            _solicitacoes.Add(solicitacao);
        }

        public ObjetoEncontrado? ObterObjetoPorId(int id)
        {
            return _objetos.FirstOrDefault(x => x.Id == id);
        }

        public Pessoa? ObterPessoaPorId(int id)
        {
            return _pessoas.FirstOrDefault(x => x.Id == id);
        }
    }
}
