using System;
using PortalAdmin.MultiTenant.Strategy;

namespace PortalAdmin.MultiTenant
{
    public class StrategyInfo
    {
        public Type  StrategyType { get; set; }
        public IMultiTenantStrategy Strategy { get; set; }
        public MultiTenantContext MultiTenantContext { get; set; }
    }
}