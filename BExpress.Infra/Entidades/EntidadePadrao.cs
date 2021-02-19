using System;
using System.Collections.Generic;
using System.Text;

namespace BExpress.Infra.Entidades
{
    public abstract class EntidadePadrao
    {
        public EntidadePadrao()
        {
            Id = new int();
        }

        public int Id { get; private set; }
    }
}
