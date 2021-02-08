using AspNetCoreJWTTest2.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreJWTTest2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //读取配置信息
            services.AddSingleton<ITokenHelper, TokenHelper>();
            services.Configure<JWTConfig>(Configuration.GetSection("JWT"));
            //启用JWT
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//认证方案
            }).AddJwtBearer();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            /********注册中间件，注意顺序********/
            app.UseAuthentication();//jwt认证 

            app.UseAuthorization();//授权
            /********注册中间件，注意顺序********/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
/***** Scheme: 因为在 ASP.NET Core 中可以支持各种各样的认证方式（如，cookie, bearer, oauth, openid 等等），
 而 Scheme 用来标识使用的是哪种认证方式，不同的认证方式其处理方式是完全不一样的，所以Scheme是非常重要的。
Challenge:需要认证的标识,提示用户登录,通常会返回一个 401 状态码。*****/
