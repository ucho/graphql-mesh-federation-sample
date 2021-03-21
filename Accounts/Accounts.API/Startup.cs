using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Accounts.Infrastructure;

namespace Accounts.API
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
            services
                .AddDbContext<AccountsDbContext>(options => options.UseInMemoryDatabase("accounts"))
                .AddScoped<AccountsSchema>()
                .AddGraphQL(options => options.EnableMetrics = true)
                .AddErrorInfoProvider(options => options.ExposeExceptionStackTrace = true)
                .AddSystemTextJson()
                .AddGraphTypes(typeof(AccountsSchema));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<AccountsSchema>();
            app.UseGraphQLPlayground();
        }
    }
}