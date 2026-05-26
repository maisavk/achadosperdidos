using AchadosPerdidos.Application.Services;
using AchadosPerdidos.Domain.Enums;
using AchadosPerdidos.Infrastructure.Persistence;

var repository = new InMemoryRepository();
var service = new SistemaService(repository);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Sistema Achados e Perdidos ===");
    Console.WriteLine("1. Cadastrar Objeto Encontrado");
    Console.WriteLine("2. Consultar Objetos Cadastrados");
    Console.WriteLine("3. Registrar Solicitação de Retirada");
    Console.WriteLine("4. Confirmar Devolução de Objeto");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");
    var opcao = Console.ReadLine();

    try
    {
        switch (opcao)
        {
            case "1":
                CadastrarObjeto(service);
                break;
            case "2":
                ConsultarObjetos(service);
                break;
            case "3":
                RegistrarSolicitacao(service);
                break;
            case "4":
                ConfirmarDevolucao(service);
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Opção inválida. Pressione ENTER para continuar...");
                Console.ReadLine();
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
        Console.WriteLine("Pressione ENTER para voltar ao menu...");
        Console.ReadLine();
    }
}

void CadastrarObjeto(SistemaService service)
{
    Console.Clear();
    Console.WriteLine("=== Cadastrar Objeto Encontrado ===");
    Console.Write("Descrição: ");
    var descricao = Console.ReadLine() ?? string.Empty;
    Console.Write("Categoria: ");
    var categoria = Console.ReadLine() ?? string.Empty;
    Console.Write("Local: ");
    var local = Console.ReadLine() ?? string.Empty;
    Console.Write("Estado de Conservação: ");
    var estadoConservacao = Console.ReadLine() ?? string.Empty;

    var objeto = service.RegistrarObjetoEncontrado(descricao, categoria, local, estadoConservacao);
    Console.WriteLine($"Objeto cadastrado com sucesso! Id = {objeto.Id}");
    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

void ConsultarObjetos(SistemaService service)
{
    Console.Clear();
    Console.WriteLine("=== Consultar Objetos Cadastrados ===");
    Console.Write("Filtrar por categoria (enter para não filtrar): ");
    var categoria = Console.ReadLine();
    Console.Write("Filtrar por local (enter para não filtrar): ");
    var local = Console.ReadLine();
    Console.Write("Filtrar por situação (AguardandoRetirada, EmAnalise, Devolvido, Descartado; enter para não filtrar): ");
    var situacaoTexto = Console.ReadLine();

    SituacaoObjeto? situacao = null;
    if (!string.IsNullOrWhiteSpace(situacaoTexto) && Enum.TryParse<SituacaoObjeto>(situacaoTexto.Trim(), true, out var situacaoParseada))
    {
        situacao = situacaoParseada;
    }

    var resultados = service.ConsultarObjetos(categoria, local, situacao).ToList();

    Console.WriteLine();
    if (!resultados.Any())
    {
        Console.WriteLine("Nenhum objeto encontrado com os filtros selecionados.");
    }
    else
    {
        foreach (var objeto in resultados)
        {
            Console.WriteLine($"Id: {objeto.Id}");
            Console.WriteLine($"Descrição: {objeto.Descricao}");
            Console.WriteLine($"Categoria: {objeto.Categoria}");
            Console.WriteLine($"Local: {objeto.Local}");
            Console.WriteLine($"Estado de Conservação: {objeto.EstadoConservacao}");
            Console.WriteLine($"Situação: {objeto.Situacao}");
            Console.WriteLine($"Data de Registro: {objeto.DataRegistro:dd/MM/yyyy HH:mm}");
            Console.WriteLine(new string('-', 40));
        }
    }

    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

void RegistrarSolicitacao(SistemaService service)
{
    Console.Clear();
    Console.WriteLine("=== Registrar Solicitação de Retirada ===");
    ExibirObjetos(service);

    Console.Write("Id do objeto: ");
    var objetoId = LerInteiro();

    Console.WriteLine();
    Console.WriteLine("Usuários disponíveis:");
    foreach (var pessoa in service.ObterPessoasCadastradas())
    {
        Console.WriteLine($"{pessoa.Id} - {pessoa.Nome} ({pessoa.Tipo})");
    }

    Console.Write("Id do solicitante: ");
    var solicitanteId = LerInteiro();
    Console.Write("Descrição de validação: ");
    var descricaoValidacao = Console.ReadLine() ?? string.Empty;

    var solicitacao = service.RegistrarSolicitacaoRetirada(objetoId, solicitanteId, descricaoValidacao);
    Console.WriteLine($"Solicitação registrada com sucesso! Id = {solicitacao.Id}");
    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

void ConfirmarDevolucao(SistemaService service)
{
    Console.Clear();
    Console.WriteLine("=== Confirmar Devolução de Objeto ===");
    ExibirObjetos(service);

    Console.Write("Id do objeto a devolver: ");
    var objetoId = LerInteiro();

    var objeto = service.ConfirmarDevolucao(objetoId);
    Console.WriteLine($"Objeto '{objeto.Descricao}' alterado para situação '{objeto.Situacao}'.");
    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

void ExibirObjetos(SistemaService service)
{
    var objetos = service.ConsultarObjetos().ToList();
    if (!objetos.Any())
    {
        Console.WriteLine("Nenhum objeto cadastrado ainda.");
        return;
    }

    foreach (var objeto in objetos)
    {
        Console.WriteLine($"{objeto.Id} - {objeto.Descricao} | {objeto.Categoria} | {objeto.Local} | {objeto.Situacao}");
    }
}

int LerInteiro()
{
    while (true)
    {
        var texto = Console.ReadLine();
        if (int.TryParse(texto, out var valor) && valor > 0)
            return valor;

        Console.Write("Valor inválido. Informe um número inteiro positivo: ");
    }
}
