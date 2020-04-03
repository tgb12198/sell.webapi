using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Data;

namespace sell.database
{
    public class DbConnectionFactory
    {
        //接收的数据库类型
        private static string _dbType = string.Empty;

        //数据库连接名
        private static string _connection = string.Empty;

        //获取连接名
        private static string Connection
        {
            get { return _connection; }
        }

        //返回连接实例
        private static IDbConnection dbConnection = null;

        //静态变量保存类的实例
        private static DbConnectionFactory instance;

        //定义一个标识确保线程同步
        private static readonly object locker = new object();

        /// <summary>
        /// 私有构造函数，使外界不能创建该类的实例
        /// </summary>
        private DbConnectionFactory()
        {
            string dbConnection = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            if (!string.IsNullOrEmpty(dbConnection))
            {
                //接收：数据库类型
                _dbType = ConfigurationManager.ConnectionStrings["connStr"].ProviderName;
                //接收：数据库连接名
                _connection = dbConnection;
            }
        }

        /// <summary>
        /// 全局访问点
        /// </summary>
        /// <returns></returns>
        public static DbConnectionFactory GetInstance()
        {
            // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
            // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            // 双重锁定只需要一句判断就可以了
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new DbConnectionFactory();
                    }
                }
            }
            return instance;
        }

        public static IDbConnection OpenCurrentDbConnection()
        {
            switch (_dbType.ToUpper())
            {
                case "SQLSERVER":
                    if (dbConnection == null)
                    {
                        dbConnection = new SqlConnection(Connection);
                    }
                    break;
                case "MYSQL":
                    if (dbConnection == null)
                    {
                        dbConnection = new MySqlConnection(Connection);
                    }
                    break;
                default:
                    break;
            }
            //判断连接状态
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }

        /// <summary>
        /// 打开自定义配置的数据库类型和连接名
        /// </summary>
        /// <param name="dbConnectionString">连接内容</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static IDbConnection OpenCurrentDbConnection(string dbConnectionString, DbType dbType = DbType.SqlServer)
        {
            switch (dbType.ToString().ToUpper())
            {
                case "SQLSERVER":
                    if (dbConnection == null)
                    {
                        dbConnection = new SqlConnection(dbConnectionString);
                    }
                    break;
                case "MYSQL":
                    if (dbConnection == null)
                    {
                        dbConnection = new MySqlConnection(dbConnectionString);
                    }
                    break;
                default:
                    break;
            }
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }
    }
}

/// <summary>
/// 数据库类型
/// </summary>
public enum DbType
{
    SqlServer = 1,
    Oracle = 2,
    MySql = 3
}
