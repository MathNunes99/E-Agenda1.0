using E_Agenda1._0_ConsoleApp1.Compartilhado;
using E_Agenda1._0_ConsoleApp1.ModuloContato;
using E_Agenda1._0_ConsoleApp1.ModuloItens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Tarefa> _repositorioTarefa;
        private readonly Notificador _notificador;
        private readonly IRepositorio<Itens> _repositorioItens;
        private readonly TelaCadastroItens _telaCadastroItens;

        public TelaCadastroTarefa(
            IRepositorio<Tarefa> repositorioTarefa, 
            Notificador notificador,
            TelaCadastroItens telaCadastroItens,
            IRepositorio<Itens> repositorioItens) : base("Cadastro de Tarefas")
        {
            this._repositorioTarefa = repositorioTarefa;
            this._notificador = notificador;
            this._repositorioItens = repositorioItens;
            this._telaCadastroItens = telaCadastroItens;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Tarefas");

            Tarefa novaTarefa = ObterTarefa();

            _repositorioTarefa.Inserir(novaTarefa);

            _notificador.ApresentarMensagem("Tarefa cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Tarefa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroRegistro();

            Tarefa TarefaAtualizada = ObterTarefa();

            Tarefa TarefaAntiga = _repositorioTarefa.SelecionarRegistro(numeroTarefa);

            TarefaAtualizada._dataCriacao = TarefaAntiga._dataCriacao;

            bool conseguiuEditar = _repositorioTarefa.Editar(numeroTarefa, TarefaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasRegistradas = VisualizarRegistros("Pesquisando");

            if (temTarefasRegistradas == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Tarefa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroFuncionario = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioTarefa.Excluir(numeroFuncionario);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa excluída com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            Console.WriteLine("1 - Deseja ver Tarefas Pendetes");
            Console.WriteLine("2 - Deseja ver Tarefas Completas");
            Console.WriteLine();
            Console.Write("- ");
            string opcao = Console.ReadLine();
            List<Tarefa> tarefas = null;
            switch (opcao)
            {
                case "1":
                    tarefas = _repositorioTarefa.Filtrar(x => x.Percentual() < 100);
                    break;
                case "2":
                    tarefas = _repositorioTarefa.Filtrar(x => x.Percentual() >= 100);
                    break;
            }            

            if (tarefas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        private Tarefa ObterTarefa()
        {
            int prioridade = 0;
            while (prioridade < 1 || prioridade > 3)
            {
                Console.WriteLine("Digite a Prioridade da Tarefa: ");
                Console.WriteLine();
                Console.WriteLine("Sendo: \n1-Alta\n2-Media\n3-Baixa");
                Console.WriteLine();
                Console.Write("Prioridade: ");
                int.TryParse(Console.ReadLine(), out prioridade);
            }

            Console.Write("Digite o Titulo da Tarefa: ");
            string titulo = Console.ReadLine();

            Console.WriteLine("Digite a quantidade de Itens que será cadastrado: ");
            Console.Write("- ");
            int.TryParse(Console.ReadLine(), out int qtdItens);

            List<Itens> itensCadastrados = new List<Itens>();
            Console.WriteLine();
            
            for (int i = 0; i < qtdItens; i++)
            {
                Console.WriteLine("Digite um novo Iten para tarefa");
                Console.Write("-");
                Itens iten = _telaCadastroItens.InserirIten();
                itensCadastrados.Add(iten);
                if (iten.titulo == null)
                {
                    iten.titulo = titulo;
                }                
            }
            
            return new Tarefa(prioridade, titulo, itensCadastrados);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Tarefa que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioTarefa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Tarefa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        
    }
}
