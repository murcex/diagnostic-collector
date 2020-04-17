using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Blob
{
    class Initi
    {
        public void execute_account(string account)
        {

        }

        public void execute_container(string account, string container)
        {

        }

        public void execute_containers(string account, List<string> containers)
        {

        }

        public void execute_accounts(Dictionary<string, string> accounts)
        {

        }
    }

    class AccountManager
    {
        private ConcurrentDictionary<string, Account> clients = new ConcurrentDictionary<string, Account>();

        public bool AddAccount(Account account)
        {
            try
            {
                return clients.TryAdd(account.Name, account);
            }
            catch
            {
                return false;
            }
        }

        public Account GetAccount(string account)
        {
            Account blobAccount = null;

            try
            {
                clients.TryGetValue(account.ToUpper(), out blobAccount);

                return blobAccount;
            }
            catch
            {
                return blobAccount;
            }
        }

    }

    class Account
    {
        public string Name { get; private set; }

        private CloudBlobClient client;

        private Dictionary<string, CloudBlobContainer> containers = new Dictionary<string, CloudBlobContainer>();

        public bool AddAccount(string account)
        {
            try
            {
                Name = account.ToUpper();

                client = CloudStorageAccount.Parse(account).CreateCloudBlobClient();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool AddContainer(string container)
        {
            try
            {
                CloudBlobContainer containerReference = client.GetContainerReference(container.ToLower());

                return containers.TryAdd(container.ToUpper(), containerReference);
            }
            catch
            {
                return false;
            }
        }

        public CloudBlobContainer GetContainer(string container)
        {
            CloudBlobContainer blobContainer = null;

            containers.TryGetValue(container.ToUpper(), out blobContainer);

            return blobContainer;
        }
    }
}
