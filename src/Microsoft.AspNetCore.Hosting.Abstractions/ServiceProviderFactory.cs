// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Hosting
{
    public abstract class ServiceProviderFactory<TBuilder> : IServiceProviderFactory
    {
        Type IServiceProviderFactory.ContainerBuilderType => typeof(TBuilder);

        object IServiceProviderFactory.CreateContainerBuilder(IServiceCollection services) => CreateContainerBuilder(services);

        IServiceProvider IServiceProviderFactory.CreateServiceProvider(object containerBuilder) => CreateServiceProvider((TBuilder) containerBuilder);

        protected abstract TBuilder CreateContainerBuilder(IServiceCollection services);

        protected abstract IServiceProvider CreateServiceProvider(TBuilder containerBuilder);
    }
}
