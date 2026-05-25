namespace AchadosPerdidos.Domain.Enums
{
    /// <summary>
    /// Enum que representa o estado atual de um objeto encontrado no sistema
    /// </summary>
    public enum SituacaoObjeto
    {
        /// <summary>
        /// Objeto aguardando a retirada pelo proprietário
        /// </summary>
        AguardandoRetirada = 1,

        /// <summary>
        /// Objeto em análise para verificação de autenticidade
        /// </summary>
        EmAnalise = 2,

        /// <summary>
        /// Objeto já foi devolvido ao proprietário
        /// </summary>
        Devolvido = 3,

        /// <summary>
        /// Objeto foi descartado do sistema
        /// </summary>
        Descartado = 4
    }
}
