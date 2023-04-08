using System.Collections.Generic;
using SirvaMe.Views;

namespace SirvaMe.Menu
{
    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            Add(new MenuItem
            {
                Title = "Novo Agendamento",
                TargetType = typeof(ServicosListaPage)
            });
            Add(new MenuItem
            {
                Title = "Pedidos de Agendamento",
                TargetType = typeof(AgendamentosListaPage)
            });
            Add(new MenuItem
            {
                Title = "Histórico",
                TargetType = typeof(AgendamentosHistoricoPage)
            });
            Add(new MenuItem
            {
                Title = "Sair",
                TargetType = typeof(LogoffPage)
            });
        }
    }
}
