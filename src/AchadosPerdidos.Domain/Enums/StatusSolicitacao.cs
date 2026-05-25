namespace AchadosPerdidos.Domain.Enums
{
    /// <summary>
    /// Enum que representa o status de uma solicitação de retirada de objeto
    /// </summary>
    public enum StatusSolicitacao
    {
        /// <summary>
        /// Solicitação aguardando análise
        /// </summary>
        Pendente = 1,

        /// <summary>
        /// Solicitação foi aprovada
        /// </summary>
        Aprovada = 2,

        /// <summary>
        /// Solicitação foi rejeitada
        /// </summary>
        Rejeitada = 3
    }
}
