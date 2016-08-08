// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Hosting.Internal
{
    public class ConfigureServicesBuilder
    {
        public ConfigureServicesBuilder(MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; }

        public Func<IServiceCollection, IServiceProvider> Build(object instance) => services => Invoke(instance, services);

        private IServiceProvider Invoke(object instance, IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var parameters = MethodInfo.GetParameters();

            if (parameters.Length > 1 || parameters.Any(p => p.ParameterType != typeof(IServiceCollection)))
            {
                throw new InvalidOperationException("The ConfigureServices method must either be parameterless or take only one parameter of type IServiceCollection.");
            }

            var arguments = new object[parameters.Length];

            if (arguments.Length > 0)
            {
                arguments[0] = services;
            }

            return MethodInfo.Invoke(instance, arguments) as IServiceProvider;
        }
    }
}
