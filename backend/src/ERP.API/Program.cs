using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

// Bootstrap logger captures startup failures before full Serilog configuration loads.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // ── Logging ──────────────────────────────────────────────────────────────────
    builder.Host.UseSerilog((context, services, configuration) =>
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

    // ── Controllers ───────────────────────────────────────────────────────────────
    // ASP.NET Core discovers controllers from all referenced module assemblies.
    // Route convention: /api/v1/{resource}  (applied per-controller via [Route])
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();

    // ── Problem Details (RFC 7807) ────────────────────────────────────────────────
    builder.Services.AddProblemDetails();

    // ── Authentication — JWT Bearer ───────────────────────────────────────────────
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer           = true,
                ValidateAudience         = true,
                ValidateLifetime         = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer              = builder.Configuration["Jwt:Issuer"],
                ValidAudience            = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey         = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });

    builder.Services.AddAuthorization();

    // ── Swagger / OpenAPI ──────────────────────────────────────────────────────────
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title       = "RentalERP API",
            Version     = "v1",
            Description = "Enterprise Rental ERP — REST API"
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name         = "Authorization",
            Type         = SecuritySchemeType.Http,
            Scheme       = "Bearer",
            BearerFormat = "JWT",
            In           = ParameterLocation.Header,
            Description  = "Enter: Bearer {token}"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id   = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    // ── Hangfire ───────────────────────────────────────────────────────────────────
    // Storage (PostgreSQL) is wired here once ERP.Infrastructure exposes AddInfrastructure():
    //   builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddHangfire(config =>
        config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings());

    builder.Services.AddHangfireServer();

    // ── Persistence ────────────────────────────────────────────────────────────────
    // TODO: uncomment once ERP.Persistence defines AppDbContext
    // builder.Services.AddDbContext<AppDbContext>(opt =>
    //     opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

    // ── MediatR ────────────────────────────────────────────────────────────────────
    // TODO: register all module assemblies once module code exists
    // builder.Services.AddMediatR(cfg =>
    //     cfg.RegisterServicesFromAssemblies(
    //         typeof(ERP.Foundation.Anchor).Assembly,
    //         typeof(ERP.Administration.Anchor).Assembly,
    //         typeof(ERP.Security.Anchor).Assembly,
    //         typeof(ERP.Customer.Anchor).Assembly,
    //         typeof(ERP.Vendor.Anchor).Assembly,
    //         typeof(ERP.Product.Anchor).Assembly,
    //         typeof(ERP.Warehouse.Anchor).Assembly,
    //         typeof(ERP.Inventory.Anchor).Assembly,
    //         typeof(ERP.Asset.Anchor).Assembly,
    //         typeof(ERP.Rental.Anchor).Assembly,
    //         typeof(ERP.Service.Anchor).Assembly,
    //         typeof(ERP.Purchase.Anchor).Assembly,
    //         typeof(ERP.Sales.Anchor).Assembly,
    //         typeof(ERP.Accounting.Anchor).Assembly,
    //         typeof(ERP.Reporting.Anchor).Assembly,
    //         typeof(ERP.Workflow.Anchor).Assembly,
    //         typeof(ERP.Notification.Anchor).Assembly,
    //         typeof(ERP.Dashboard.Anchor).Assembly,
    //         typeof(ERP.Audit.Anchor).Assembly,
    //         typeof(ERP.Integration.Anchor).Assembly,
    //         typeof(ERP.Scheduler.Anchor).Assembly,
    //         typeof(ERP.SystemConfiguration.Anchor).Assembly,
    //         typeof(ERP.Platform.Anchor).Assembly));

    // ── AutoMapper ─────────────────────────────────────────────────────────────────
    // TODO: builder.Services.AddAutoMapper(moduleAssemblies);

    // ── FluentValidation ───────────────────────────────────────────────────────────
    // TODO: builder.Services.AddValidatorsFromAssemblies(moduleAssemblies);

    // ── Module service registrations ───────────────────────────────────────────────
    // TODO: builder.Services.AddFoundationModule();
    // TODO: builder.Services.AddAdministrationModule();
    // TODO: builder.Services.AddSecurityModule();
    // TODO: builder.Services.AddCustomerModule();
    // TODO: builder.Services.AddVendorModule();
    // TODO: builder.Services.AddProductModule();
    // TODO: builder.Services.AddWarehouseModule();
    // TODO: builder.Services.AddInventoryModule();
    // TODO: builder.Services.AddAssetModule();
    // TODO: builder.Services.AddRentalModule();
    // TODO: builder.Services.AddServiceModule();
    // TODO: builder.Services.AddPurchaseModule();
    // TODO: builder.Services.AddSalesModule();
    // TODO: builder.Services.AddAccountingModule();
    // TODO: builder.Services.AddReportingModule();
    // TODO: builder.Services.AddWorkflowModule();
    // TODO: builder.Services.AddNotificationModule();
    // TODO: builder.Services.AddDashboardModule();
    // TODO: builder.Services.AddAuditModule();
    // TODO: builder.Services.AddIntegrationModule();
    // TODO: builder.Services.AddSchedulerModule();
    // TODO: builder.Services.AddSystemConfigurationModule();
    // TODO: builder.Services.AddPlatformModule();

    // ─────────────────────────────────────────────────────────────────────────────

    var app = builder.Build();

    // ── Exception handling ─────────────────────────────────────────────────────────
    // UseExceptionHandler() with no arguments produces RFC 7807 ProblemDetails
    // responses automatically when AddProblemDetails() is registered above.
    app.UseExceptionHandler();
    app.UseStatusCodePages();

    // ── Swagger (development only) ─────────────────────────────────────────────────
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "RentalERP API v1");
            options.RoutePrefix = string.Empty;
        });
    }

    // ── Request pipeline ───────────────────────────────────────────────────────────
    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.UseRouting();

    // TODO: app.UseCors(corsPolicy); — add CORS policy before going to production

    app.UseAuthentication();
    app.UseAuthorization();

    // ── Hangfire Dashboard ─────────────────────────────────────────────────────────
    // TODO: restrict dashboard access in production via DashboardOptions.Authorization
    app.UseHangfireDashboard("/hangfire");

    // ── Controller mapping ─────────────────────────────────────────────────────────
    app.MapControllers();

    // ── Module endpoint mapping ────────────────────────────────────────────────────
    // TODO: app.MapFoundationEndpoints();
    // TODO: app.MapAdministrationEndpoints();
    // TODO: app.MapSecurityEndpoints();
    // TODO: app.MapCustomerEndpoints();
    // TODO: app.MapVendorEndpoints();
    // TODO: app.MapProductEndpoints();
    // TODO: app.MapWarehouseEndpoints();
    // TODO: app.MapInventoryEndpoints();
    // TODO: app.MapAssetEndpoints();
    // TODO: app.MapRentalEndpoints();
    // TODO: app.MapServiceEndpoints();
    // TODO: app.MapPurchaseEndpoints();
    // TODO: app.MapSalesEndpoints();
    // TODO: app.MapAccountingEndpoints();
    // TODO: app.MapReportingEndpoints();
    // TODO: app.MapWorkflowEndpoints();
    // TODO: app.MapNotificationEndpoints();
    // TODO: app.MapDashboardEndpoints();
    // TODO: app.MapAuditEndpoints();
    // TODO: app.MapIntegrationEndpoints();
    // TODO: app.MapSchedulerEndpoints();
    // TODO: app.MapSystemConfigurationEndpoints();
    // TODO: app.MapPlatformEndpoints();

    await app.RunAsync();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
