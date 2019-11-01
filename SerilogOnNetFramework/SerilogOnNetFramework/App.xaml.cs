using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using SimpleInjector;
using ILogger = Serilog.ILogger;

namespace SerilogOnNetFramework
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private static Container Container { get; } = new Container();

        public static MainWindowViewModel MainWindowViewModel => Container.GetInstance<MainWindowViewModel>();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var connectionString = @"server=localhost;uid=DFS;password=DFS;";
            var tableName = "Logs";

            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Add(StandardColumn.LogEvent);
            columnOptions.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn("Caller", SqlDbType.NVarChar)
            };

            // ロガーを構築する
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithCaller()
                .WriteTo.Async(x => x.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message} (at {Caller}){NewLine}{Exception}"))
                .WriteTo.Async(x => x.RollingFile(
                    new CompactJsonFormatter(),
                    "logs/log-{Date}.log"))
                .WriteTo.Async(x => x.MSSqlServer(
                    connectionString, tableName,
                    columnOptions: columnOptions,
                    autoCreateSqlTable:true
                ))
                .CreateLogger();

            // Setup Microsoft.Extensions.Logging
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog();
            var logger = loggerFactory.CreateLogger<App>();
            //logger.LogInformation("Application#Startup completed. {@param}", e);
            Log.Logger.Information("Application#Startup completed. {@param}", e);

            //Log.Information("Application#Startup completed. {@param}", e);
            Container.Register(() => loggerFactory.CreateLogger("MyApp"));
            Container.Register<MainWindowViewModel>();
            Container.Verify();
        }



        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Log.CloseAndFlush();
        }
    }

    class CallerEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var skip = 3;
            while (true)
            {
                var stack = new StackFrame(skip);
                if (!stack.HasMethod())
                {
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue("<unknown method>")));
                    return;
                }

                var method = stack.GetMethod();
                if (method.DeclaringType.Assembly != typeof(Log).Assembly
                    && method.DeclaringType.Assembly != typeof(Serilog.Extensions.Logging.SerilogLoggerProvider).Assembly
                    && method.DeclaringType.Assembly != typeof(LoggerFactory).Assembly
                    && method.DeclaringType.Assembly != typeof(Microsoft.Extensions.Logging.ILogger).Assembly)
                {
                    var caller = $"{method.DeclaringType.FullName}.{method.Name}({string.Join(", ", method.GetParameters().Select(pi => pi.ParameterType.FullName))})";
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue(caller)));
                }

                skip++;
            }
        }
    }

    static class LoggerCallerEnrichmentConfiguration
    {
        public static LoggerConfiguration WithCaller(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            return enrichmentConfiguration.With<CallerEnricher>();
        }

        public static bool HasMethod(this StackFrame stackFrame)
        {
            return stackFrame.GetMethod() != (MethodBase)null;
        }
    }

}
