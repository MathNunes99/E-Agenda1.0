using E_Agenda1._0_ConsoleApp1.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.ModuloItens
{
    public class Itens : EntidadeBase
    {
        public string titulo;
        public bool pendencia;
        private readonly string _descricao;        
        

        public Itens(string descricao)
        {
            this._descricao = descricao;
            this.pendencia = true;
            
        }
        
        public override string ToString()
        {
            return "Tarefa: " + titulo + Environment.NewLine +
                id + " - "  + _descricao + Environment.NewLine +
                "Pendencia: " + strPendencia() + Environment.NewLine;
        }
        public string ToStringTarefa()
        {
            return id + " - " + _descricao + Environment.NewLine +
                "Pendencia: " + strPendencia() + Environment.NewLine;
        }
        public string strPendencia()
        {
            string strPendencia = "";
            if (pendencia == true)
            {
                return strPendencia = "PENDENTE";
            }
            return strPendencia = "CONCLUIDO";
        }
    }
}
