using System;
using System.IO;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace Data
{
    public class DesignTimeAppDataContextFactory : IDesignTimeDbContextFactory<AppDataContext>
    {
        private const string DatabaseConfigurationFileRelativePath = @"..\db.env"; // This env file is provided to database service.

        public AppDataContext CreateDbContext(string[] args) // From docs => Prior to EFCore 5.0 the args parameter is unused.
        {
            string EnsureStringIsNotNullOrWhiteSpace(string value) => string.IsNullOrWhiteSpace(value) ? throw new ArgumentOutOfRangeException(nameof(value), "Value can't be null or whiteSpace.") : value;

            string connectionString;
            try
            {
                Env.Load(DatabaseConfigurationFileRelativePath);

                connectionString = new NpgsqlConnectionStringBuilder
                {
                    Host = "localhost",
                    Username = EnsureStringIsNotNullOrWhiteSpace(Env.GetString("POSTGRES_USER")),
                    Password = EnsureStringIsNotNullOrWhiteSpace(Env.GetString("POSTGRES_PASSWORD")),
                    Database = EnsureStringIsNotNullOrWhiteSpace(Env.GetString("POSTGRES_DB"))
                }.ConnectionString;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Database configuration file is corrupted. Check it at '{Path.GetFullPath(DatabaseConfigurationFileRelativePath)}'.", e);
            }

            Console.WriteLine($"Connection string is {connectionString}");

            DbContextOptions<AppDataContext> contextOptions = new DbContextOptionsBuilder<AppDataContext>().UseNpgsql(connectionString).Options;
            var appDataContext = new AppDataContext(contextOptions);

            return appDataContext;
        }
    }
}