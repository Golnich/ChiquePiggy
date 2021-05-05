using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChiquePiggy.MVC.AppService
{
    public interface IPonstosAppService
    {
        IReadOnlyList<PontosViewModel> ListarPontos();
        void AtualizaPontos(PontosViewModel parametros);

        void DeletarPontos(PontosViewModel parametros);
    }
}
