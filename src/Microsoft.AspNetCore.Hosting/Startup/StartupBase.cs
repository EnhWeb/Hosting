// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Hosting
{
    public abstract class StartupBase : IStartup
    {
        public abstract void Configure(IApplicationBuilder app);

        IServiceProvider IStartup.ConfigureServices(IServiceCollection services, IServiceProviderFactory factory)
        {
            return ConfigureServices(services);
        }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }

    public abstract class StartupBase<TBuilder> : IStartup
    {
        IServiceProvider IStartup.ConfigureServices(IServiceCollection services, IServiceProviderFactory factory)
        {
            if (!IsCompatible(factory))
            {
                throw new ArgumentException("Startup class is not compatible with the current IServiceProviderFactory.", nameof(factory));
            }

            ConfigureServices(services);

            var builder = factory.CreateContainerBuilder(services);

            ConfigureContainer((TBuilder) builder);

            return factory.CreateServiceProvider(builder);
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        protected virtual void ConfigureContainer(TBuilder builder)
        {
        }

        public abstract void Configure(IApplicationBuilder app);

        private static bool IsCompatible(IServiceProviderFactory factory)
        {
            return typeof(TBuilder).GetTypeInfo().IsAssignableFrom(factory.ContainerBuilderType.GetTypeInfo());
        }
    }
}