using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace StorageTriggerFuncWithAkv
{
    public class StorageTriggerWithAkv
    {
        private readonly IConfiguration _Configuration;
        
        public StorageTriggerWithAkv(IConfiguration configRoot)
        {
            this._Configuration = configRoot;
        }

        [FunctionName("StorageTriggerWithAkv")]
        public void Run([BlobTrigger("samples-workitems/{name}", Connection = "connstring")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            log.LogInformation($"{this._Configuration["connstring"]}");
            log.LogInformation($"{this._Configuration["connstring"]}");
            log.LogInformation($"{this._Configuration["connstring"]}");
            log.LogInformation($"{this._Configuration["connstring"]}");
            log.LogInformation($"{this._Configuration["connstring"]}");
            log.LogInformation($"{this._Configuration["connstring"]}");
            log.LogInformation($"{this._Configuration["connstring"]}");
        }
    }
}
