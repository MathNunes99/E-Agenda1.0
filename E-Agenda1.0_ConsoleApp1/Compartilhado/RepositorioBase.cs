using E_Agenda1._0_ConsoleApp1.Compartilhado;
using E_Agenda1._0_ConsoleApp1.ModuloContato;
using E_Agenda1._0_ConsoleApp1.ModuloItens;
using E_Agenda1._0_ConsoleApp1.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1
{
    public class RepositorioBase<T> : IRepositorio<T> where T : EntidadeBase
    {
        protected readonly List<T> registros;

        protected int contadorId;

        public RepositorioBase()
        {
            registros = new List<T>();
        }

        public virtual string Inserir(T entidade)
        {
            entidade.id = ++contadorId;

            registros.Add(entidade);

            return "REGISTRO_VALIDO";
        }

        public bool Editar(int idSelecionado, T novaEntidade)
        {
            foreach (T entidade in registros)
            {
                if (idSelecionado == entidade.id)
                {
                    novaEntidade.id = entidade.id;

                    int posicaoParaEditar = registros.IndexOf(entidade);
                    registros[posicaoParaEditar] = novaEntidade;

                    return true;
                }
            }

            return false;
        }
        public bool Editar(int idSelecionado)
        {
            foreach (T entidade in registros)
            {
                if (idSelecionado == entidade.id)
                {
                    if (registros[idSelecionado - 1] is Itens iten)
                        iten.pendencia = false;

                    return true;
                }
            }

            return false;
        }        
        public Contato RetornarCtt(int idSelecionado)
        {
            Contato contato = null;
            foreach (T entidade in registros)
            {
                if (idSelecionado == entidade.id)
                {
                    contato = entidade as Contato;
                }
            }
            return contato;
        }

        public bool Editar(Predicate<T> condicao, T novaEntidade)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    novaEntidade.id = entidade.id;

                    int posicaoParaEditar = registros.IndexOf(entidade);
                    registros[posicaoParaEditar] = novaEntidade;

                    return true;
                }
            }

            return false;
        }

        public bool Excluir(int idSelecionado)
        {
            foreach (T entidade in registros)
            {
                if (idSelecionado == entidade.id)
                {
                    registros.Remove(entidade);
                    return true;
                }
            }
            return false;
        }

        public bool Excluir(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    registros.Remove(entidade);
                    return true;
                }
            }
            return false;
        }

        public T SelecionarRegistro(int idSelecionado)
        {
            foreach (T entidade in registros)
            {
                if (idSelecionado == entidade.id)
                    return entidade;
            }

            return null;
        }

        public T SelecionarRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                    return entidade;
            }

            return null;
        }

        public List<T> SelecionarTodos()
        {
            return registros;
        }

        public List<T> Filtrar(Predicate<T> condicao)
        {
            List<T> registrosFiltrados = new List<T>();

            foreach (T registro in registros)
                if (condicao(registro))
                    registrosFiltrados.Add(registro);

            return registrosFiltrados;
        }

        public bool ExisteRegistro(int idSelecionado)
        {
            foreach (T entidade in registros)
                if (idSelecionado == entidade.id)
                    return true;

            return false;
        }

        public bool ExisteRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
                if (condicao(entidade))
                    return true;

            return false;
        }

    }
}
