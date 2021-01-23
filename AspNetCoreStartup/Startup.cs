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
            //不添加的话连接不成功
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
            //        ValidateIssuerSigningKey = true,//是否调用对签名securityToken的SecurityKey进行验证
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenParameter.Secret)),//签名秘钥
            //        ValidateIssuer = true,//是否验证颁发者
            //        ValidIssuer = TokenParameter.Issuer, //颁发者
            //        ValidateAudience = true, //是否验证接收者
            //        ValidAudience = TokenParameter.Audience,//接收者
            //        ValidateLifetime = true,//是否验证失效时间
            //    };
            //});

            //#region JWT认证 
            ////将appsettings.json中的JwtSettings部分文件读取到JwtSettings注入依赖
            //services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            ////由于初始化就需要用，所以使用Bind的方式读取配置
            ////将配置绑定到JwtSettings实例中
            //var jwtSettings = new JwtSettings();
            //Configuration.Bind("JwtSettings", jwtSettings);

            //services.AddAuthentication(options =>
            //{
            //    //认证middleware配置
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(o =>
            //{
            //    //主要是jwt  token参数设置
            //    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        //Token颁发机构
            //        ValidIssuer = jwtSettings.Issuer,
            //        //颁发给谁
            //        ValidAudience = jwtSettings.Audience,
            //        //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
            //        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            //        //ValidateIssuerSigningKey=true,
            //        //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
            //        //ValidateLifetime=true,
            //        //允许的服务器时间偏移量
            //        //ClockSkew=TimeSpan.Zero
            //    };
            //});

            //#endregion

            //#region 跨域  

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

            app.UseAuthentication();//必须在app.UseAuthorization();之前

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseMvc(route => { route.MapRoute("default", "api/{controller}/{action}"); });
        }
    }
}
