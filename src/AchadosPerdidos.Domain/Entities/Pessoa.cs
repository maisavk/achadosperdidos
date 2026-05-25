namespace AchadosPerdidos.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma pessoa no sistema (aluno, professor, funcionário, etc.)
    /// </summary>
    public class Pessoa
    {
        /// <summary>
        /// Identificador único da pessoa
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo da pessoa
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Número de registro acadêmico (matrícula) ou CPF da pessoa
        /// </summary>
        public string RegistroOuCpf { get; private set; }

        /// <summary>
        /// Tipo de pessoa (Aluno, Professor, Funcionário, etc.)
        /// </summary>
        public string Tipo { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância da classe Pessoa
        /// </summary>
        /// <param name="nome">Nome completo da pessoa</param>
        /// <param name="registroOuCpf">Registro acadêmico ou CPF</param>
        /// <param name="tipo">Tipo de pessoa (Aluno, Professor, Funcionário, etc.)</param>
        /// <exception cref="ArgumentException">Lançada quando algum campo obrigatório está vazio</exception>
        public Pessoa(string nome, string registroOuCpf, string tipo)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da pessoa não pode estar vazio.", nameof(nome));

            if (string.IsNullOrWhiteSpace(registroOuCpf))
                throw new ArgumentException("O registro ou CPF não pode estar vazio.", nameof(registroOuCpf));

            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("O tipo de pessoa não pode estar vazio.", nameof(tipo));

            Nome = nome;
            RegistroOuCpf = registroOuCpf;
            Tipo = tipo;
        }
    }
}
