using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace azstoragetriggerdemo
{
    public class azstoragefunc
    {
        [FunctionName("azstoragefunc")]
        public void Run([BlobTrigger("usttestcontainer/{name}", Connection = "storageconstr")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
