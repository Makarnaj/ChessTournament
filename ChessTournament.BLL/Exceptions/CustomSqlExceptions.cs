using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.BLL.Exceptions
{
    public class CustomSqlException : Exception
    {
        public CustomSqlException(string? message) : base(message)
        {

        }
    }
}
