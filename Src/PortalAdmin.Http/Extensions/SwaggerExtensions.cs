﻿using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.OpenApi.Models;
using static PortalAdmin.HttpApi.SwaggerHelper.CustomApiVersion;

namespace PortalAdmin.HttpApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            var apiName = "PortalAdmin";

            #region Swagger 接口文档定义

            //注册Swagger生成器，定义一个或多个Swagger文档
            services.AddSwaggerGen(s =>
            {
                //根据版本名称 遍历展示
                typeof(ApiVersions).GetEnumNames().OrderBy(e => e).ToList().ForEach(version =>
                {
                    s.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{apiName} API",
                        TermsOfService = new Uri("https://www.xxx.com"), //服务条款
                        Contact = new OpenApiContact()
                        {
                            Name = "Portal",
                            Email = String.Empty
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "License",
                            Url = new Uri("https://www.xxx.com/license")
                        }
                    });
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    //添加注释到 SwaggerUI
                    s.IncludeXmlComments(xmlPath);
                }

                #region 为 SwaggerUI 添加全局 token 验证
                s.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}},
                        new string[] { }
                    }
                });

                #endregion
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
                t.RoutePrefix = ""; //直接根目录访问
                t.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); //折叠api
            });
        }
    }
}