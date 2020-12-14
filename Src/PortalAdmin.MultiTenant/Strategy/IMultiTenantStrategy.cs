using System.Threading.Tasks;

namespace PortalAdmin.MultiTenant.Strategy
{
    public interface IMultiTenantStrategy
    {
        Task<string> GetIdentifierAsync(object context);
    }
}