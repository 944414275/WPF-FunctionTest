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
            //��ӹ�����token��֤
            services.AddScoped<TokenFilter>();

            #region Jwt���� 
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            //��������֤
            services.AddAuthentication(options =>
            {
                //��֤middleware����
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //jwt token��������
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    //Token�䷢����
                    ValidIssuer = jwtSettings.Issuer,
                    //�䷢��˭
                    ValidAudience = jwtSettings.Audience,
                    //�����keyҪ���м���
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                };
            });
            #endregion

            #region SwaggerUi 
            services.AddSwaggerGen(options =>
            {
                #region 20210220��Configure��������д�ĵ�ַ�е�v1��Сд������swagger��Ȼ�Ҳ���/swagger.json����ļ��� �ĵ���ʽ��
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "³���������������ӿ�˵���ĵ�",
                    Description = "Based on Asp.net Core WebApi��Powered By ³������ www.lk.com",
                    Contact = new OpenApiContact
                    {
                        Name = "komla",
                        Email = "944414275@qq.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "���֤",
                    }
                });

                //Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var xmlPath1 = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                options.IncludeXmlComments(xmlPath1, true);
                #endregion

                #region JWT
                //����auth֧��
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "���������Bearer��Token,����: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //ȫ������
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

            //����Autofacע���ļ��ϴ�������Ϣ
            services.Configure<UpFileOptions>(Configuration.GetSection("UpFileOptions"));

            #region �����ļ��ϴ���С ���ֵ��������
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });
            #endregion 

            #region JSON ȫ������
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //���ݸ�ʽ��ԭ�����  --��ѡ���Ĭ��������� 
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                //�޸��������Ƶ����л���ʽ������ĸСд(�������Ϊ С�շ�)
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                //�޸�ʱ������л���ʽ
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" });

                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                //���Կ�ֵ
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            #endregion
             
            //�رղ����Զ�У��,������Ҫ�����Զ���ĸ�ʽ
            services.Configure<ApiBehaviorOptions>((o) =>
            {
                o.SuppressModelStateInvalidFilter = true;
            });

        }
         
        /// <summary>
        /// 1��UseServiceProviderFactory(new AutofacServiceProviderFactory());
        /// 2��public void ConfigureContainer(ContainerBuilder containerBuilder)
        /// 3��ָ�����������ڿ������л�ȡ����ʵ�� 
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            #region Module  ���԰ѷ���ע�Ჿ�ָ��뿪��

            // 1�� ���һ�� CustomAutofacModule ����Autofac.Module
            // 2�� ��дLoad ����
            // 3�� ��ConfigureContainer��Ĵ���ᵽ CustomAutofacModule ��Load������
            // 4�� ��ConfigureContainer   containerBuilder.RegisterModule<CustomAutofacModule>();
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
            #region komla20210127 ��ͼ���һ��Ĭ��ҳ��
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("swagger/index.html");

            ////����HTML ��̬ҳ��
            app.UseStaticFiles();
            #endregion
             
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
             
            #region komla 20210127 ��ͼ����Ĭ��·��
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        "default",
            //        "{controller=Home}/{action=Index}/{id?}");
            //});
            #endregion
             
            
             
            app.UseSwagger();
            // ָ��վ��
            app.UseSwaggerUI(options =>
            {
                //����һ��������Ϣ ����
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //��������/swagger·�����ʵĻ����ͷſ������ע��
                //options.RoutePrefix = string.Empty;
                //ʹ���Զ����ҳ��(��Ҫ�������Ѻõ������֤����)
                string path = Path.Combine(env.WebRootPath, "swagger/ui/index.html");
                if (File.Exists(path)) options.IndexStream = () => new MemoryStream(File.ReadAllBytes(path));
                //����һ��������Ϣ ����
                //options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            //�����Ȩ��֤
            app.UseAuthentication();
            app.UseRouting();

            //CORS �м����������Ϊ�ڶ� UseRouting �� UseEndpoints�ĵ���֮��ִ�С� ���ò���ȷ�������м��ֹͣ�������С�
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
