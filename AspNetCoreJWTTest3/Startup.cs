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
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Ĭ����Ȩ����
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    { 
                        ValidateIssuer = true,
                        ValidIssuer = _tokenParameter.Issuer,

                        ValidateAudience = true,
                        ValidAudience = _tokenParameter.Audience,//Ϊʲô��������Ϳ��ԣ���������������������������

                        //ValidateLifetime = true,
                         
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.SecurityKey)),
                         
                        //ע�����ǻ������ʱ�䣬�ܵ���Чʱ��������ʱ�����jwt�Ĺ���ʱ�䣬��������ã�Ĭ����5����
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

            app.UseAuthentication();//��֤��app.UseRouting();֮��app.UseAuthorization();֮ǰ

            app.UseAuthorization();//��Ȩ

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
