using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedaya.Application.MassCultures.Models
{
    public class MassCultureResponse
    {
        public MassCultureDto MassCulture { get; set; }
        public List<MassCultureDto> RelatedMassCultures { get; set; }
    }

}
