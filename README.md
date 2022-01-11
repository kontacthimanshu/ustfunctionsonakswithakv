### Azure Functions on Azure Kubernetes Services with KEDA and Secrets from Azure Key Vault
#### This POC demonstrates hosting Azure Functions written in C# on Azure Kubernetes Services with KEDA and secrets hosted in Azure Key Vault.
### Pre-requisites
#### 1. An Azure Subscription with contributor access
#### 2. Latest version of Azure Command Line Interface (CLI)
#### 3. Azure Functions Core Tools
#### 4. Docker Desktop Community Edition or higher
#### 5. Kubectl on client machine
#### ----------------------------------------------------------------------------------------------------------------------------------------
### High Level Steps
#### 1. Create a Resource Group
#### 2. Create a Managed Identity (hereinafter called MI)
#### 3. Get MI's userID, resourceID and service principal ID
#### 4. Create a Storage Account
#### 5. Create a Blob Container in it
#### 6. Create an Azure Container Registry (hereinafter called ACR)
#### 7. Get the resource ID of the ACR
#### 8. Assign the MI created above a role to read from ACR (Skip if you don't want control plaine MI to access ACR)
#### 9. Get connection string of the Storage Account
#### 10. Create an Azure Key Vault (hereinafter called AKV). Make a note of the tenant ID of the AKV displayed in the output of the command.
#### 11. Create following secrets in AKV which will store the connection string of the storage account
##### ----. secret name: AzureWebJobsStorage, secret value: connection string obtained above
##### ----. secret name: storageconstr, secret value: connection string obtained above
#### 14. Create AKS with following add-ons:
##### ----. Managed Identity
##### ----. Azure Key Vault Provider for Secrets Store CSI Driver
#### 15. Download AKS cluster credentials and update current kubectl context
#### 16. Install KEDA in AKS cluster
#### 17. Get the client ID and service principal ID of the MI of kubelet (autocreated by AKS for your, you can choose to have pre-created MI for Kubelet as well). Make a note of it.
#### 18. Assign the kubelet MI roles to read from AKV
#### 19. Assign the kubelet MI roles to read from ACR
#### 21. Clone the repo repo URL
#### 20. Create SecretProviderClass which will do
##### ----. Sync secrets from AKV to POD as volume
##### ----. Sync secrets from AKV to K8s secret created above (Do not pre-create the secret, secret will be auto created and destroyed by the CSI Driver)
#### 22. Run following commands to run function PODs
##### ----. Build the docker image
##### ----. Push the docker image
#### 23. Create the deployment
#### 24. Upload few files in the blob container in your storage account
#### 25. Check the logs of the pods to see if the file got processed
#### ----------------------------------------------------------------------------------------------------------------------------------------
### Steps with Commands
#### ----------------------------------------------------------------------------------------------------------------------------------------
#### --- Create a Resource Group
##### - az group create -l eastus
#### --- Create a Managed Identity (hereinafter called MI)
##### - az identity create --name ustdemomi --resource-group ustdemo
#### --- - Copy the output of the above command and keep it as a note
#### --- - Get the resource ID and service principal Id of the MI
##### - $miresourceId=$(az identity show --resource-group ustdemo --name ustdemomi --query id --output tsv)
##### - $MiSpId=$(az identity show --resource-group ustdemo --name ustdemomi --query principalId --output tsv)
#### --- - Create a Storage Account
##### - az storage account create -n ustfuncdemostorage -g ustdemo -l eastus --sku Standard_LRS
#### --- - Create a blob container in the above storage account
##### - az storage account keys list -g ustdemo -n ustfuncdemostorage
#### --- - Copy the value of one of the key listed as output of above command and update the command parameter "account key" in the command below:
##### - az storage container create -n usttestcontainer --account-name ustfuncdemostorage --public-access off --resource-group ustdemo --auth-mode key --account-key ""
#### --- - Run the above command you just updated. 
#### --- - Create an Azure Container Registry (hereinafter called ACR)
##### - az acr create -n ustdemoacr -g ustdemo -l eastus --sku standard
#### --- - Get the resource ID of the ACR
##### - $acrresourceID =$(az acr show --resource-group ustdemo --name ustdemoacr --query id --output tsv)
#### --- - Assign the MI a role on the ACR to pull images
##### - az role assignment create --assignee $MiSpId --scope $acrresourceID --role acrpull
#### --- - Get connection string of the Storage Account (Copy the value of the connection string and create PowerShell variable)
##### - az storage account show-connection-string --name ustfuncdemostorage
##### - $connstr = "paste the connection string that you copied from output of previous command"
#### --- - Create an Azure Key Vault (hereinafter called AKV). Make a note of the tenant ID of the AKV displayed in the output of the command.
##### - az keyvault create -n ustdemoazkv -g ustdemo -l eastus
#### --- - Create following secrets in AKV which will store the connection string of the storage account
##### - az keyvault secret set --vault-name ustdemoazkv -n AzureWebJobsStorage --value $connstr
##### - az keyvault secret set --vault-name ustdemoazkv -n storageconstr --value $connstr

