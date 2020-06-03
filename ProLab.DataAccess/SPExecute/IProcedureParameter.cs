using Dapper;

namespace ProLab.DataAccess.SPExecute
{
    internal interface IProcedureParameter
    {
        public string connectionString { get; set; }
        public string SPName { get; set; }
        public DynamicParameters SPPatameters { get; set; }
    }
}
