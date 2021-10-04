using AutomatedFinances.Application.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace AutomatedFinances.Infrastructure.Data
{
    internal class AutomedFinancesDbContext : IAutomedFinancesDbContext, IDisposable
    {
        public IDbConnection GetWriterConnection() => _dbConnection ??= IntialiseWriterConnection();

        private readonly object _lockObject = new();
        private readonly DatabaseSettings _settings;

        private IDbConnection? _dbConnection;

        public AutomedFinancesDbContext(DatabaseSettings settings)
        {
            _settings = settings;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _dbConnection?.Close();
            _dbConnection?.Dispose();
            _dbConnection = null;
        }

        private IDbConnection IntialiseWriterConnection()
        {
            lock(_lockObject)
            {
                if (_dbConnection != null)
                {
                    return _dbConnection;
                }

                return new SqlConnection(_settings.ConnectionString);
            }
        }
    }
}
