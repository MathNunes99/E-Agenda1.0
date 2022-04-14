using E_Agenda1._0_ConsoleApp1.Compartilhado;
using E_Agenda1._0_ConsoleApp1.ModuloContato;
using E_Agenda1._0_ConsoleApp1.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase, ITelaCadastravel
    {
        private IRepositorio<Compromisso> _repositorioCompromisso;
        private Notificador _notificador;
        private TelaCadastroContato _telaCadastroContato;
        private IRepositorio<Contato> _repositorioContato;        

        public TelaCadastroCompromisso(
            IRepositorio<Compromisso> repositorioCompromisso,
            Notificador notificador,
            TelaCadastroContato telaCadastroContato,
            IRepositorio<Contato> repositorioContato) : base("Cadastro de Compromissos")
        {
            this._repositorioCompromisso = repositorioCompromisso;
            this._notificador = notificador;
            this._telaCadastroContato = telaCadastroContato;
            this._repositorioContato = repositorioContato;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Funcionário");

            Compromisso novoFuncionrio = ObterCompromisso();

            _repositorioCompromisso.Inserir(novoFuncionrio);

            _notificador.ApresentarMensagem("Compromisso cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Compromisso");

            bool temCompromissosCadastrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Compromisso cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroRegistro();

            Compromisso compromissoAtualizado = ObterCompromisso();

            bool conseguiuEditar = _repositorioCompromisso.Editar(numeroCompromisso, compromissoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Compromisso editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temCompromissosRegistrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum compromissos cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioCompromisso.Excluir(numeroCompromisso);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Compromisso excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Compromissos");

            List<Compromisso> compromissos = _repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();
            return true;
        }

        private Compromisso ObterCompromisso()
        {
            Console.Write("Digite o Assunto: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite a Data: ");
            string data = Console.ReadLine();

            Console.Write("Digite o Local: ");
            string local = Console.ReadLine();

            Console.Write("Digite Hora de inicio: ");
            string inicio = Console.ReadLine();

            Console.Write("Digite Hora de Termino: ");
            string termino = Console.ReadLine();

            Console.WriteLine("com qual contato sera seu compromisso:");
            Console.WriteLine();
            List<Contato> contatos = _repositorioContato.SelecionarTodos();
            Console.WriteLine();

            Contato contatoNovo = null;

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToStringBasic());

            int numeroContato = ObterNumeroRegistroContato();

            Contato contatoCadastrado = _repositorioContato.RetornarCtt(numeroContato);
            
            contatoNovo = _repositorioContato.SelecionarRegistro(numeroContato);

            return new Compromisso(assunto,data,local,inicio,termino, contatoNovo);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Compromisso: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioCompromisso.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Compromisso não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
        public int ObterNumeroRegistroContato()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Contato: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioContato.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Contato não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
