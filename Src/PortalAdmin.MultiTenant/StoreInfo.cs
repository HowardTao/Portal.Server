using System;
using PortalAdmin.MultiTenant.Store;

namespace PortalAdmin.MultiTenant
{
    public class StoreInfo
    {
        public Type  StoreType { get; set; }
        public IMultiTenantStore  Store { get; set; }
        public MultiTenantContext MultiTenantContext { get; set; }
    }
}