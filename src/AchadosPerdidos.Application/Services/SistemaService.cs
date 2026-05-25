using AchadosPerdidos.Domain.Entities;
using AchadosPerdidos.Domain.Enums;
using AchadosPerdidos.Infrastructure.Persistence;

namespace AchadosPerdidos.Application.Services
{
    public class SistemaService
    {
        public ObjetoEncontrado RegistrarObjetoEncontrado(string descricao, string categoria, string local, string estadoConservacao)
        {
            var objeto = new ObjetoEncontrado(descricao, categoria, local, estadoConservacao);
            InMemoryRepository.AdicionarObjeto(objeto);
            return objeto;
        }

        public IEnumerable<ObjetoEncontrado> ConsultarObjetos(string? categoria = null, string? local = null, SituacaoObjeto? situacao = null)
        {
            return InMemoryRepository.Objetos
                .Where(o => string.IsNullOrWhiteSpace(categoria) || o.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .Where(o => string.IsNullOrWhiteSpace(local) || o.Local.Equals(local, StringComparison.OrdinalIgnoreCase))
                .Where(o => !situacao.HasValue || o.Situacao == situacao.Value)
                .ToList();
        }

        public SolicitacaoRetirada RegistrarSolicitacaoRetirada(int objetoId, int solicitanteId, string descricaoValidacao)
        {
            var objeto = InMemoryRepository.ObterObjetoPorId(objetoId);
            if (objeto is null)
                throw new ArgumentException("Objeto não encontrado.", nameof(objetoId));

            if (objeto.Situacao == SituacaoObjeto.Devolvido)
                throw new InvalidOperationException("Não é possível registrar solicitação: o objeto já foi devolvido.");

            if (objeto.Situacao == SituacaoObjeto.EmAnalise)
                throw new InvalidOperationException("Não é possível registrar solicitação: o objeto já está em análise.");

            var solicitante = InMemoryRepository.ObterPessoaPorId(solicitanteId);
            if (solicitante is null)
                throw new ArgumentException("Solicitante não encontrado.", nameof(solicitanteId));

            if (!DescricaoCombina(objeto, descricaoValidacao))
                throw new InvalidOperationException("A descrição de validação não combina com o objeto informado.");

            var solicitacao = new SolicitacaoRetirada(objetoId, solicitanteId, descricaoValidacao);
            InMemoryRepository.AdicionarSolicitacao(solicitacao);
            objeto.AlterarSituacao(SituacaoObjeto.EmAnalise);
            return solicitacao;
        }

        public ObjetoEncontrado ConfirmarDevolucao(int objetoId)
        {
            var objeto = InMemoryRepository.ObterObjetoPorId(objetoId);
            if (objeto is null)
                throw new ArgumentException("Objeto não encontrado.", nameof(objetoId));

            objeto.AlterarSituacao(SituacaoObjeto.Devolvido);
            return objeto;
        }

        public IEnumerable<Pessoa> ObterPessoasCadastradas()
        {
            return InMemoryRepository.Pessoas;
        }

        private static bool DescricaoCombina(ObjetoEncontrado objeto, string descricaoValidacao)
        {
            if (string.IsNullOrWhiteSpace(descricaoValidacao))
                return false;

            var texto = descricaoValidacao.Trim().ToLowerInvariant();
            return texto.Contains(objeto.Descricao.ToLowerInvariant())
                || texto.Contains(objeto.Categoria.ToLowerInvariant())
                || texto.Contains(objeto.Local.ToLowerInvariant())
                || texto.Contains(objeto.EstadoConservacao.ToLowerInvariant());
        }
    }
}
