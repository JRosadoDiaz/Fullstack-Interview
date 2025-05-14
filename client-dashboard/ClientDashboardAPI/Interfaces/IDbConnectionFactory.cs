using System.Data;

namespace ClientDashboardAPI.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
