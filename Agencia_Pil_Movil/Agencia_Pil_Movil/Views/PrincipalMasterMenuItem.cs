using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencia_Pil_Movil.Views
{
    public class PrincipalMasterMenuItem
    {
        public PrincipalMasterMenuItem()
        {
            TargetType = typeof(PrincipalMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}