using System;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using FileUpLoad.AutofacUtility;
using FileUpLoad.Filter;
using FileUpLoad.Utility;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FileUpLoad
{

    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.Init();
        }

        private readonly string AllowSpecificOrigin = "AllowSpecificOrigin";

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        { 
            //添加过滤器token验证
            services.AddScoped<TokenFilter>();

            #region Jwt配置 
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            //添加身份验证
            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //jwt token参数设置
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    //Token颁发机构
                    ValidIssuer = jwtSettings.Issuer,
                    //颁发给谁
                    ValidAudience = jwtSettings.Audience,
                    //这里的key要进行加密
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                };
            });
            #endregion

            #region SwaggerUi 
            services.AddSwaggerGen(options =>
            {
                #region 20210220在Configure启动中填写的地址中的v1是小写。所以swagger当然找不到/swagger.json这个文件了 文档格式化
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "鲁科物联网服务器接口说明文档",
                    Description = "Based on Asp.net Core WebApi，Powered By 鲁科物联 www.lk.com",
                    Contact = new OpenApiContact
                    {
                        Name = "komla",
                        Email = "944414275@qq.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "许可证",
                    }
                });

                //为 Swagger JSON and UI设置xml文档注释路径
                var xmlPath1 = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                options.IncludeXmlComments(xmlPath1, true);
                #endregion

                #region JWT
                //启用auth支持
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入带有Bearer的Token,例如: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //全局设置
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },new string[] { }
                    }
                });
                #endregion
            });

            #endregion

            //基于Autofac注入文件上传配置信息
            services.Configure<UpFileOptions>(Configuration.GetSection("UpFileOptions"));

            #region 设置文件上传大小 最大值不受限制
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });
            #endregion 

            #region JSON 全局配置
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //数据格式按原样输出  --此选项开启默认属性输出 
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                //修改属性名称的序列化方式，首字母小写(属性输出为 小驼峰)
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                //修改时间的序列化方式
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" });

                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                //忽略空值
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            #endregion
             
            //关闭参数自动校验,我们需要返回自定义的格式
            services.Configure<ApiBehaviorOptions>((o) =>
            {
                o.SuppressModelStateInvalidFilter = true;
            });

        }
         
        /// <summary>
        /// 1、UseServiceProviderFactory(new AutofacServiceProviderFactory());
        /// 2、public void ConfigureContainer(ContainerBuilder containerBuilder)
        /// 3、指定允许允许在控制器中获取服务实例 
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            #region Module  可以把服务注册部分隔离开来

            // 1、 添加一个 CustomAutofacModule 集成Autofac.Module
            // 2、 覆写Load 方法
            // 3、 把ConfigureContainer里的代码搬到 CustomAutofacModule 的Load方法里
            // 4、 在ConfigureContainer   containerBuilder.RegisterModule<CustomAutofacModule>();
            containerBuilder.RegisterModule<CustomAutofacModule>();

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region komla20210127 试图添加一个默认页面
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("swagger/index.html");

            ////访问HTML 静态页面
            app.UseStaticFiles();
            #endregion
             
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
             
            #region komla 20210127 试图设置默认路由
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        "default",
            //        "{controller=Home}/{action=Index}/{id?}");
            //});
            #endregion
             
            
             
            app.UseSwagger();
            // 指定站点
            app.UseSwaggerUI(options =>
            {
                //做出一个限制信息 描述
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //如果不想带/swagger路径访问的话，就放开下面的注释
                //options.RoutePrefix = string.Empty;
                //使用自定义的页面(主要是增加友好的身份认证体验)
                string path = Path.Combine(env.WebRootPath, "swagger/ui/index.html");
                if (File.Exists(path)) options.IndexStream = () => new MemoryStream(File.ReadAllBytes(path));
                //做出一个限制信息 描述
                //options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            //身份授权认证
            app.UseAuthentication();
            app.UseRouting();

            //CORS 中间件必须配置为在对 UseRouting 和 UseEndpoints的调用之间执行。 配置不正确将导致中间件停止正常运行。
            app.UseCors(AllowSpecificOrigin);

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //20210225 
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //    //endpoints.MapControllers();
            //});
        }
    }
}
