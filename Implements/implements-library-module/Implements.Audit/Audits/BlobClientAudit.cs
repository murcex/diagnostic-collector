namespace Implements.Audit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection.Metadata;
    using System.Text;
    using Implements.Substrate.Blob;
    class BlobClientAudit
    {
        /// -----
        /// 1) Create a new Azure Storage Blob account
        /// 2) Copy the storage account creds into the "Account" and "Container" vars below
        /// -----
        
        // *** ADD STORAGE LOGIN + KEY HERE ***
        private const string Account = "";

        // *** ADD CONTAINER HERE -- IT SHOULD NOT EXIST! ***
        private const string Container = "Audit";

        private const string Profile = "audit";

        // --- blob items ---
        private static string s_blob_1 = @"test.txt";
        private static string s_blob_2 = @"l1\test.txt";
        private static string s_blob_3 = @"l1\l2\test.txt";

        private static string s_blob_4 = @"testupdate1.txt";

        private static string s_blob_6 = @"testrename1.txt";
        private static string s_blob_7 = @"testrename2.txt";

        private static string s_blob_8 = @"testcopy1.txt";
        private static string s_blob_9 = @"testcopy2.txt";

        private static string s_blob_10 = @"testcopydelete1.txt";
        private static string s_blob_11 = @"testcopydelete2.txt";

        private static string s_blob_12 = @"testdelete1.txt";

        private static string blob1_string;
        private static string blob2_string;
        private static byte[] blob1_byte;
        private static byte[] blob2_byte;

        private List<string> blobs_all = new List<string>
        {
            s_blob_1,
            s_blob_2,
            s_blob_3
        };

        public static void Execute(bool execute)
        {
            if (!execute)
            {
                Console.WriteLine($"Audit: {nameof(BlobClientAudit)} has not been marked for execution, skipping audit.");
                Console.WriteLine($"*** PRESS ANY KEY TO CONTINUE *** \r\n");
                Console.ReadKey();

                return;
            }

            Console.WriteLine($"Starting Storage Blob Audit.");

            // initialize
            var initiClient = BlobClient.Initialize(Profile, Account, Container).GetAwaiter().GetResult();
            Assert("Initialize", initiClient, true);

            // create container
            ManualCheck("Confirm Container doesn't exist");
            var testCreateContainer = BlobClient.CreateContainer(Profile);
            Assert($"CreateContainer operation", testCreateContainer, true);
            ManualCheck("Confirm Container has been created");

            // --- insert ---
            // insert root
            var insertDocument_1 = BlobClient.InsertBlob(Profile, s_blob_1, blob1_byte).GetAwaiter().GetResult();
            Assert("Insert root", insertDocument_1, true);
            ManualCheck($"Verify blob insert");

            // insert root\prefix

            // insert root\prefix\prefix

            // insert duplicate check root

            // insert duplicate check root\prefix


            var loadAllResult = LoadAllTestBlobs();

            // --- select ---
            // select document from root
            var testSelect_1 = BlobClient.SelectBlob(Profile, s_blob_1).GetAwaiter().GetResult();
            var testSelect_2 = BlobClient.ByteToString(testSelect_1);
            //Console.WriteLine($"Select root Assert: {Equals(checkContents_1, blob1_string)}");

            // select document from root\prefix

            // select document from root\prefix\prefix


            // --- update ---
            // update existing document
            var testUpdate_1 = BlobClient.SelectBlob(Profile, s_blob_4).GetAwaiter().GetResult();
            var testUpdate_2 = BlobClient.UpdateBlob(Profile, s_blob_4, blob2_byte).GetAwaiter().GetResult();
            var testUpdate_3 = BlobClient.SelectBlob(Profile, s_blob_4).GetAwaiter().GetResult();
            Assert("Check Blob before update contents", testUpdate_1, blob1_byte);
            Assert("UpdateBlob operation", testUpdate_2, true);
            Assert("Check Blob after update contents", testUpdate_3, blob2_byte);
            ManualCheck("UpdateBlob");

            // rename existing document
            var testRename_1 = BlobClient.SelectBlob(Profile, s_blob_6).GetAwaiter().GetResult();
            var testRename_2 = BlobClient.RenameBlobInContainer(Profile, s_blob_6, s_blob_7).GetAwaiter().GetResult();
            var testRename_3 = BlobClient.SelectBlob(Profile, s_blob_6).GetAwaiter().GetResult();
            var testRename_4 = BlobClient.SelectBlob(Profile, s_blob_7).GetAwaiter().GetResult();
            Assert("RenameDocumentInContainer operation", testRename_2, true);
            Assert("Check if original Blob has been removed", testRename_3, null);
            Assert("Check if original and new Blob are identical", testRename_1, testRename_4);
            ManualCheck("RenameBlobInContainer");

            // copy existing document
            var testCopy_1 = BlobClient.SelectBlob(Profile, s_blob_8).GetAwaiter().GetResult();
            var testCopy_2 = BlobClient.CopyBlobInContainer(Profile, s_blob_8, s_blob_9).GetAwaiter().GetResult();
            var testCopy_3 = BlobClient.SelectBlob(Profile, s_blob_8).GetAwaiter().GetResult();
            var testCopy_4 = BlobClient.SelectBlob(Profile, s_blob_9).GetAwaiter().GetResult();
            Assert("CopyBlobInContainer operation", testCopy_2, true);
            Assert("Check if original blob is unchanged", testCopy_1, testCopy_3);
            Assert("Check if original and copied blob are identical", testCopy_3, testCopy_4);
            ManualCheck("CopyBlobInContainer");

            // copy existing doucment with delete after copy
            var testCopyDelete_1 = BlobClient.SelectBlob(Profile, s_blob_10).GetAwaiter().GetResult();
            var testCopyDelete_2 = BlobClient.CopyBlobInContainer(Profile, s_blob_10, s_blob_11, deleteSource: true).GetAwaiter().GetResult();
            var testCopyDelete_3 = BlobClient.SelectBlob(Profile, s_blob_10).GetAwaiter().GetResult();
            var testCopyDelete_4 = BlobClient.SelectBlob(Profile, s_blob_11).GetAwaiter().GetResult();
            //Console.WriteLine($"Assert: {Equals(testRename_2, true)}");
            //Console.WriteLine($"Assert: {Equals(testRename_1, testRename_3)}");
            ManualCheck("CopyBlobInContainer, with deleteSource: true");

            // --- list ---
            // list container
            var listBlobs_1 = BlobClient.SelectBlobList(Profile);
            var listDoucmentCount = listBlobs_1.Count;
            //if (files_all.Contains(listDocuments_1))
            //Console.WriteLine($"Assert: {Equals(listDoucmentCount, true)}");

            // list root\prefix

            // list root\prefix\prefix


            // --- delete ---
            // delete root + confirm root\prefix, root\prefix\prefix available
            var testDelete_1 = BlobClient.DeleteBlob(Profile, s_blob_1).GetAwaiter().GetResult();
            var testDelete_2 = BlobClient.SelectBlob(Profile, s_blob_1).GetAwaiter().GetResult();
            //Console.WriteLine($"Delete + Confirm root Assert: {Equals(deleteDocument_1, true)}, {Equals(deleteCheck_1, null)}");

            // delete root\prefix + confirm root\prefix\prefix available

            // delete root\prefix\prefix


            // --- container clean up ---
            // delete container
            var testDeleteContainer_1 = BlobClient.DeleteContainer(Profile).GetAwaiter().GetResult();
            //Console.WriteLine($"Delete Container Assert: {Equals(deleteContainer, true)}");
            ManualCheck("Container has been deleted.");
        }

        private static void BuildBlobs()
        {
            blob1_string = Guid.NewGuid().ToString();
            blob2_string = Guid.NewGuid().ToString();

            blob1_byte = BlobClient.StringToByte(blob1_string);
            blob2_byte = BlobClient.StringToByte(blob2_string);
        }

        private static void Assert(string eventName, object objectA, object objectB)
        {
            var result = Equals(objectA, objectB);

            var resultMessage = result == true ? "PASS" : "FAIL";

            Console.WriteLine($"Assert: {eventName} - {resultMessage}");
        }

        private static void ManualCheck(string message)
        {
            Console.WriteLine($"Manual Check: {message} \r\n");
            Console.WriteLine($"*** PRESS ANY KEY TO CONTINUE *** \r\n");
            Console.ReadKey();
        }

        private static bool LoadAllTestBlobs()
        {
            if (!BlobClient.InsertBlob(Profile, s_blob_6, blob1_byte).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, s_blob_8, blob1_byte).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, s_blob_10, blob1_byte).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, s_blob_12, blob1_byte).GetAwaiter().GetResult())
            {
                return false;
            }

            return true;
        }
    }
}
