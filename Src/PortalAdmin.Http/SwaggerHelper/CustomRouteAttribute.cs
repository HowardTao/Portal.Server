using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PortalAdmin.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PortalAdmin.HttpApi.SwaggerHelper.CustomApiVersion;

namespace PortalAdmin.HttpApi.SwaggerHelper
{
    /// <summary>
    /// 自定义路由 /api/{version}/{controller}/[action]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 分组名称,是来实现接口 IApiDescriptionGroupNameProvider
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 自定义版本+路由构造函数，继承基类路由
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="actionName"></param>
        public CustomRouteAttribute(ApiVersions version,string moduleName="",string actionName = "[action]"):base(GetRouteTemplate(version,moduleName,actionName))
        {
            GroupName = version.ToString();
        }
        private static string GetRouteTemplate(ApiVersions version, string moduleName = "", string actionName = "[action]")
        {
            var path = moduleName == "" ? version.ToString() : version.ToString() + "/" + moduleName;
            var startupConfig = StartupConfigHelper.Get();
            string temp;
            switch (startupConfig.TenantRouteStrategy)
            {
                case Common.Configs.TenantRouteStrategy.Route:
                    temp = "/{__tenant__}/api/" + path + "/[controller]/" + actionName;
                    break;
                case Common.Configs.TenantRouteStrategy.Host:
                    temp = "/api/" + path + "/[controller]/" + actionName;
                    break;
                default:
                    temp = "/{__tenant__}/api/" + path + "/[controller]/" + actionName;
                    break;
            }
            return temp;
        }
    }
}
