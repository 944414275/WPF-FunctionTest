using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreJWT
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
            //注入jwt
            services.AddScoped<GenerateJwt>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            #region jwt验证
            var jwtConfig = new JwtConfig();
            Configuration.Bind("JwtConfig", jwtConfig);
            services.AddAuthentication(option =>
            {
                //认证middleware配置
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Token颁发机构
                    ValidIssuer = jwtConfig.Issuer,
                    //颁发给谁
                    ValidAudience = jwtConfig.Audience,
                    //这里的key要进行加密
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                    //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true,
                };
            });
            #endregion
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
            app.UseAuthentication();//jwt 

            app.UseAuthorization();
            /********注册中间件，注意顺序********/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
