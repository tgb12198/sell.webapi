using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using sell.database;
using sell.web.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace sell.web
{
    public class AutofacConfig
    {
       public static void RegisterDependencies()
        {
            #region 自动注入
            //创建autofac管理注册类的容器实例
            var builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(Assembly.LoadFrom).ToArray();
            //注册所有实现了 IDependency 接口的类型
            Type baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies)
                       .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                       .AsSelf().AsImplementedInterfaces()
                       .PropertiesAutowired().InstancePerLifetimeScope();


            var iService = Assembly.Load("sell.web.IService");
            var service = Assembly.Load("sell.web.Service");
            var iRepository = Assembly.Load("sell.web.IRepository");
            var repository = Assembly.Load("sell.web.Repository");

            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(iService, service)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();

            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(iRepository, repository)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces();

            //注册数据操作类
            builder.Register(x => new DbContext()).As<IDbContext>();

            //注册MVC类型
            builder.RegisterControllers(assemblies).PropertiesAutowired();
            //注册MVC api类型
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());  //注入所有Controller
            //builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterFilterProvider();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
        }
    }
}