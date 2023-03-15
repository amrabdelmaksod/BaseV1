using Hedaya.Application.MassCultures.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedaya.Application.MassCultures.Models
{
    public class MassCulturesResponse
    {
        public List<CategoryDto> Categories { get; set; }
        public List<MassCultureDto> AllCultures { get; set; }
    }
}
