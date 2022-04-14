using E_Agenda1._0_ConsoleApp1.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        string Inserir(T entidade);
        public Contato RetornarCtt(int idSelecionado);
        bool Editar(int idSelecionado, T novaEntidade);
        bool Editar(int idSelecionado);
        bool Excluir(int idSelecionado);
        bool ExisteRegistro(int idSelecionado);
        T SelecionarRegistro(int idSelecionado);
        T SelecionarRegistro(Predicate<T> condicao);
        List<T> Filtrar(Predicate<T> condicao);
        List<T> SelecionarTodos();

    }
}
