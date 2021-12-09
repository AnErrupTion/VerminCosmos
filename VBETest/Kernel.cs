using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System.Text;

namespace VBETest
{
    public class Kernel : Sys.Kernel
    {
        private CosmosVFS fs = new CosmosVFS();

        protected override void BeforeRun()
        {
            //VBEDriver.Initialize(800, 600, VBERegisters.VBE_DISPI_BPP_24
            VFSManager.RegisterVFS(fs);
        }

        protected override void Run()
        {
            try
            {
                /*var directory_list = fs.GetDirectoryListing(@"0:\");
                foreach (var directoryEntry in directory_list)
                {
                    var file_stream = directoryEntry.GetFileStream();
                    if (file_stream.CanRead)
                    {
                        var entry_type = directoryEntry.mEntryType;
                        if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                        {
                            byte[] content = new byte[file_stream.Length];
                            file_stream.Read(content, 0, content.Length);
                            Console.WriteLine("File name: " + directoryEntry.mName);
                            Console.WriteLine("File size: " + directoryEntry.mSize);
                            Console.WriteLine("Content: ");
                            foreach (var ch in content)
                            {
                                Console.Write(ch.ToString());
                            }
                            Console.WriteLine();
                        }
                    }
                }*/

                Console.WriteLine("Writing!");
                var filec = fs.CreateFile("0:\\testing.txt");
                var filec_stream = filec.GetFileStream();

                if (filec_stream.CanWrite)
                {
                    byte[] write_buffer = Encoding.ASCII.GetBytes("Hey!");
                    filec_stream.Write(write_buffer, 0, write_buffer.Length);
                }

                Console.WriteLine("Reading!");
                var file = fs.GetFile(@"0:\testing.txt");
                var file_stream = file.GetFileStream();

                if (file_stream.CanRead)
                {
                    byte[] file_buffer = new byte[file_stream.Length];
                    file_stream.Read(file_buffer, 0, file_buffer.Length);
                    foreach (var ch in file_buffer)
                    {
                        Console.Write(ch.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //VBEDriver.Clear(Color.Blue);
        }
    }
}
