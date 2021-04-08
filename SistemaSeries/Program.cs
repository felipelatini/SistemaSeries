using System;

namespace SistemaSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSerie();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Seja bem vindo ao cadastro de séries!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void ListarSerie()
        {
            Console.WriteLine("Listar séries");
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach(var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: = {1} - {2}", serie.retornaId(), serie.retornaTitulo(), excluido ? "*Excluido*" : "");
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir série");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("Digite o gênero entre as opções acima: ");
            string entradaGenero    = Console.ReadLine();
            try 
            { 
                int i = int.Parse(entradaGenero);
                if (i > 13)
                {
                    Console.WriteLine("Gênero inválido!!!");
                    return;
                }
            } 
            catch 
            {
                Console.WriteLine("Gênero inválido!!!");
                return;
            }

            Console.WriteLine("Digite o título da série: ");
            string entradaTitulo    = Console.ReadLine();
            if (string.IsNullOrEmpty(entradaTitulo))
            {
                Console.WriteLine("Título inválido!!!");
                return;
            }

            Console.WriteLine("Digite o ano de ínicio da série: ");
            string entradaAno       = Console.ReadLine();
            try 
            { 
                int i = int.Parse(entradaAno);
                if (i.ToString().Length != 4)
                {
                    Console.WriteLine("Ano inválido!!!");
                    return;
                }
            } 
            catch 
            {
                Console.WriteLine("Ano inválido!!!");
                return;
            }

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();
            if (string.IsNullOrEmpty(entradaDescricao))
            {
                Console.WriteLine("Descrição inválida!!!");
                return;
            }

            Serie novaSerie         = new(id       : repositorio.ProximoId(),
                                          genero   : (Genero)int.Parse(entradaGenero),
                                          titulo   : entradaTitulo,
                                          ano      : int.Parse(entradaAno),
                                          descricao: entradaDescricao);

            repositorio.Insere(novaSerie);

            Console.WriteLine("Série inserida com sucesso!!!");
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Atualizar a série");

            Console.WriteLine("Digite o id da série: ");
            string indiceId            = Console.ReadLine();
            try 
            { 
                int i = int.Parse(indiceId);

                if (i < 0)
                {
                    Console.WriteLine("ID inválido!!!");
                    return;
                }
            } 
            catch 
            {
                Console.WriteLine("ID inválido!!!");
                return;
            }

            var serie                  = repositorio.RetornarPorId(int.Parse(indiceId));
            if (serie.Id == -1)
            {
                Console.WriteLine("Não foi encontrado uma série com o ID informado!!!");
                return;
            }

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("Digite o gênero entre as opções acima: ");
            string entradaGenero    = Console.ReadLine();
            try 
            { 
                int i = int.Parse(entradaGenero);
                if (i > 13)
                {
                    Console.WriteLine("Gênero inválido!!!");
                    return;
                }
            } 
            catch 
            {
                Console.WriteLine("Gênero inválido!!!");
                return;
            }

            Console.WriteLine("Digite o título da série: ");
            string entradaTitulo    = Console.ReadLine();
            if (string.IsNullOrEmpty(entradaTitulo))
            {
                Console.WriteLine("Título inválido!!!");
                return;
            }

            Console.WriteLine("Digite o ano de ínicio da série: ");
            string entradaAno       = Console.ReadLine();
            try 
            { 
                int i = int.Parse(entradaAno);
                if (i.ToString().Length != 4)
                {
                    Console.WriteLine("Ano inválido!!!");
                    return;
                }
            } 
            catch 
            {
                Console.WriteLine("Ano inválido!!!");
                return;
            }

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();
            if (string.IsNullOrEmpty(entradaDescricao))
            {
                Console.WriteLine("Descrição inválida!!!");
                return;
            }

            Serie atualizaSerie     = new(id       : int.Parse(indiceId),
                                          genero   : (Genero)int.Parse(entradaGenero),
                                          titulo   : entradaTitulo,
                                          ano      : int.Parse(entradaAno),
                                          descricao: entradaDescricao);

            repositorio.Atualiza(int.Parse(indiceId), atualizaSerie);

            Console.WriteLine("Série alterada com sucesso!!!");
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Digite o id da série: ");
            string indiceId = Console.ReadLine();
            try 
            { 
                int i = int.Parse(indiceId);

                if (i < 0)
                {
                    Console.WriteLine("ID inválido!!!");
                    return;
                }
            } 
            catch 
            {
                Console.WriteLine("ID inválido!!!");
                return;
            }

            var serie       = repositorio.RetornarPorId(int.Parse(indiceId));
            if (serie.Id == -1)
            {
                Console.WriteLine("Não foi encontrado uma série com o ID informado!!!");
                return;
            }

            repositorio.Excluir(int.Parse(indiceId));

            Console.WriteLine("Série excluida com sucesso!!!");
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie       = repositorio.RetornarPorId(indiceSerie);
            if (serie.Id == -1)
            {
                Console.WriteLine("Não foi encontrado uma série com o ID informado!!!");
                return;
            }

            Console.WriteLine(serie);
        }
    }
}
