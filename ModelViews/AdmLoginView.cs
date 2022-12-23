using System;
using System.Collections.Generic;

namespace webapi.ModelViews
{
    public record AdmLoginView
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
