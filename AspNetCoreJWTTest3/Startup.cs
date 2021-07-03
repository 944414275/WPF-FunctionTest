using AspNetCoreJWTTest3.Interface;
using AspNetCoreJWTTest3.Model;
using AspNetCoreJWTTest3.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using Microsoft.IdentityModel.Tokens;
using System;  
using System.Text; 

namespace AspNetCoreJWTTest3
{
    public class Startup
    {
        private TokenParameter _tokenParameter;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenParameter = configuration.GetSection("TokenParameter").Get<TokenParameter>() ?? throw new ArgumentNullException(nameof(_tokenParameter));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IJWTService, JWTService>();
            services.AddControllers();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//默认授权机制
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    { 
                        ValidateIssuer = true,
                        ValidIssuer = _tokenParameter.Issuer,

                        ValidateAudience = true,
                        ValidAudience = _tokenParameter.Audience,//为什么加上这个就可以？？？？？？？？？？？？？？

                        //ValidateLifetime = true,
                         
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.SecurityKey)),
                         
                        //注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟
                        //ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });
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

            app.UseAuthentication();//认证在app.UseRouting();之后，app.UseAuthorization();之前

            app.UseAuthorization();//授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
