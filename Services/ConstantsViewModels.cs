using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services
{
   public class ConstantsViewModels
    {
        private static readonly IConfiguration AppSeetingconfig = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json").Build();

        private static readonly IConfiguration config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings." + AppSeetingconfig["Environment"] + ".json").Build();

        //.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings." + EnvName + ".json").Build();
        //public static string Env => AppSeetingconfig["Environment"];

        //Base url
        public static string BaseUrl => config["App:BaseUrl"];
        public static string ConnectionString => config["App:ConnectionString"];
    }
}
