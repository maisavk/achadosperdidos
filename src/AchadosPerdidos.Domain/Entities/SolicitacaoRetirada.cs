using AchadosPerdidos.Domain.Enums;

namespace AchadosPerdidos.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma solicitação de retirada de um objeto encontrado
    /// </summary>
    public class SolicitacaoRetirada
    {
        /// <summary>
        /// Identificador único da solicitação
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador do objeto encontrado
        /// </summary>
        public int ObjetoId { get; private set; }

        /// <summary>
        /// Identificador da pessoa solicitante (que quer retirar o objeto)
        /// </summary>
        public int SolicitanteId { get; private set; }

        /// <summary>
        /// Data em que a solicitação foi realizada
        /// </summary>
        public DateTime DataSolicitacao { get; private set; }

        /// <summary>
        /// Descrição ou validação da solicitação (ex: comprovante de propriedade)
        /// </summary>
        public string DescricaoValidacao { get; private set; }

        /// <summary>
        /// Status atual da solicitação
        /// </summary>
        public StatusSolicitacao Status { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância da classe SolicitacaoRetirada
        /// </summary>
        /// <param name="objetoId">Id do objeto encontrado</param>
        /// <param name="solicitanteId">Id da pessoa solicitante</param>
        /// <param name="descricaoValidacao">Descrição ou validação da solicitação</param>
        /// <exception cref="ArgumentException">Lançada quando algum campo obrigatório está vazio ou inválido</exception>
        public SolicitacaoRetirada(int objetoId, int solicitanteId, string descricaoValidacao)
        {
            if (objetoId <= 0)
                throw new ArgumentException("O identificador do objeto deve ser maior que zero.", nameof(objetoId));

            if (solicitanteId <= 0)
                throw new ArgumentException("O identificador do solicitante deve ser maior que zero.", nameof(solicitanteId));

            if (string.IsNullOrWhiteSpace(descricaoValidacao))
                throw new ArgumentException("A descrição de validação não pode estar vazia.", nameof(descricaoValidacao));

            ObjetoId = objetoId;
            SolicitanteId = solicitanteId;
            DescricaoValidacao = descricaoValidacao;
            DataSolicitacao = DateTime.Now;
            Status = StatusSolicitacao.Pendente;
        }

        /// <summary>
        /// Aprova a solicitação de retirada
        /// </summary>
        public void Aprovar()
        {
            Status = StatusSolicitacao.Aprovada;
        }

        /// <summary>
        /// Rejeita a solicitação de retirada
        /// </summary>
        public void Rejeitar()
        {
            Status = StatusSolicitacao.Rejeitada;
        }

        /// <summary>
        /// Verifica se a solicitação está pendente de análise
        /// </summary>
        /// <returns>True se o status é Pendente, false caso contrário</returns>
        public bool EstaPendente()
        {
            return Status == StatusSolicitacao.Pendente;
        }

        /// <summary>
        /// Verifica se a solicitação foi aprovada
        /// </summary>
        /// <returns>True se o status é Aprovada, false caso contrário</returns>
        public bool FoiAprovada()
        {
            return Status == StatusSolicitacao.Aprovada;
        }
    }
}
