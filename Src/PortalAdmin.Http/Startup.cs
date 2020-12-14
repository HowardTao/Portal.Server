using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortalAdmin.Common.Configs;
using PortalAdmin.HttpApi.Extensions;
using PortalAdmin.MultiTenant;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PortalAdmin.HttpApi
{
    public class Startup
    {
        private IWebHostEnvironment Env { get; }

        public IConfiguration Configuration { get; }

        private readonly string _allowSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;

            //得到启动生效的配置项
            //_startupConfig = SettingHelper.Get<StartupConfig>("startupsettings", env.EnvironmentName) ?? new StartupConfig();
        }

        // 运行时调用此方法。使用此方法向容器添加服务。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_allowSpecificOrigins, builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    //.WithMethods("GET","POST","PUT","DELETE","OPTIONS")
                );
            });
            // 得到程序启动需要的参数配置
            var _startupConfig = Configuration.GetSection("Startup").Get<StartupConfig>();

            // 注册系统配置到容器
            services.Configure<SystemConfig>(Configuration.GetSection("System"));

            // 注册租户配置到容器
            services.Configure<List<TenantInfo>>(Configuration.GetSection("Tenants"));

            services.AddControllers(options =>
            {
                if (_startupConfig.Log.Operation) options.Filters.Add<LogActionFilter>();
                options.Filters.Add<ValidateModelFilter>(); //自定义模型验证
            }).ConfigureApiBehaviorOptions(options =>
            {
                //关闭默认模型验证,因为我们使用了自己的ValidateModelFilter
                options.SuppressModelStateInvalidFilter = true;
            }).AddNewtonsoftJson(options => //设定json序列化参数
            {
                // 忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // 使用小驼峰
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                // 设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // 每个租户可以有不同的配置参数，故在租户里面配置参数 (JwtBearerOptions:o)属性，
                // 见RegsiterMultiTenantServices WithPerTenantOptions<JwtBearerOptions>
            });
        }

        // 运行时调用此方法。使用此方法配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 启用swagger中间件
            app.UseSwaggerMiddleware();
            // 全局异常捕获

            //静态文件

            app.UseHttpsRedirection();
            // 路由中间件
            app.UseRouting();
// 对请求进行权限验证
            app.UseAuthorization();
        }
    }
}