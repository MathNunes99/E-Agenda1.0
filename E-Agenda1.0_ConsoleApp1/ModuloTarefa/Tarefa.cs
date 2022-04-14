using E_Agenda1._0_ConsoleApp1.Compartilhado;
using E_Agenda1._0_ConsoleApp1.ModuloItens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        private readonly int _prioridade;
        List<Itens> listaItens;
        public string _titulo;
        public DateTime _dataCriacao;
        DateTime _dataConclusao;
        public int _percentualConclusao;

        public Tarefa(int prioridade, string titulo, List<Itens> itensCadastrados)
        {
            this._titulo = titulo;
            this._prioridade = prioridade;            
            listaItens = itensCadastrados;
            this._dataCriacao = PassarHora();
        }
        public DateTime PassarHora()
        {
            if (_dataCriacao == DateTime.MinValue)
            {
                return DateTime.Now;
            }
            return _dataCriacao;
        }
        public int Percentual()
        {
            _percentualConclusao = 0;
            foreach (Itens itens in listaItens)
            {               
                int percentual = 100 / listaItens.Count; 
                if (itens.pendencia == false)
                {
                    _percentualConclusao += percentual;
                }
                
            }
            if (_percentualConclusao >= 100)
            {
                if (_dataConclusao == DateTime.MinValue)
                {
                    _dataConclusao = DateTime.Now;
                }
            }
            return _percentualConclusao;
        }
        public override string ToString()
        {
            //concluido
            if (_dataConclusao != DateTime.MinValue)
            {
                return "Id: " + id + Environment.NewLine +
                "Data da Conclusão: " + _dataConclusao + Environment.NewLine +
                "Porcentagem de Conclusão: " + Percentual() + "%" + Environment.NewLine +
                "Tarefa: " + _titulo + Environment.NewLine +
                ToStringDeItens() + Environment.NewLine;
            }
            //pendente
            return "Id: " + id + Environment.NewLine +
                "Data da Criaçao: " + _dataCriacao + Environment.NewLine +
                "Porcentagem de Conclusão: " + Percentual() + "%" + Environment.NewLine +
                "Tarefa: " + _titulo + Environment.NewLine +
                ToStringDeItens() + Environment.NewLine;
        }
        private string ToStringDeItens()
        {
            StringBuilder si = new StringBuilder();

            foreach (Itens itens in listaItens)
            {
                si.Append(itens.ToStringTarefa());
            }
            return si.ToString();
        }        
    }
}
