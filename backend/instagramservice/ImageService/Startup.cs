using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.picsfeed.ImageService.Models;
using com.picsfeed.ImageService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace com.picsfeed.ImageService
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOptions();
            var imageServiceConfigSection = Configuration.GetSection("ImageServiceConfig");
            services.Configure<ImageServiceConfig>(imageServiceConfigSection);
            var rabbitConfig = Configuration.GetSection("RabbitMqConfig");
            services.Configure<RabbitMqConfig>(rabbitConfig);

            services.AddScoped<IImageService, Services.ImageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".mpd"] = "application/dash+xml";
            provider.Mappings[".webm"] = "video/webm";
            provider.Mappings[".weba"] = "audio/webm";

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "rawcon")),
                RequestPath = "/StaticContentDir",
                ContentTypeProvider = provider
            });

            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .Build()
            //);

            //app.Use( async(context, next) => {
            //    var headersInfo = context.Request.Headers;
            //    if (headersInfo.ContainsKey("Authorization"))
            //    {
            //        var token = headersInfo["Authorization"];
            //        var secret = Configuration.GetSection("ImageServiceConfig")["Secret"];
            //        var tokenValidationParams = new TokenValidationParameters()
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //        var jwtHandler = new JwtSecurityTokenHandler();
            //        JwtSecurityToken jwtToken = null;
            //        try
            //        {
            //            var principal = jwtHandler.ValidateToken(token, tokenValidationParams, out var validatedToken);
            //            jwtToken = validatedToken as JwtSecurityToken;

            //            if (!jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.Ordinal))
            //            {
            //                throw new Exception("Token is not in Hmac256");
            //            }
            //            if (jwtToken == null)
            //            {
            //                throw new Exception("JWt is null");
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            context.Response.StatusCode = 401;
            //            return;
            //        }
            //        await next.Invoke();
            //    }
            //    else {
            //        context.Response.StatusCode = 401;
            //        return;
            //    }
            //} );

            app.UseMvc();
        }
    }
}
