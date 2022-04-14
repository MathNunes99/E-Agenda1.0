using E_Agenda1._0_ConsoleApp1.ModuloCompromisso;
using E_Agenda1._0_ConsoleApp1.ModuloContato;
using E_Agenda1._0_ConsoleApp1.ModuloItens;
using E_Agenda1._0_ConsoleApp1.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private IRepositorio<Itens> repositorioItens;
        private TelaCadastroItens telaCadastroItens;

        private IRepositorio<Tarefa> repositorioTarefa;
        private TelaCadastroTarefa telaCadastroTarefa;

        private IRepositorio<Contato> repositorioContato;
        private TelaCadastroContato telaCadastroContato;

        private IRepositorio<Compromisso> repositorioCompromisso;
        private TelaCadastroCompromisso telaCadastroCompromisso;
        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioItens = new RepositorioItens();
            telaCadastroItens = new TelaCadastroItens(repositorioItens, notificador);

            repositorioTarefa = new RepositorioTarefa();
            telaCadastroTarefa = new TelaCadastroTarefa(repositorioTarefa, notificador,telaCadastroItens,repositorioItens);

            repositorioContato = new RepositorioContato();
            telaCadastroContato = new TelaCadastroContato(repositorioContato,notificador);

            repositorioCompromisso = new RepositorioCompromisso();
            telaCadastroCompromisso = new TelaCadastroCompromisso(repositorioCompromisso, notificador, telaCadastroContato, repositorioContato);
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Controle de E-Agenda");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Tarefas");
            Console.WriteLine("Digite 2 para Gerenciar Itens das Tarefas");
            Console.WriteLine("Digite 3 para Gerenciar Contatos");
            Console.WriteLine("Digite 4 para Gerenciar Compromissos");
            Console.WriteLine();
            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroTarefa;

            else if (opcao == "2")
                tela = telaCadastroItens;

            else if (opcao == "3")
                tela = telaCadastroContato;

            else if (opcao == "4")
                tela = telaCadastroCompromisso;

            else if (opcao == "5")
                tela = null;            

            return tela;
        }
    }
}
