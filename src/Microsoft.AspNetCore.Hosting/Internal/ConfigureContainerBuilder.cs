// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;

namespace Microsoft.AspNetCore.Hosting.Internal
{
    public class ConfigureContainerBuilder
    {
        public ConfigureContainerBuilder(MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            MethodInfo = methodInfo;
        }

        private MethodInfo MethodInfo { get; }

        public void Invoke(object instance, object builder, Type builderType)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var parameters = MethodInfo.GetParameters();

            if (parameters.Length != 1 || parameters[0].ParameterType.IsAssignableFrom(builderType))
            {
                throw new InvalidOperationException($"The ConfigureContainer method must take only one parameter of type {builderType.Name}.");
            }

            var arguments = new object[parameters.Length];

            if (arguments.Length > 0)
            {
                arguments[0] = builder;
            }

            MethodInfo.Invoke(instance, arguments);
        }
    }
}
