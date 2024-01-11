using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Domain.Contracts
{
    public interface IEntity
    {
        void GenId();
        bool Compare(string value);
    }
}
