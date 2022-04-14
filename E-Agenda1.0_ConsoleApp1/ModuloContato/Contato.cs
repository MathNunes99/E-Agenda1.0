using E_Agenda1._0_ConsoleApp1.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.ModuloContato
{
    public class Contato : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _email;
        private readonly string _telefone;
        private readonly string _empresa;
        private readonly string _cargo;

        public Contato(string nome, string email, string telefone,string empresa, string cargo)
        {
            _nome = nome;
            _email = email;
            _telefone = telefone;
            _empresa = empresa;
            _cargo = cargo;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + _nome + Environment.NewLine +
                "E-mail: " + _email + Environment.NewLine +
                "Telefone: " + _telefone + Environment.NewLine +
                "Empresa: " + _empresa + Environment.NewLine+
                "Cargo: " + _cargo + Environment.NewLine;
        }
        public string ToStringBasic()
        {
            return "Nome: " + _nome + Environment.NewLine +
                "Empresa: " + _empresa + Environment.NewLine +
                "Cargo: " + _cargo + Environment.NewLine;
        }
    }
}
