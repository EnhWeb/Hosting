// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Hosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = WebApplicationConfiguration.GetDefault(args);

            var application = new WebApplicationBuilder()
                .UseConfiguration(config)
                .Build();

            application.Run();
        }
    }
}
