using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PortalAdmin.HttpApi.SwaggerHelper.CustomApiVersion;

namespace PortalAdmin.HttpApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            string apiName = "PortalAdmin";

            #region Swagger 接口文档定义
            //注册Swagger生成器，定义一个或多个Swagger文档
            services.AddSwaggerGen(s =>
            {
                //根据版本名称 遍历展示
                typeof(ApiVersions).GetEnumNames().OrderBy(e => e).ToList().ForEach(version =>
                {
                    s.SwaggerDoc(version,new open)
                });
            });
            #endregion
        }

        public static void UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            var apiName = "PortalAdmin";
            //启用swagger组件
            app.UseSwagger();

            //启用中间件服务swagger-ui (HTML, JS, CSS等)，
            //指定Swagger JSON端点。
            app.UseSwaggerUI(t =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //根据版本名称倒序 遍历展示
                typeof(ApiVersions).GetEnumNames().OrderBy(e => e).ToList().ForEach(version =>
                {
                    t.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{apiName}{version}");
                });
                t.RoutePrefix = "";//直接根目录访问
                t.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);//折叠api
            });
        }
    }

}
