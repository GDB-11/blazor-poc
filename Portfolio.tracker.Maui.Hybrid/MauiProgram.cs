using Application.Core.Contract.PortfolioInterface;
using Application.Core.Contract.Shared;
using Application.Core.Contract.TransactionInterface;
using Application.Core.Contract.UserInterface;
using Application.Core.Models.Application.Config;
using Application.Core.Models.BlazorDTO.Shared;
using Application.Core.Models.Constants.PortfolioItem;
using Application.Core.Models.Database.Transactions;
using Application.Core.Service.PortfolioImplementation;
using Application.Core.Service.Shared;
using Application.Core.Service.TransactionImplementation;
using Application.Core.Service.UserImplementation;
using CurrieTechnologies.Razor.SweetAlert2;
using Gian.Basic.Helper;
using Infrastructure.Core.Database;
using Infrastructure.Core.IDatabase;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace Portfolio.tracker.Maui.Hybrid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Portfolio.tracker.Maui.Hybrid.appconfig.json");
            if (stream != null)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                builder.Configuration.AddConfiguration(config);
            }

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSweetAlert2();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            ConnectionStrings connectionStrings = new()
            {
                AppSqliteConnectionString = $"Data Source={Path.Combine(PathHelper.ExecutingLocation(), "appdb.db")};Version=3;",
                TxLiteDbConnectionString = $"Filename={Path.Combine(PathHelper.ExecutingLocation(), "tx.db")}"
            };

            using Stream? cultureStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Portfolio.tracker.Maui.Hybrid.culture.json");
            string cultureJson = string.Empty;
            CultureData? cultureData;
            string cultureName = "en";

            if (cultureStream != null)
            {
                using (StreamReader reader = new(cultureStream))
                {
                    cultureJson = reader.ReadToEnd();
                }

                cultureData = System.Text.Json.JsonSerializer.Deserialize<CultureData>(cultureJson);
                cultureName = cultureData?.Culture ?? "en";
            }

            builder.Services.AddSingleton(connectionStrings);

            builder.Services.AddSingleton<IDbConnection>(sp => new SQLiteConnection(sp.GetRequiredService<ConnectionStrings>().AppSqliteConnectionString));

            builder.Services.AddSingleton<IApplicationCulture>(sp => new ApplicationCultureService(cultureName));
            builder.Services.AddSingleton<IMessageHandling>(sp => new MessageHandlingService("Messages", new ApplicationCultureService(cultureName)));

            builder.Services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddSingleton<ILiteDbGenericRepository<Transaction>>(sp =>
                    new LiteDbGenericRepository<Transaction>(sp.GetRequiredService<ConnectionStrings>().TxLiteDbConnectionString, "transaction"));

            builder.Services.AddSingleton(typeof(IUser), typeof(UserService));
            builder.Services.AddSingleton(typeof(IClock), typeof(SystemClock));
            builder.Services.AddSingleton(typeof(IApplicationSession), typeof(ApplicationSessionService));
            builder.Services.AddSingleton(typeof(IPortfolio), typeof(PortfolioService));
            builder.Services.AddSingleton(typeof(IPortfolioItem), typeof(PortfolioItemService));
            builder.Services.AddSingleton(typeof(IAssetTransaction), typeof(AssetTransactionService));

            BsonMapper.Global.RegisterType(
                serialize: (enumValue) => new BsonValue((int)enumValue),
                deserialize: (bsonValue) => (Constants.OperationType)Enum.Parse(typeof(Constants.OperationType), bsonValue.AsString)
            );

            return builder.Build();
        }
    }
}