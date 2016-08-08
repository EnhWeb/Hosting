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

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }

    public abstract class StartupBase<TBuilder> : IStartup
    {
        protected StartupBase(IServiceProviderFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (!IsCompatible(factory))
            {
                throw new ArgumentException("Startup class is not compatible with the current IServiceProviderFactory.", nameof(factory));
            }

            Factory = factory;
        }

        private IServiceProviderFactory Factory { get; }

        IServiceProvider IStartup.ConfigureServices(IServiceCollection services)
        {
            ConfigureServices(services);

            var builder = Factory.CreateContainerBuilder(services);

            ConfigureContainer((TBuilder) builder);

            return Factory.CreateServiceProvider(builder);
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