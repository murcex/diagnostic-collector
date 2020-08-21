namespace Implements.Audit
{
    using System;
    using System.Collections.Generic;
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

        // --- 
        // blob names 
        // ---

        // core blob: insert, select and delete
        private static string Blob_Core { get; set; }

        // list blobs
        private static string Blob_List_1 { get; set; }
        private static string Blob_List_2 { get; set; }
        private static string Blob_List_3 { get; set; }

        // updating existing blob.
        private static string Blob_Update { get; set; }

        // renaming blobs
        private static string Blob_Rename_1 { get; set; }
        private static string Blob_Rename_2 { get; set; }

        // copy blobs
        private static string Blob_Copy_1 { get; set; }
        private static string Blob_Copy_2 { get; set; }

        // copy with delete blobs
        private static string Blob_CopyDelete_1 { get; set; }
        private static string Blob_CopyDelete_2 { get; set; }

        // unused..
        private static string Blob_Unused { get; set; } // TODO: remove?

        // blob body contents as string
        private static string Blob_Body_1 { get; set; }
        private static string Blob_Body_2 { get; set; }

        // blob body contents as byte[]
        private static byte[] Blob_Byte_1 { get; set; }
        private static byte[] Blob_Byte_2 { get; set; }

        // list of blobs
        private static List<string> Blobs_List { get; set; }

        public static void Execute(bool execute)
        {
            if (!execute)
            {
                Console.WriteLine($"Audit: {nameof(BlobClientAudit)} has not been marked for execution, skipping audit.");
                Console.WriteLine($"*** PRESS ANY KEY TO CONTINUE *** \r\n");
                Console.ReadKey();

                return;
            }

            var setup = SetBlobFileNames() && BuildBlobContent() && LoadAllTestBlobs();
            Assert("Test Setup", setup, true);

            if (!setup)
            {
                Console.ReadKey();

                return;
            }

            Console.WriteLine($"Starting Storage Blob Audit.");

            // ---
            // CORE
            // ---

            // initialize
            var initiClient = BlobClient.Initialize(Profile, Account, Container).GetAwaiter().GetResult();
            Assert("Initialize", initiClient, true);

            // create container
            ManualCheck("Confirm Container doesn't exist");
            var testCreateContainer = BlobClient.CreateContainer(Profile);
            Assert($"CreateContainer operation", testCreateContainer, true);
            ManualCheck("Confirm Container has been created");






            // --- 
            // INSERT
            // ---

            // insert root
            var insertDocument_1 = BlobClient.InsertBlob(Profile, Blob_Core, Blob_Byte_1).GetAwaiter().GetResult();
            Assert("Insert root", insertDocument_1, true);
            ManualCheck($"Verify blob insert");

            // insert root\prefix

            // insert root\prefix\prefix

            // insert duplicate check root

            // insert duplicate check root\prefix







            // ---
            // SELECT
            // ---

            // select document from root
            var testSelect_1 = BlobClient.SelectBlob(Profile, Blob_Core).GetAwaiter().GetResult();
            var testSelect_2 = BlobClient.ByteToString(testSelect_1);
            Assert("Select root byte", testSelect_1, Blob_Byte_1);
            Assert("Select root as string", testSelect_2, Blob_Body_1);
            ManualCheck("Select blob");

            // select document from root\prefix

            // select document from root\prefix\prefix






            // ---
            // UPDATE
            // ---

            // update existing document
            var testUpdate_1 = BlobClient.SelectBlob(Profile, Blob_Update).GetAwaiter().GetResult();
            var testUpdate_2 = BlobClient.UpdateBlob(Profile, Blob_Update, Blob_Byte_2).GetAwaiter().GetResult();
            var testUpdate_3 = BlobClient.SelectBlob(Profile, Blob_Update).GetAwaiter().GetResult();
            Assert("Check Blob before update contents", testUpdate_1, Blob_Byte_1);
            Assert("UpdateBlob operation", testUpdate_2, true);
            Assert("Check Blob after update contents", testUpdate_3, Blob_Byte_2);
            ManualCheck("UpdateBlob");





            // ---
            // RENAME
            // ---

            // rename existing document
            var testRename_1 = BlobClient.SelectBlob(Profile, Blob_Rename_1).GetAwaiter().GetResult();
            var testRename_2 = BlobClient.RenameBlobInContainer(Profile, Blob_Rename_1, Blob_Rename_2).GetAwaiter().GetResult();
            var testRename_3 = BlobClient.SelectBlob(Profile, Blob_Rename_1).GetAwaiter().GetResult();
            var testRename_4 = BlobClient.SelectBlob(Profile, Blob_Rename_2).GetAwaiter().GetResult();
            Assert("Confirm Base Select", testRename_1, Blob_Byte_1);
            Assert("RenameDocumentInContainer operation", testRename_2, true);
            Assert("Check if original Blob has been removed", testRename_3, null);
            Assert("Check if original and new Blob are identical", testRename_1, testRename_4);
            ManualCheck("RenameBlobInContainer");





            // ---
            // COPY
            // ---

            // copy existing document
            var testCopy_1 = BlobClient.SelectBlob(Profile, Blob_Copy_1).GetAwaiter().GetResult();
            var testCopy_2 = BlobClient.CopyBlobInContainer(Profile, Blob_Copy_1, Blob_Copy_2).GetAwaiter().GetResult();
            var testCopy_3 = BlobClient.SelectBlob(Profile, Blob_Copy_1).GetAwaiter().GetResult();
            var testCopy_4 = BlobClient.SelectBlob(Profile, Blob_Copy_2).GetAwaiter().GetResult();
            Assert("Confirm Base Select", testCopy_1, Blob_Byte_1);
            Assert("CopyBlobInContainer operation", testCopy_2, true);
            Assert("Check if original blob is unchanged", testCopy_1, testCopy_3);
            Assert("Check if original and copied blob are identical", testCopy_3, testCopy_4);
            ManualCheck("CopyBlobInContainer");

            // copy existing doucment with delete after copy
            var testCopyDelete_1 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_1).GetAwaiter().GetResult();
            var testCopyDelete_2 = BlobClient.CopyBlobInContainer(Profile, Blob_CopyDelete_1, Blob_CopyDelete_2, deleteSource: true).GetAwaiter().GetResult();
            var testCopyDelete_3 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_1).GetAwaiter().GetResult();
            var testCopyDelete_4 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_2).GetAwaiter().GetResult();
            Assert("Confirm Base Select", testCopyDelete_1, Blob_Byte_1);
            Assert("Confirm Copy with Delete operation", testCopyDelete_2, true);
            Assert("Confirm Original Deleted", testCopyDelete_3, null);
            Assert("Confirm Copy Select", testCopyDelete_4, Blob_Byte_1);
            ManualCheck("CopyBlobInContainer, with deleteSource: true");






            // ---
            // LIST
            // ---

            // list container
            var listBlobs_1 = BlobClient.SelectBlobList(Profile);
            var listDoucmentCount = listBlobs_1.Count;
            Assert("List Contains Check", AssertLists(listBlobs_1, Blobs_List), true);
            Assert("List Doucment Count", listDoucmentCount, Blobs_List.Count);
            ManualCheck("List Blobs");

            // list root\prefix

            // list root\prefix\prefix






            // --- 
            // DELETE
            // ---

            // delete root + confirm root\prefix, root\prefix\prefix available
            var testDelete_1 = BlobClient.DeleteBlob(Profile, Blob_Core).GetAwaiter().GetResult();
            var testDelete_2 = BlobClient.SelectBlob(Profile, Blob_Core).GetAwaiter().GetResult();
            Assert("Delete root", testDelete_1, true);
            Assert("Confirm Delete root", testDelete_2, null);
            ManualCheck("Delete Blob");

            // delete root\prefix + confirm root\prefix\prefix available

            // delete root\prefix\prefix






            // --- 
            // CLEAN-UP
            // ---

            // delete container
            var testDeleteContainer_1 = BlobClient.DeleteContainer(Profile).GetAwaiter().GetResult();
            Assert("Delete Container", testDeleteContainer_1, true);
            ManualCheck("Container has been deleted.");
        }





        // ---
        // TEST UTILITY
        // ---

        // assert utility for audit
        private static void Assert(string eventName, object objectA, object objectB)
        {
            var result = Equals(objectA, objectB);

            var resultMessage = result == true ? "PASS" : "FAIL";

            Console.WriteLine($"Assert: {eventName} - {resultMessage}");
        }

        // manual check point step
        private static void ManualCheck(string message)
        {
            Console.WriteLine($"Manual Check: {message} \r\n");
            Console.WriteLine($"*** PRESS ANY KEY TO CONTINUE *** \r\n");
            Console.ReadKey();
        }

        private static bool AssertLists(List<string> listA, List<string> listB)
        {
            foreach (var item in listA)
            {
                if (!listB.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }






        // ---
        // DATA UTILITY
        // ---

        private static bool SetBlobFileNames()
        {
            Blob_Core = @"test.txt";

            Blob_List_1 = @"l1\test.txt";
            Blob_List_2 = @"l1\l2\test.txt";

            Blob_Update = @"testupdate.txt";

            Blob_Rename_1 = @"testrename1.txt";
            Blob_Rename_2 = @"testrename2.txt";

            Blob_Copy_1 = @"testcopy1.txt";
            Blob_Copy_2 = @"testcopy2.txt";

            Blob_CopyDelete_1 = @"testcopydelete1.txt";
            Blob_CopyDelete_2 = @"testcopydelete2.txt";

            Blob_Unused = @"testdelete.txt";

            Blobs_List = new List<string> { Blob_Core, Blob_List_1, Blob_List_2 };

            return true;
        }

        private static bool BuildBlobContent()
        {
            // sample blob body as string
            Blob_Body_1 = Guid.NewGuid().ToString();
            Blob_Body_2 = Guid.NewGuid().ToString();

            // sample blob body as byte[]
            Blob_Byte_1 = BlobClient.StringToByte(Blob_Body_1);
            Blob_Byte_2 = BlobClient.StringToByte(Blob_Body_2);

            return true;
        }

        private static bool LoadAllTestBlobs()
        {
            if (!BlobClient.InsertBlob(Profile, Blob_Rename_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_Copy_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_CopyDelete_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_Unused, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            return true;
        }
    }
}
