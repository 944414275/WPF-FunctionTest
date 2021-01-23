using AspNetCoreStartup.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AspNetCoreStartup
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
            //����ӵĻ����Ӳ��ɹ�
            services.AddControllers().AddNewtonsoftJson();

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,//�Ƿ���ö�ǩ��securityToken��SecurityKey������֤
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenParameter.Secret)),//ǩ����Կ
            //        ValidateIssuer = true,//�Ƿ���֤�䷢��
            //        ValidIssuer = TokenParameter.Issuer, //�䷢��
            //        ValidateAudience = true, //�Ƿ���֤������
            //        ValidAudience = TokenParameter.Audience,//������
            //        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
            //    };
            //});

            //#region JWT��֤ 
            ////��appsettings.json�е�JwtSettings�����ļ���ȡ��JwtSettingsע������
            //services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            ////���ڳ�ʼ������Ҫ�ã�����ʹ��Bind�ķ�ʽ��ȡ����
            ////�����ð󶨵�JwtSettingsʵ����
            //var jwtSettings = new JwtSettings();
            //Configuration.Bind("JwtSettings", jwtSettings);

            //services.AddAuthentication(options =>
            //{
            //    //��֤middleware����
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(o =>
            //{
            //    //��Ҫ��jwt  token��������
            //    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        //Token�䷢����
            //        ValidIssuer = jwtSettings.Issuer,
            //        //�䷢��˭
            //        ValidAudience = jwtSettings.Audience,
            //        //�����keyҪ���м��ܣ���Ҫ����Microsoft.IdentityModel.Tokens
            //        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            //        //ValidateIssuerSigningKey=true,
            //        //�Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
            //        //ValidateLifetime=true,
            //        //����ķ�����ʱ��ƫ����
            //        //ClockSkew=TimeSpan.Zero
            //    };
            //});

            //#endregion

            //#region ����  

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //});

            //#endregion

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

            app.UseAuthentication();//������app.UseAuthorization();֮ǰ

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseMvc(route => { route.MapRoute("default", "api/{controller}/{action}"); });
        }
    }
}
