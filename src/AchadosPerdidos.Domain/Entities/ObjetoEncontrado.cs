using AchadosPerdidos.Domain.Enums;

namespace AchadosPerdidos.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um objeto encontrado no campus
    /// </summary>
    public class ObjetoEncontrado
    {
        /// <summary>
        /// Identificador único do objeto
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descrição detalhada do objeto encontrado
        /// </summary>
        public string Descricao { get; private set; }

        /// <summary>
        /// Categoria do objeto (eletrônicos, roupas, documentos, etc.)
        /// </summary>
        public string Categoria { get; private set; }

        /// <summary>
        /// Local onde o objeto foi encontrado
        /// </summary>
        public string Local { get; private set; }

        /// <summary>
        /// Data em que o objeto foi registrado no sistema
        /// </summary>
        public DateTime DataRegistro { get; private set; }

        /// <summary>
        /// Estado de conservação do objeto
        /// </summary>
        public string EstadoConservacao { get; private set; }

        /// <summary>
        /// Situação atual do objeto no sistema
        /// </summary>
        public SituacaoObjeto Situacao { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância da classe ObjetoEncontrado
        /// </summary>
        /// <param name="descricao">Descrição do objeto</param>
        /// <param name="categoria">Categoria do objeto</param>
        /// <param name="local">Local onde foi encontrado</param>
        /// <param name="estadoConservacao">Estado de conservação</param>
        /// <exception cref="ArgumentException">Lançada quando algum campo obrigatório está vazio</exception>
        public ObjetoEncontrado(string descricao, string categoria, string local, string estadoConservacao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição do objeto não pode estar vazia.", nameof(descricao));

            if (string.IsNullOrWhiteSpace(categoria))
                throw new ArgumentException("A categoria do objeto não pode estar vazia.", nameof(categoria));

            if (string.IsNullOrWhiteSpace(local))
                throw new ArgumentException("O local de encontro não pode estar vazio.", nameof(local));

            if (string.IsNullOrWhiteSpace(estadoConservacao))
                throw new ArgumentException("O estado de conservação não pode estar vazio.", nameof(estadoConservacao));

            Descricao = descricao;
            Categoria = categoria;
            Local = local;
            EstadoConservacao = estadoConservacao;
            DataRegistro = DateTime.Now;
            Situacao = SituacaoObjeto.AguardandoRetirada;
        }

        /// <summary>
        /// Altera a situação do objeto
        /// </summary>
        /// <param name="novaSituacao">Nova situação do objeto</param>
        public void AlterarSituacao(SituacaoObjeto novaSituacao)
        {
            Situacao = novaSituacao;
        }

        /// <summary>
        /// Verifica se o objeto está disponível para retirada
        /// </summary>
        /// <returns>True se está em estado de aguardando retirada, false caso contrário</returns>
        public bool EstaDisponivel()
        {
            return Situacao == SituacaoObjeto.AguardandoRetirada;
        }
    }
}
