using E_Agenda1._0_ConsoleApp1.Compartilhado;
using E_Agenda1._0_ConsoleApp1.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.ModuloItens
{
    public class TelaCadastroItens : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Itens> _repositorioItens;
        private readonly Notificador _notificador;
        private readonly IRepositorio<Tarefa> _repositorioTarefa;
        private readonly TelaCadastroTarefa _telaCadastroTarefa;
        public TelaCadastroItens(IRepositorio<Itens> repositorioItens, Notificador notificador)
            : base("Cadastro de Itens")
        {
            _repositorioItens = repositorioItens;
            _notificador = notificador;            
        }

        public override string MostrarOpcoes()
        {            
            return "2";            
        }
        public Itens InserirIten()
        {
            MostrarTitulo("Cadastro de Itens");

            Itens novoIten = ObterItens();

            _repositorioItens.Inserir(novoIten);

            _notificador.ApresentarMensagem("Iten cadastrado com sucesso!", TipoMensagem.Sucesso);
            
            return novoIten;
        }
        public void Inserir()
        {

        }
        public void Editar()
        {
            MostrarTitulo("Editando Itens");

            bool temItensCadastradas = VisualizarRegistros("Pesquisando");

            if (temItensCadastradas == false)
            {
                _notificador.ApresentarMensagem("Nenhum Iten cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroItens = ObterNumeroRegistro();

            string opcao = MostrarOpcoesEditar();
            
            if (opcao == "1")
            {
                Itens ItensAtualizado = ObterItens();
                bool conseguiuEditar = _repositorioItens.Editar(numeroItens, ItensAtualizado);
                if (!conseguiuEditar)
                    _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
                else
                    _notificador.ApresentarMensagem("Iten editado com sucesso!", TipoMensagem.Sucesso);
            }
            else if(opcao == "2")
            {                
                bool conseguiuEditar = _repositorioItens.Editar(numeroItens);
                if (!conseguiuEditar)
                    _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
                else
                    _notificador.ApresentarMensagem("Iten editado com sucesso!", TipoMensagem.Sucesso);
            }           

            
        }
        private string MostrarOpcoesEditar()
        {
            Console.WriteLine("1- editar descricao");
            Console.WriteLine("2- Concluir Iten");

            string opcao = Console.ReadLine();
            return opcao;
        }
        public void Excluir()
        {
            MostrarTitulo("Excluindo Funcionário");

            bool temItensRegistrados = VisualizarRegistros("Pesquisando");            

            int numeroIten = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioItens.Excluir(numeroIten);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Iten excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Itens");

            List<Itens> itens = _repositorioItens.SelecionarTodos();

            if (itens.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum Iten disponível.", TipoMensagem.Atencao);
                return false;
            }            
            foreach (Itens iten in itens)
            {                              
                Console.WriteLine(iten.ToString());                        
            }
                

            return true;
        }

        private Itens ObterItens()
        {
            Console.Write("Digite uma nova descricao: ");
            string descricao = Console.ReadLine();            

            return new Itens(descricao);
        }        
        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Iten que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioItens.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Iten não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
