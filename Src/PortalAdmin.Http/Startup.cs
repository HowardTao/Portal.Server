using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortalAdmin.Common.Configs;
using PortalAdmin.HttpApi.Extensions;

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

            // 

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}