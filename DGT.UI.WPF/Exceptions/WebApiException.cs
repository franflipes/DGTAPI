using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGT.UI.WPF.Exceptions
{
    public class WebApiException:Exception
    {
        public WebApiException(String message) : base(message) { }
    }
}
