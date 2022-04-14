using E_Agenda1._0_ConsoleApp1.Compartilhado;
using E_Agenda1._0_ConsoleApp1.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        private readonly string _assunto;
        private readonly string _data;
        private readonly string _local;
        private readonly string _inicio;
        private readonly string _termino;
        Contato _contato;

        public Compromisso(string assunto, string data, string local, string inicio, string termino, Contato contato)
        {
            _assunto = assunto;
            _data = data;
            _local = local;
            _inicio = inicio;
            _termino = termino;
            _contato = contato;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Assunto: " + _assunto + Environment.NewLine +
                "Data: " + _data + Environment.NewLine +
                "Local: " + _local + Environment.NewLine +
                "Inicio: " + _inicio + Environment.NewLine +
                "Termino: " + _termino + Environment.NewLine + 
                "Dados do Contato: " + ToStringBasicContato() + Environment.NewLine;
        }
        public string ToStringBasicContato()
        {
            return "Nome: " + _contato._nome + Environment.NewLine +
                "Empresa: " + _contato._empresa + Environment.NewLine;
        }
    }
}
