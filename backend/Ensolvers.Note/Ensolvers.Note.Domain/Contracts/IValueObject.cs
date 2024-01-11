using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Domain.Contracts
{
    public interface IValueObject
    {
        bool Compare(string value);
    }
}
