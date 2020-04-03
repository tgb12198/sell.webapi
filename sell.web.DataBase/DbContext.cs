using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace sell.database
{
    public class DbContext : IDbContext
    {
        /// <summary>
        /// 获取开启数据库的连接
        /// </summary>
        private IDbConnection DbHelper
        {
            get
            {
                //创建单一实例
                DbConnectionFactory.GetInstance();
                return DbConnectionFactory.OpenCurrentDbConnection();
            }
        }

        /// <summary>
        /// 查出一条记录
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public dynamic SelectFirst(string sql)
        {
            return DbHelper.QueryFirst(sql);
        }

        /// <summary>
        /// 查出多条记录
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Select(string sql)
        {
            return DbHelper.Query(sql);
        }


        /// <summary>
        /// 查出一条记录的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T SelectFirst<T>(string sql) where T : class, new()
        {
            return DbHelper.QueryFirst<T>(sql);
        }

        /// <summary>
        /// 查出一条记录的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T SelectFirst<T>(string sql, object obj) where T : class, new()
        {
            return DbHelper.QueryFirst<T>(sql, obj);
        }


        /// <summary>
        /// 查出多条记录的实体泛型集合
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> Select<T>(string sql) where T : class, new()
        {
            return await DbHelper.QueryAsync<T>(sql);
        }

        public async Task<IEnumerable<T>> Select<T>(string sql, object obj) where T : class, new()
        {
            return await DbHelper.QueryAsync<T>(sql, obj);
        }


        /// <summary>
        /// 同时查询多张表数据（高级查询）
        /// "select *from K_City;select *from K_Area";
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlMapper.GridReader SelectMultiple(string sql)
        {
            return DbHelper.QueryMultiple(sql);
        }

        /// <summary>
        /// 单，多表 更新（事务），执行原生sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<int> Update(string sql)
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    rows = await DbHelper.ExecuteAsync(sql, null, DbTransaction);
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }

        /// <summary>
        /// 单表批量更新（事务），泛型T集合作为参数
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="sql">sql语句（sql="update Person set password='ddd' where id=@id",</param>
        /// <param name="obj">参数类（obj=new{id=1}）</param>
        /// <returns></returns>
        public async Task<int> Update<T>(string sql, object obj) where T : class, new()
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    rows = await DbHelper.ExecuteAsync(sql, obj, DbTransaction);
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }

        /// <summary>
        /// 多表 批量插入（事物），可以是泛型T集合数据
        /// </summary>
        /// <param name="param">二元组集合参数</param>
        /// <returns></returns>
        public int Update(List<Tuple<string, object>> param)
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    foreach (var item in param)
                    {
                        rows += DbHelper.Execute(item.Item1, item.Item2, DbTransaction);
                    }
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }


        /// <summary>
        /// 单，多表删除（事物），原生sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Delete(string sql)
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    rows = DbHelper.Execute(sql, null, DbTransaction);
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }

        /// <summary>
        /// 单表批量删除（事物），泛型T集合作为参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Delete<T>(string sql, object obj) where T : class, new()
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    rows = DbHelper.Execute(sql, obj, DbTransaction);
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }

        /// <summary>
        /// 多表批量删除（事物），可以是泛型T集合数据
        /// </summary>
        /// <param name="param">二元组参数</param>
        /// <returns></returns>
        public int Delete(List<Tuple<string, object>> param)
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    foreach (var item in param)
                    {
                        rows += DbHelper.Execute(item.Item1, item.Item2, DbTransaction);
                    }
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }


        /// <summary>
        /// 单，多表插入（事物），原生sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Insert(string sql)
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    rows = DbHelper.Execute(sql, null, DbTransaction);
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }

        /// <summary>
        /// 单表批量插入（事物），泛型T集合作为参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Insert<T>(string sql, object obj) where T : class, new()
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    rows = DbHelper.Execute(sql, obj, DbTransaction);
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }

        /// <summary>
        /// 多表批量插入（事物），可以是泛型T集合数据
        /// </summary>
        /// <param name="param">二元组参数</param>
        /// <returns></returns>
        public int Insert(List<Tuple<string, object>> param)
        {
            int rows = 0;
            using (IDbTransaction DbTransaction = DbHelper.BeginTransaction())
            {
                try
                {
                    foreach (var item in param)
                    {
                        rows += DbHelper.Execute(item.Item1, item.Item2, DbTransaction);
                    }
                }
                catch (DataException ex)
                {
                    DbTransaction.Rollback();
                }
                DbTransaction.Commit();
            }
            return rows;
        }
    }
}
