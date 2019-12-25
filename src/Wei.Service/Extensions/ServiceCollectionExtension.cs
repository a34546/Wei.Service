using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wei.Repository;

namespace Wei.Service
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAppService(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var assemblies = AppDomain.CurrentDomain.GetCurrentPathAssembly().Where(x => !(x.GetName().Name.Equals("Wei.Service")));
            services.AddAppService(assemblies, typeof(IAppService<,>), serviceLifetime);
            services.AddAppService(assemblies, typeof(IAppService<,,>), serviceLifetime);
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(typeof(IAppService<,>), typeof(AppService<,>));
                    services.AddSingleton(typeof(IAppService<,,>), typeof(AppService<,,>));
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(typeof(IAppService<,>), typeof(AppService<,>));
                    services.AddScoped(typeof(IAppService<,,>), typeof(AppService<,,>));
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(typeof(IAppService<,>), typeof(AppService<,>));
                    services.AddTransient(typeof(IAppService<,,>), typeof(AppService<,,>));
                    break;
            }
            return services;
        }

        private static void AddAppService(this IServiceCollection services, IEnumerable<Assembly> assemblies, Type baseType, ServiceLifetime serviceLifetime)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                                    .Where(x => x.IsClass
                                            && !x.IsAbstract
                                            && x.BaseType != null
                                            && x.HasImplementedRawGeneric(baseType));
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();
                    var interfaceType = interfaces.FirstOrDefault(x => x.Name == $"I{type.Name}");
                    if (interfaceType == null) interfaceType = type;
                    var serviceDescriptor = new ServiceDescriptor(interfaceType, type, serviceLifetime);
                    if (!services.Contains(serviceDescriptor)) services.Add(serviceDescriptor);
                }
            }
        }
    }
}
