namespace Implements.Audit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Implements.Blob;

    class BlobClientAudit
    {
        /// -----
        /// 1) Create a new Azure Storage Blob account
        /// 2) Copy the storage account creds into the "Account" and "Container" vars below
        /// -----

        // *** ADD STORAGE LOGIN + KEY HERE ***
        private static string Account { get; set; } =
            "DefaultEndpointsProtocol=https;" +
            "AccountName=<accountName>;" +
            "AccountKey=<accountKey>";

        // *** ADD CONTAINER HERE -- IT SHOULD NOT EXIST! ***
        private static string Container { get; set; } = "audit";

        private static string Profile { get; set; } = "audit";

        // --- 
        // blob names 
        // ---

        // core blob: insert, select and delete
        private static string Blob_Core { get; set; }

        // list blobs
        private static string Blob_L1_1 { get; set; }
        private static string Blob_L1_2 { get; set; }
        private static string Blob_L2_1 { get; set; }
        private static string Blob_L2_2 { get; set; }
        private static string Blob_L2_2_2 { get; set; }
        private static string Blob_L2_2_3 { get; set; }
        private static string Prefix_L1 { get; set; }
        private static string Prefix_L2_1 { get; set; }
        private static string Prefix_L2_2 { get; set; }
        private static string Prefix_L2_3 { get; set; }

        // updating existing blob.
        private static string Blob_Update_1 { get; set; }
        private static string Blob_Update_2 { get; set; }
        private static string Blob_Update_3 { get; set; }

        // renaming blobs
        private static string Blob_Rename_1_1 { get; set; }
        private static string Blob_Rename_1_2 { get; set; }
        private static string Blob_Rename_2_1 { get; set; }
        private static string Blob_Rename_2_2 { get; set; }
        private static string Blob_Rename_3_1 { get; set; }
        private static string Blob_Rename_3_2 { get; set; }

        // copy blobs
        private static string Blob_Copy_1_1 { get; set; }
        private static string Blob_Copy_1_2 { get; set; }
        private static string Blob_Copy_2_1 { get; set; }
        private static string Blob_Copy_2_2 { get; set; }
        private static string Blob_Copy_3_1 { get; set; }
        private static string Blob_Copy_3_2 { get; set; }

        // copy with delete blobs
        private static string Blob_CopyDelete_1_1 { get; set; }
        private static string Blob_CopyDelete_1_2 { get; set; }
        private static string Blob_CopyDelete_2_1 { get; set; }
        private static string Blob_CopyDelete_2_2 { get; set; }
        private static string Blob_CopyDelete_3_1 { get; set; }
        private static string Blob_CopyDelete_3_2 { get; set; }

        // blob body contents as string
        private static string Blob_Body_1 { get; set; }
        private static string Blob_Body_2 { get; set; }

        // blob body contents as byte[]
        private static byte[] Blob_Byte_1 { get; set; }
        private static byte[] Blob_Byte_2 { get; set; }

        // list of blobs
        private static List<string> Blobs_List_L0 { get; set; }
        private static List<string> Blobs_List_L1 { get; set; }
        private static List<string> Blobs_List_L2 { get; set; }
        private static List<string> Prefix_List_L1 { get; set; }

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

            // ---
            // CORE
            // ---

            TestHeader("Initialize(), CreateContainer()");

            // basic setup
            var setup = SetBlobFileNames() && BuildBlobContent();
            Assert("SetBlobFileNames() & BuildBlobContent()", setup, true);
            Break();

            // initialize
            var initiClient = BlobClient.Initialize(Profile, Account, Container).GetAwaiter().GetResult();
            Assert("Initialize()", initiClient, true);
            Break();

            // create container
            ManualCheck("Confirm Container doesn't exist");
            var testCreateContainer = BlobClient.CreateContainer(Profile).GetAwaiter().GetResult();
            Assert($"CreateContainer()", testCreateContainer, true);
            ManualCheck("Confirm Container has been created");

            // --- 
            // INSERT
            // ---

            TestHeader("InsertBlob()");

            // insert root
            var insertDocument_1 = BlobClient.InsertBlob(Profile, Blob_Core, Blob_Byte_1).GetAwaiter().GetResult();
            Assert("InsertBlob() root", insertDocument_1, true);
            Break();

            // insert bulk
            var bulkInsert = BulkInsertTestBlobs();
            Assert("BulkInsertTestBlobs()", bulkInsert, true);
            Break();

            // insert duplicate root
            var insertDocument_2 = BlobClient.InsertBlob(Profile, Blob_Core, Blob_Byte_1).GetAwaiter().GetResult();
            Assert("InsertBlob() Duplicate root", insertDocument_2, false);
            Break();

            // insert duplicate level1
            var insertDocument_3 = BlobClient.InsertBlob(Profile, Blob_L2_2_3, Blob_Byte_1).GetAwaiter().GetResult();
            Assert("InsertBlob() Duplicate level1\\level2", insertDocument_3, false);

            ManualCheck($"Verify Insert Operations");

            // ---
            // LIST
            // ---

            TestHeader("SelectBlobList(), SelectPrefixList()");

            // list root
            var listBlobs_1 = BlobClient.SelectBlobList(Profile);
            var listDoucmentCount_1 = listBlobs_1.Count;
            Assert("SelectBlobList() Count root", listDoucmentCount_1, 5);
            Assert("SelectBlobList() Contains root", listBlobs_1, Blobs_List_L0);
            Break();

            // list level1
            var listBlobs_2 = BlobClient.SelectBlobList(Profile, "level1/");
            var listDoucmentCount_2 = listBlobs_2.Count;
            Assert("SelectBlobList() Count level1\\", listDoucmentCount_2, 2);
            Assert("SelectBlobList() Contains level1\\", listBlobs_2, Blobs_List_L1);
            Break();

            // list level2
            var listBlobs_3 = BlobClient.SelectBlobList(Profile, "level1/level2/");
            var listDoucmentCount_3 = listBlobs_3.Count;
            Assert("SelectBlobList() Count level1\\level2\\", listDoucmentCount_3, 2);
            Assert("SelectBlobList() Contains level1\\level2\\", listBlobs_3, Blobs_List_L2);
            Break();

            // list level2-2
            var listBlobs_4 = BlobClient.SelectBlobList(Profile, "level1/level2-2/");
            var listDoucmentCount_4 = listBlobs_4.Count;
            Assert("SelectBlobList() Count level1\\level2-2\\", listDoucmentCount_4, 1);
            Assert("SelectBlobList() Contains level1\\level2-2\\", listBlobs_4, Blob_L2_2_2);
            Break();

            // list level2-3
            var listBlobs_5 = BlobClient.SelectBlobList(Profile, "level1/level2-3/");
            var listDoucmentCount_5 = listBlobs_5.Count;
            Assert("SelectBlobList() Count level1\\level2-3\\", listDoucmentCount_5, 1);
            Assert("SelectBlobList() Contains level1\\level2-3\\", listBlobs_5, Blob_L2_2_3);
            Break();

            // list root prefixes
            var listPrefix_1 = BlobClient.SelectPrefixList(Profile);
            var listPrefixCount_1 = listPrefix_1.Count;
            Assert("SelectPrefixList() Count root", listPrefixCount_1, 1);
            Assert("SelectPrefixList() Contains root", listPrefix_1, Prefix_L1);
            Break();

            // list level1 prefixes
            var listPrefix_2 = BlobClient.SelectPrefixList(Profile, "level1/");
            var listPrefixCount_2 = listPrefix_2.Count;
            Assert("SelectPrefixList() Count level1\\", listPrefixCount_2, 3);
            Assert("SelectPrefixList() Contains level1\\", listPrefix_2, Prefix_List_L1);

            ManualCheck("Verify List Operations");

            // ---
            // SELECT
            // ---

            TestHeader("SelectBlob()");

            // select root
            var testSelect_1 = BlobClient.SelectBlob(Profile, Blob_Core).GetAwaiter().GetResult();
            var testString_1 = BlobClient.ByteToString(testSelect_1);
            Assert("SelectBlob() root", testSelect_1, Blob_Byte_1);
            Assert("ByteToString() root", testString_1, Blob_Body_1);
            Break();

            // select level1
            var testSelect_2 = BlobClient.SelectBlob(Profile, Blob_L1_1).GetAwaiter().GetResult();
            var testString_2 = BlobClient.ByteToString(testSelect_2);
            Assert("SelectBlob() level1\\", testSelect_2, Blob_Byte_1);
            Assert("ByteToString() level1\\", testString_2, Blob_Body_1);
            Break();

            // select level2
            var testSelect_3 = BlobClient.SelectBlob(Profile, Blob_L2_1).GetAwaiter().GetResult();
            var testString_3 = BlobClient.ByteToString(testSelect_3);
            Assert("SelectBlob() level\\level2\\", testSelect_3, Blob_Byte_1);
            Assert("ByteToString() level1\\level2\\", testString_3, Blob_Body_1);

            ManualCheck("Verify Select Operations");

            // ---
            // UPDATE
            // ---

            TestHeader("UpdateBlob()");

            // update root
            var update_select_1 = BlobClient.SelectBlob(Profile, Blob_Update_1).GetAwaiter().GetResult();
            var update_update_1 = BlobClient.UpdateBlob(Profile, Blob_Update_1, Blob_Byte_2).GetAwaiter().GetResult();
            var update_verify_1 = BlobClient.SelectBlob(Profile, Blob_Update_1).GetAwaiter().GetResult();
            Assert("SelectBlob() Baseline root", update_select_1, Blob_Byte_1);
            Assert("UpdateBlob() root", update_update_1, true);
            Assert("SelectBlob() Verify root", update_verify_1, Blob_Byte_2);
            Break();

            // update level1
            var update_select_2 = BlobClient.SelectBlob(Profile, Blob_Update_2).GetAwaiter().GetResult();
            var update_update_2 = BlobClient.UpdateBlob(Profile, Blob_Update_2, Blob_Byte_2).GetAwaiter().GetResult();
            var update_verify_2 = BlobClient.SelectBlob(Profile, Blob_Update_2).GetAwaiter().GetResult();
            Assert("SelectBlob() Baseline level1\\", update_select_2, Blob_Byte_1);
            Assert("UpdateBlob() level1\\", update_update_2, true);
            Assert("SelectBlob() Verify level1\\", update_verify_2, Blob_Byte_2);
            Break();

            // update level2
            var update_select_3 = BlobClient.SelectBlob(Profile, Blob_Update_3).GetAwaiter().GetResult();
            var update_update_3 = BlobClient.UpdateBlob(Profile, Blob_Update_3, Blob_Byte_2).GetAwaiter().GetResult();
            var update_verify_3 = BlobClient.SelectBlob(Profile, Blob_Update_3).GetAwaiter().GetResult();
            Assert("SelectBlob() Baseline level1\\level2\\", update_select_3, Blob_Byte_1);
            Assert("UpdateBlob() level1\\level2\\", update_update_3, true);
            Assert("SelectBlob() Verify level1\\level2\\", update_verify_3, Blob_Byte_2);

            ManualCheck("Verify UpdateBlob Operations");

            // ---
            // RENAME
            // ---

            TestHeader("RenameBlobInContainer()");

            // rename root
            var rename_select_1 = BlobClient.SelectBlob(Profile, Blob_Rename_1_1).GetAwaiter().GetResult();
            var rename_rename_1 = BlobClient.RenameBlobInContainer(Profile, Blob_Rename_1_1, Blob_Rename_1_2).GetAwaiter().GetResult();
            var rename_verify_before_1 = BlobClient.SelectBlob(Profile, Blob_Rename_1_1).GetAwaiter().GetResult();
            var rename_verify_after_1 = BlobClient.SelectBlob(Profile, Blob_Rename_1_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck root", rename_select_1, Blob_Byte_1);
            Assert("RenameBlobInContainer() root", rename_rename_1, true);
            Assert("SelectBlob() Baseline root", rename_verify_before_1, (object)null);
            Assert("SelectBlob() Verify root", rename_select_1, rename_verify_after_1);
            Break();

            // rename level1
            var rename_select_2 = BlobClient.SelectBlob(Profile, Blob_Rename_2_1).GetAwaiter().GetResult();
            var rename_rename_2 = BlobClient.RenameBlobInContainer(Profile, Blob_Rename_2_1, Blob_Rename_2_2).GetAwaiter().GetResult();
            var rename_verify_before_2 = BlobClient.SelectBlob(Profile, Blob_Rename_2_1).GetAwaiter().GetResult();
            var rename_verify_after_2 = BlobClient.SelectBlob(Profile, Blob_Rename_2_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck level1\\", rename_select_2, Blob_Byte_2);
            Assert("RenameBlobInContainer() level1\\", rename_rename_2, true);
            Assert("SelectBlob() Baseline level1\\", rename_verify_before_2, (object)null);
            Assert("SelectBlob() Verify level1\\", rename_select_2, rename_verify_after_2);
            Break();

            // rename level2
            var rename_select_3 = BlobClient.SelectBlob(Profile, Blob_Rename_3_1).GetAwaiter().GetResult();
            var rename_rename_3 = BlobClient.RenameBlobInContainer(Profile, Blob_Rename_3_1, Blob_Rename_3_2).GetAwaiter().GetResult();
            var rename_verify_before_3 = BlobClient.SelectBlob(Profile, Blob_Rename_3_1).GetAwaiter().GetResult();
            var rename_verify_after_3 = BlobClient.SelectBlob(Profile, Blob_Rename_3_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck level1\\level2\\", rename_select_3, Blob_Byte_2);
            Assert("RenameBlobInContainer() level1\\level2\\", rename_rename_3, true);
            Assert("SelectBlob() Baseline level1\\level2\\", rename_verify_before_3, (object)null);
            Assert("SelectBlob() Verify level1\\level2\\", rename_select_3, rename_verify_after_3);

            ManualCheck("Verify RenameBlobInContainer Operations");

            // ---
            // COPY (deleteSource: false)
            // ---

            TestHeader("CopyBlobInContainer()");

            // copy root
            var copy_select_1 = BlobClient.SelectBlob(Profile, Blob_Copy_1_1).GetAwaiter().GetResult();
            var copy_execute_1 = BlobClient.CopyBlobInContainer(Profile, Blob_Copy_1_1, Blob_Copy_1_2).GetAwaiter().GetResult();
            var copy_verify_original_1 = BlobClient.SelectBlob(Profile, Blob_Copy_1_1).GetAwaiter().GetResult();
            var copy_verify_copy_1 = BlobClient.SelectBlob(Profile, Blob_Copy_1_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck root", copy_select_1, Blob_Byte_1);
            Assert("CopyBlobInContainer() root", copy_execute_1, true);
            Assert("SelectBlob() Baseline root", copy_select_1, copy_verify_original_1);
            Assert("SelectBlob() Verify root", copy_verify_original_1, copy_verify_copy_1);
            Break();

            // copy level1
            var copy_select_2 = BlobClient.SelectBlob(Profile, Blob_Copy_2_1).GetAwaiter().GetResult();
            var copy_execute_2 = BlobClient.CopyBlobInContainer(Profile, Blob_Copy_2_1, Blob_Copy_2_2).GetAwaiter().GetResult();
            var copy_verify_original_2 = BlobClient.SelectBlob(Profile, Blob_Copy_2_1).GetAwaiter().GetResult();
            var copy_verify_copy_2 = BlobClient.SelectBlob(Profile, Blob_Copy_2_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck level1\\", copy_select_2, Blob_Byte_2);
            Assert("CopyBlobInContainer() level1\\", copy_execute_2, true);
            Assert("SelectBlob() Baseline level1\\", copy_select_2, copy_verify_original_2);
            Assert("SelectBlob() Verify level1\\", copy_verify_original_2, copy_verify_copy_2);
            Break();

            // copy level2
            var copy_select_3 = BlobClient.SelectBlob(Profile, Blob_Copy_3_1).GetAwaiter().GetResult();
            var copy_execute_3 = BlobClient.CopyBlobInContainer(Profile, Blob_Copy_3_1, Blob_Copy_3_2).GetAwaiter().GetResult();
            var copy_verify_original_3 = BlobClient.SelectBlob(Profile, Blob_Copy_3_1).GetAwaiter().GetResult();
            var copy_verify_copy_3 = BlobClient.SelectBlob(Profile, Blob_Copy_3_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck level1\\level2\\", copy_select_3, Blob_Byte_2);
            Assert("CopyBlobInContainer() level1\\level2\\", copy_execute_3, true);
            Assert("SelectBlob() Baseline level1\\level2\\", copy_select_3, copy_verify_original_3);
            Assert("SelectBlob() Verify level1\\level2\\", copy_verify_original_3, copy_verify_copy_3);

            ManualCheck("Verify Copy Operations");

            // ---
            // COPY (deleteSource: true)
            // ---

            TestHeader("CopyBlobInContainer(deleteSource: true)");

            // copy with delete root
            var copydel_select_1 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_1_1).GetAwaiter().GetResult();
            var copydel_execute_1 = BlobClient.CopyBlobInContainer(Profile, Blob_CopyDelete_1_1, Blob_CopyDelete_1_2, deleteSource: true).GetAwaiter().GetResult();
            var copydel_verify_original_1 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_1_1).GetAwaiter().GetResult();
            var copydel_verify_copy_1 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_1_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck root", copydel_select_1, Blob_Byte_1);
            Assert("CopyBlobInContainer(deleteSource: true) root", copydel_execute_1, true);
            Assert("SelectBlob() Baseline root", copydel_verify_original_1, (object)null);
            Assert("SelectBlob() Verify root", copydel_verify_copy_1, Blob_Byte_1);
            Break();

            // copy with delete level1
            var copydel_select_2 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_2_1).GetAwaiter().GetResult();
            var copydel_execute_2 = BlobClient.CopyBlobInContainer(Profile, Blob_CopyDelete_2_1, Blob_CopyDelete_2_2, deleteSource: true).GetAwaiter().GetResult();
            var copydel_verify_original_2 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_2_1).GetAwaiter().GetResult();
            var copydel_verify_copy_2 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_2_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck level1\\", copydel_select_2, Blob_Byte_2);
            Assert("CopyBlobInContainer(deleteSource: true) level1\\", copydel_execute_2, true);
            Assert("SelectBlob() Baseline level1\\", copydel_verify_original_2, (object)null);
            Assert("SelectBlob() Verify level1\\", copydel_verify_copy_2, Blob_Byte_2);
            Break();

            // copy with delete level2
            var copydel_select_3 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_3_1).GetAwaiter().GetResult();
            var copydel_execute_3 = BlobClient.CopyBlobInContainer(Profile, Blob_CopyDelete_3_1, Blob_CopyDelete_3_2, deleteSource: true).GetAwaiter().GetResult();
            var copydel_verify_original_3 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_3_1).GetAwaiter().GetResult();
            var copydel_verify_copy_3 = BlobClient.SelectBlob(Profile, Blob_CopyDelete_3_2).GetAwaiter().GetResult();
            Assert("SelectBlob() PreCheck level1\\level2\\", copydel_select_3, Blob_Byte_2);
            Assert("CopyBlobInContainer(deleteSource: true) level1\\level2\\", copydel_execute_3, true);
            Assert("SelectBlob() Baseline level1\\level2\\", copydel_verify_original_3, (object)null);
            Assert("SelectBlob() Verify level1\\level2\\", copydel_verify_copy_3, Blob_Byte_2);

            ManualCheck("Verify Copy (with delete) Operations");

            // --- 
            // DELETE
            // ---

            TestHeader("DeleteBlob()");

            // delete root
            var testDelete_1 = BlobClient.DeleteBlob(Profile, Blob_Core).GetAwaiter().GetResult();
            var testConfirm_1 = BlobClient.SelectBlob(Profile, Blob_Core).GetAwaiter().GetResult();
            Assert("DeleteBlob() root", testDelete_1, true);
            Assert("SelectBlob() root", testConfirm_1, (object)null);
            Break();

            // delete level1
            var testDelete_2 = BlobClient.DeleteBlob(Profile, Blob_L1_1).GetAwaiter().GetResult();
            var testConfirm_2 = BlobClient.SelectBlob(Profile, Blob_L1_1).GetAwaiter().GetResult();
            Assert("DeleteBlob() level1\\", testDelete_2, true);
            Assert("SelectBlob() level1\\", testConfirm_2, (object)null);
            Break();

            // delete level2
            var testDelete_3 = BlobClient.DeleteBlob(Profile, Blob_L2_1).GetAwaiter().GetResult();
            var testConfirm_3 = BlobClient.SelectBlob(Profile, Blob_L2_1).GetAwaiter().GetResult();
            Assert("DeleteBlob() level1\\level2\\", testDelete_3, true);
            Assert("SelectBlob() level1\\level2\\", testConfirm_3, (object)null);

            ManualCheck("Verify Delete Operations");

            // --- 
            // CLEAN-UP
            // ---

            TestHeader("DeleteContainer()");

            // delete container
            var testDeleteContainer_1 = BlobClient.DeleteContainer(Profile).GetAwaiter().GetResult();
            Assert("DeleteContainer()", testDeleteContainer_1, true);

            ManualCheck("Confirm the Container has been deleted");
        }

        // ---
        // TEST UTILITY
        // ---

        private static void Assert(string eventName, object objectA, object objectB)
        {
            var result = Equals(objectA, objectB);

            var resultMessage = result == true ? "PASS" : "FAIL";

            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"\t<!> Assert: {eventName} - {resultMessage}");
            Console.ResetColor();
        }

        private static void Assert(string eventName, byte[] arrayA, byte[] arrayB)
        {
            bool result;

            try
            {
                result = Enumerable.SequenceEqual(arrayA, arrayB);
            }
            catch (ArgumentNullException)
            {
                result = false;
            }

            var resultMessage = result == true ? "PASS" : "FAIL";

            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"\t<!> Assert: {eventName} - {resultMessage}");
            Console.ResetColor();
        }

        private static void Assert(string eventName, List<string> inputA, List<string> inputB)
        {
            bool result = true;

            foreach (var item in inputA)
            {
                if (!inputB.Contains(item))
                {
                    result = false;
                }
            }

            var resultMessage = result == true ? "PASS" : "FAIL";

            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"\t<!> Assert: {eventName} - {resultMessage}");
            Console.ResetColor();
        }

        private static void Assert(string eventName, List<string> inputA, string inputB)
        {
            bool result = false;

            foreach (var item in inputA)
            {
                if (Equals(item.ToUpper(), inputB.ToUpper()))
                {
                    result = true;
                }
            }

            var resultMessage = result == true ? "PASS" : "FAIL";

            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"\t<!> Assert: {eventName} - {resultMessage}");
            Console.ResetColor();
        }

        private static void ManualCheck(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\t<?> Manual Check: {message} \r\n");
            Console.WriteLine($"\t*** PRESS ANY KEY TO CONTINUE *** \r\n");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void TestHeader(string test)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"<^> TEST: {test}\n\r");
            Console.ResetColor();
        }

        private static void Break()
        {
            Console.WriteLine($"\r");
        }

        // ---
        // DATA UTILITY
        // ---

        private static bool SetBlobFileNames()
        {
            Blob_Core = @"test.txt";

            Blob_L1_1 = @"level1\test.txt";
            Blob_L1_2 = @"level1\test2.txt";

            Blob_L2_1 = @"level1\level2\test.txt";
            Blob_L2_2 = @"level1\level2\test2.txt";

            Blob_L2_2_2 = @"level1\level2-2\test.txt";
            Blob_L2_2_3 = @"level1\level2-3\test.txt";

            Blob_Update_1 = @"testupdate.txt";
            Blob_Update_2 = @"level1\test2.txt";
            Blob_Update_3 = @"level1\level2\test2.txt";

            Blob_Rename_1_1 = @"testrename1.txt";
            Blob_Rename_1_2 = @"testrename2.txt";
            Blob_Rename_2_1 = @"level1\test2.txt";
            Blob_Rename_2_2 = @"level1\testrename2.txt";
            Blob_Rename_3_1 = @"level1\level2\test2.txt";
            Blob_Rename_3_2 = @"level1\level2\testrename2.txt";

            Blob_Copy_1_1 = @"testcopy1.txt";
            Blob_Copy_1_2 = @"testcopy2.txt";
            Blob_Copy_2_1 = @"level1\testrename2.txt";
            Blob_Copy_2_2 = @"level1\testcopy2.txt";
            Blob_Copy_3_1 = @"level1\level2\testrename2.txt";
            Blob_Copy_3_2 = @"level1\level2\testcopy2.txt";

            Blob_CopyDelete_1_1 = @"testcopydelete1.txt";
            Blob_CopyDelete_1_2 = @"testcopydelete2.txt";
            Blob_CopyDelete_2_1 = @"level1\testcopy2.txt";
            Blob_CopyDelete_2_2 = @"level1\testcopydelete2.txt";
            Blob_CopyDelete_3_1 = @"level1\level2\testcopy2.txt";
            Blob_CopyDelete_3_2 = @"level1\level2\testcopydelete2.txt";

            Prefix_L1 = @"level1\";
            Prefix_L2_1 = @"level1\level2\";
            Prefix_L2_2 = @"level1\level2-2\";
            Prefix_L2_3 = @"level1\level2-3\";

            Blobs_List_L0 = new List<string> 
            { 
                Blob_Core,
                Blob_Update_1, 
                Blob_Rename_1_1, 
                Blob_Copy_1_1, 
                Blob_CopyDelete_1_1 
            };

            Blobs_List_L1 = new List<string>
            {
                Blob_L1_1,
                Blob_L1_2
            };

            Blobs_List_L2 = new List<string>
            {
                Blob_L2_1,
                Blob_L2_2
            };

            Prefix_List_L1 = new List<string>
            {
                Prefix_L2_1,
                Prefix_L2_2,
                Prefix_L2_3
            };
            
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

        private static bool BulkInsertTestBlobs()
        {
            if (!BlobClient.InsertBlob(Profile, Blob_Update_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_Rename_1_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_Copy_1_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_CopyDelete_1_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_L1_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_L1_2, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_L2_1, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_L2_2, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_L2_2_2, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            if (!BlobClient.InsertBlob(Profile, Blob_L2_2_3, Blob_Byte_1).GetAwaiter().GetResult())
            {
                return false;
            }

            return true;
        }
    }
}
