using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda1._0_ConsoleApp1.Compartilhado
{
    public interface ITelaCadastravel
    {        
        void Inserir();
        void Editar();
        void Excluir();
        bool VisualizarRegistros(string tipoVisualizacao);
    }
}
