using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RealmDigital.Services
{
    /// <summary>
    /// Service Context to perform basic CRUD operations
    /// </summary>
    internal class ServiceContext : IDisposable
    {
        /// <summary>
        /// dispose status
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Get Open Connection from available server information
        /// </summary>
        /// <returns>Connection object</returns>
        private IDbConnection GetOpenConnection()
        {

            IDbConnection connection;
            connection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["addressContext"].ToString());
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);

            connection.Open();
            return connection;
        }

        /// <summary>
        /// Save the supplied object and return newly created Id
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">supplied model</param>
        /// <param name="checkForDuplicate">Need to check for duplicate</param>
        /// <param name="columns">parameter to check for duplicate</param>
        /// <returns></returns>
        public int Save<TEntity>(TEntity entity, bool checkForDuplicate = false, bool checkCombination = false, params string[] columns)
        {
            using (var connection = GetOpenConnection())
            {
                var id = (int)entity.GetType().GetProperty("ID").GetValue(entity);
                if (checkForDuplicate)
                {
                    string searchCriteria = $"ID <> {id} AND (";
                    int index = 0;
                    foreach (var column in columns)
                    {
                        if (checkCombination)
                        {
                            searchCriteria += index > 0 ? $" AND {column} = '{entity.GetType().GetProperty(column).GetValue(entity)}'" : $" {column} = '{entity.GetType().GetProperty(column).GetValue(entity)}'";
                        }
                        else
                        {
                            searchCriteria += index > 0 ?  $" OR {column} = '{entity.GetType().GetProperty(column).GetValue(entity)}'" : $" {column} = '{entity.GetType().GetProperty(column).GetValue(entity)}'";
                        }
                        index++;
                    }
                    searchCriteria += ")";

                    var list = Search<TEntity>(searchCriteria);
                    if (list.Count > 0)
                    {
                        return -1;
                    }
                }
                if (id > 0)
                {
                    return connection.Update(entity);
                }
                else
                {
                    return connection.Insert(entity) ?? 0;
                }
                
            }
        }

        /// <summary>
        /// Return the list of model for given search criteria
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="model">model</param>
        /// <returns> return List Of entity </returns>
        public List<TEntity> Search<TEntity>(string conditions)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.GetList<TEntity>($"where {conditions}").ToList();
            }
        }

        /// <summary>
        /// Get only one object from id
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="id">id</param>
        /// <returns>return the object of passed entity</returns>
        public TEntity SelectObject<TEntity>(int id)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Get<TEntity>(id);
            }
        }

        /// <summary>
        /// Delete object
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="id">id</param>
        /// <returns>The number of records affected</returns>
        public int Delete<TEntity>(int id)
        {
            using (var connection = GetOpenConnection())
            {
               return connection.Delete<TEntity>(id);
            }
        }

        /// <summary>
        /// Delete object
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="id">id</param>
        /// <returns>The number of records affected</returns>
        public int DeleteList<TEntity>(string conditions)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.DeleteList<TEntity>($"where {conditions}");
            }
        }

        /// <summary>
        /// The dispose method that implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// The virtual dispose method that allows
        /// classes inherited from this one to dispose their resources.
        /// </summary>
        /// <param name="disposing">Is Dispose</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here.
                }
                // Dispose unmanaged resources here.
            }
            this.disposed = true;
        }
    }
}
