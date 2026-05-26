using AchadosPerdidos.Domain.Entities;

namespace AchadosPerdidos.Domain.Interfaces
{
    public interface IRepository
    {
        IReadOnlyCollection<ObjetoEncontrado> Objetos { get; }
        IReadOnlyCollection<Pessoa> Pessoas { get; }
        IReadOnlyCollection<SolicitacaoRetirada> Solicitacoes { get; }

        void AdicionarObjeto(ObjetoEncontrado objeto);
        void AdicionarSolicitacao(SolicitacaoRetirada solicitacao);
        ObjetoEncontrado? ObterObjetoPorId(int id);
        Pessoa? ObterPessoaPorId(int id);
    }
}
