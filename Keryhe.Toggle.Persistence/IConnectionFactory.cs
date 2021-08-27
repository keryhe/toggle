using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Keryhe.Toggle.Persistence
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
