using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace sell.database
{
    public interface IDbContext
    {
        /// <summary>
        /// 查出一条记录
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        dynamic SelectFirst(string sql);

        /// <summary>
        /// 查出多条记录
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<dynamic> Select(string sql);


        /// <summary>
        /// 查出一条记录的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        T SelectFirst<T>(string sql) where T : class, new();

        /// <summary>
        /// 查出一条记录的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        T SelectFirst<T>(string sql, object obj) where T : class, new();


        /// <summary>
        /// 查出多条记录的实体泛型集合
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> Select<T>(string sql) where T : class, new();


        Task<IEnumerable<T>> Select<T>(string sql, object obj) where T : class, new();


        /// <summary>
        /// 同时查询多张表数据（高级查询）
        /// "select *from K_City;select *from K_Area";
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        SqlMapper.GridReader SelectMultiple(string sql);

        /// <summary>
        /// 单，多表 更新（事务），执行原生sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<int> Update(string sql);

        /// <summary>
        /// 单表批量更新（事务），泛型T集合作为参数
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="sql">sql语句（sql="update Person set password='ddd' where id=@id",</param>
        /// <param name="obj">参数类（obj=new{id=1}）</param>
        /// <returns></returns>
        Task<int> Update<T>(string sql, object obj) where T : class, new();

        /// <summary>
        /// 多表 批量插入（事物），可以是泛型T集合数据
        /// </summary>
        /// <param name="param">二元组集合参数</param>
        /// <returns></returns>
        int Update(List<Tuple<string, object>> param);


        /// <summary>
        /// 单，多表删除（事物），原生sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Delete(string sql);

        /// <summary>
        /// 单表批量删除（事物），泛型T集合作为参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        int Delete<T>(string sql, object obj) where T : class, new();

        /// <summary>
        /// 多表批量删除（事物），可以是泛型T集合数据
        /// </summary>
        /// <param name="param">二元组参数</param>
        /// <returns></returns>
        int Delete(List<Tuple<string, object>> param);


        /// <summary>
        /// 单，多表插入（事物），原生sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Insert(string sql);

        /// <summary>
        /// 单表批量插入（事物），泛型T集合作为参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        int Insert<T>(string sql, object obj) where T : class, new();

        /// <summary>
        /// 多表批量插入（事物），可以是泛型T集合数据
        /// </summary>
        /// <param name="param">二元组参数</param>
        /// <returns></returns>
        int Insert(List<Tuple<string, object>> param);
    }
}
