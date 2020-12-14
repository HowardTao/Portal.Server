namespace PortalAdmin.MultiTenant
{
    public class MultiTenantContext
    {
        public TenantInfo TenantInfo { get; set; }
        public StrategyInfo StrategyInfo { get; set; }
        public StoreInfo StoreInfo { get; set; }
    }
}