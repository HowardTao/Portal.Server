using System;
using System.Collections.Generic;
using System.Text;

namespace PortalAdmin.Common.Configs
{
   public class StartupConfig
    {
        /// <summary>
        /// 缓存配置
        /// </summary>
        public CacheConfig Cache { get; set; }

        /// <summary>
        /// 操作日志配置参数
        /// </summary>
        public LogConfig Log { get; set; }

        /// <summary>
        /// 租户路由策略 0:Route 1:Host
        /// </summary>
        public TenantRouteStrategy TenantRouteStrategy { get; set; }
    }

    public class CacheConfig
    {

    }

    public class RedisConfig
    {

    }

    public class LogConfig
    {

    }

    public enum TenantRouteStrategy
    {
        Route,
        Host,
    }
}
