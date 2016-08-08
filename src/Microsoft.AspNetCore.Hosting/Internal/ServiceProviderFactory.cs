// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Hosting.Internal
{
    public class ServiceProviderFactory : ServiceProviderFactory<IServiceCollection>
    {
        protected override IServiceCollection CreateContainerBuilder(IServiceCollection services)
        {
            return services;
        }

        protected override IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }
}
