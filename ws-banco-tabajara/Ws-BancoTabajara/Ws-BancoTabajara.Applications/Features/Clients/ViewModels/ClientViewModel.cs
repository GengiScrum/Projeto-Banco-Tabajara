using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.Clients.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
    }
}
