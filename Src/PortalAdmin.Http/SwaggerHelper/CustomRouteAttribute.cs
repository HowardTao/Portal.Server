using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
        /// 
        /// </summary>
        public string GroupName { get; set; }

        public CustomRouteAttribute(ApiVersions version,string moduleName="",string actionName = "[action]")
        {

        }
        private static string GetRouteTemplate(ApiVersions version, string moduleName = "", string actionName = "[action]")
        {
            var path = moduleName == "" ? version.ToString() : version.ToString + "/" + moduleName;
            var 
        }
    }
}
