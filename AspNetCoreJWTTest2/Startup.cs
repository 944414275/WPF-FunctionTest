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

            //��ȡ������Ϣ
            services.AddSingleton<ITokenHelper, TokenHelper>();
            services.Configure<JWTConfig>(Configuration.GetSection("JWT"));
            //����JWT
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//��֤����
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

            /********ע���м����ע��˳��********/
            app.UseAuthentication();//jwt��֤ 

            app.UseAuthorization();//��Ȩ
            /********ע���м����ע��˳��********/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
/***** Scheme: ��Ϊ�� ASP.NET Core �п���֧�ָ��ָ�������֤��ʽ���磬cookie, bearer, oauth, openid �ȵȣ���
 �� Scheme ������ʶʹ�õ���������֤��ʽ����ͬ����֤��ʽ�䴦��ʽ����ȫ��һ���ģ�����Scheme�Ƿǳ���Ҫ�ġ�
Challenge:��Ҫ��֤�ı�ʶ,��ʾ�û���¼,ͨ���᷵��һ�� 401 ״̬�롣*****/
