using E_Agenda1._0_ConsoleApp1.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Contato> _repositorioContato;
        private readonly Notificador _notificador;

        public TelaCadastroContato(IRepositorio<Contato> repositorioContato, Notificador notificador)
            : base("Cadastro de Contatos")
        {
            _repositorioContato = repositorioContato;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Contato");

            Contato novoContato = ObterContato();

            _repositorioContato.Inserir(novoContato);

            _notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Contato cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();

            Contato contatoAtualizado = ObterContato();

            bool conseguiuEditar = _repositorioContato.Editar(numeroContato, contatoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Contato editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContatosRegistrados = VisualizarRegistros("Pesquisando");

            if (temContatosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Contato cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioContato.Excluir(numeroContato);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Contato excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contatos");

            List<Contato> contatos = _repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }

        private Contato ObterContato()
        {
            Console.Write("Digite o nome do Contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite a empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();            
            
            return new Contato(nome, email, telefone, empresa, cargo);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Funcionário que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioContato.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Contato não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
