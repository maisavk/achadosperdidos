namespace AchadosPerdidos.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa um período de tempo entre duas datas
    /// </summary>
    public class Periodo
    {
        /// <summary>
        /// Data de início do período
        /// </summary>
        public DateTime DataInicio { get; private set; }

        /// <summary>
        /// Data de fim do período
        /// </summary>
        public DateTime DataFim { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância da classe Periodo
        /// </summary>
        /// <param name="dataInicio">Data de início do período</param>
        /// <param name="dataFim">Data de fim do período</param>
        /// <exception cref="ArgumentException">Lançada quando a data de fim é menor ou igual à data de início</exception>
        public Periodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataFim <= dataInicio)
            {
                throw new ArgumentException("A data de fim deve ser maior que a data de início.", nameof(dataFim));
            }

            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        /// <summary>
        /// Calcula a duração em dias do período
        /// </summary>
        /// <returns>Número de dias do período</returns>
        public int ObterDuracaoEmDias()
        {
            return (int)(DataFim - DataInicio).TotalDays;
        }

        /// <summary>
        /// Verifica se uma data está dentro do período
        /// </summary>
        /// <param name="data">Data a ser verificada</param>
        /// <returns>True se a data está dentro do período, false caso contrário</returns>
        public bool EstaNoPerido(DateTime data)
        {
            return data >= DataInicio && data <= DataFim;
        }

        public override bool Equals(object obj)
        {
            if (obj is Periodo periodo)
            {
                return DataInicio == periodo.DataInicio && DataFim == periodo.DataFim;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DataInicio, DataFim);
        }

        public override string ToString()
        {
            return $"De {DataInicio:dd/MM/yyyy} até {DataFim:dd/MM/yyyy}";
        }
    }
}
