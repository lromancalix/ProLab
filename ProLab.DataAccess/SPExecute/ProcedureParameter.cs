using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProLab.DataAccess.SPExecute
{
    internal class ProcedureParameter : IProcedureParameter
    {
        public string connectionString { get; set; }
        public string SPName { get; set; }
        public DynamicParameters SPPatameters { get; set; }
       
    }
}
