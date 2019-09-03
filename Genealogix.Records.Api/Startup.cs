using Amazon.S3;
using Genealogix.Records.Api.Db;
using Genealogix.Records.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Genealogix.Records.Api
{
    public class Startup
    {
        private const string CorsPolicyName = "_GenealogyCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup CORS policies.
            services.AddCors();

            // MongoDB setup.
            services.Configure<RecordsDatabaseSettings>(
                Configuration.GetSection(nameof(RecordsDatabaseSettings)));            
            services.AddSingleton<IRecordsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<RecordsDatabaseSettings>>().Value);
            services.AddSingleton<IRecordsDatabaseClientFactory, MongoDbClientRecordsDatabaseFactory>();
            services.AddSingleton<IRecordService, RecordService>();

            // Amazon S3 setup.
            services.AddTransient<ImageResizer>();
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddSingleton<IImageService, S3ImageService>();

            // MVC setup.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
