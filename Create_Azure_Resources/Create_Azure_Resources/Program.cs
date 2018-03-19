using System;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace Create_Azure_Resources
{
    class Program
    {
        static string applicationId = "{Application ID}";
        static string subscriptionId = "{Subscription ID}";
        static string tenantId = "{Tenant ID}";
        static string password = "{Password}";
        static string storageAddress = "{Storage Address URL}";
        static string rgName = "{Resource Group Name}"; 
        static string deploymentName = "{Deployment Name}";
        static void Main(string[] args)
        {
            var authContext = new AuthenticationContext(string.Format
            ("https://login.microsoftonline.com/{0}", tenantId));
            var credential = new ClientCredential(applicationId, password);
            AuthenticationResult token = authContext.AcquireTokenAsync
              ("https://management.core.windows.net/", credential).Result;

            if (token == null)
            {
                Console.WriteLine("Failed to obtain the token");
                return;
            }

            var creds = new TokenCredentials(token.AccessToken);
            var client = new ResourceManagementClient(creds);
            client.SubscriptionId = subscriptionId;

            var rgResponse = client.ResourceGroups.CreateOrUpdate(rgName,
            new ResourceGroup("East US")); 
            if (rgResponse.Properties.ProvisioningState != "Succeeded")
            {
                Console.WriteLine("Problem creating resource group");
                return;
            }

            Create_Resources(client);
            Console.ReadLine();
        }

        static void Create_Resources(ResourceManagementClient client)
        {
            var createResponse = client.Deployments.CreateOrUpdate(
            rgName,
            deploymentName,
            new Deployment()
            {
                Properties = new DeploymentProperties
                {
                    Mode = DeploymentMode.Incremental,
                    TemplateLink = new TemplateLink
                    {
                        Uri = storageAddress + "/templates/template.json"
                    },
                    ParametersLink = new ParametersLink
                    {
                        Uri = storageAddress + "/templates/parameters.json"
                    }
                }
            });

            string state = createResponse.Properties.ProvisioningState;
            Console.WriteLine("Deployment state: {0}", state);

            if (state != "Succeeded")
            {
                Console.WriteLine("Failed to create resources");
            }
        Console.WriteLine(createResponse.Properties.Outputs);

        }
    }
}
