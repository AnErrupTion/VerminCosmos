using Cosmos.Core.IOGroup;
using Cosmos.Core;
using System.Drawing;
using Cosmos.HAL;

namespace VBETest
{
    public enum VBERegisters
    {
        VBE_DISPI_INDEX_ID = 0,
        VBE_DISPI_INDEX_XRES,
        VBE_DISPI_INDEX_YRES,
        VBE_DISPI_INDEX_BPP,
        VBE_DISPI_INDEX_ENABLE,
        VBE_DISPI_INDEX_BANK,
        VBE_DISPI_INDEX_VIRT_WIDTH,
        VBE_DISPI_INDEX_VIRT_HEIGHT,
        VBE_DISPI_INDEX_X_OFFSET,
        VBE_DISPI_INDEX_Y_OFFSET,

        VBE_DISPI_BPP_4 = 0x04,
        VBE_DISPI_BPP_8 = 0x08,
        VBE_DISPI_BPP_15 = 0x0F,
        VBE_DISPI_BPP_16 = 0x10,
        VBE_DISPI_BPP_24 = 0x18,
        VBE_DISPI_BPP_32 = 0x20,

        VBE_DISPI_DISABLED = 0x00,
        VBE_DISPI_ENABLED = 0x01,
        VBE_DISPI_LFB_ENABLED = 0x40
    }

    public class VBEDriver
    {
        public static VBEIOGroup IO = Cosmos.Core.Global.BaseIOGroups.VBE;
        public static int Width, Height, Depth;

        private static void Write(VBERegisters index, ushort value)
        {
            IO.VbeIndex.Word = (ushort) index;
            IO.VbeData.Word = value;
        }

        public static void Initialize(int width, int height, VBERegisters depth)
        {
            var videocard = Cosmos.HAL.PCI.GetDevice(VendorID.VirtualBox, DeviceID.VBVGA);
            IO.LinearFrameBuffer = new MemoryBlock(videocard.BAR0, (uint) (Width * Height * Depth));

            Disable();

            Width = width;
            Height = height;
            Depth = 24;

            // Set display width, height and color depth
            Write(VBERegisters.VBE_DISPI_INDEX_XRES, (ushort) width);
            Write(VBERegisters.VBE_DISPI_INDEX_YRES, (ushort) height);
            Write(VBERegisters.VBE_DISPI_INDEX_BPP, (ushort) depth);

            // Enable display and the linear frame buffer
            Write(VBERegisters.VBE_DISPI_INDEX_ENABLE, (ushort) (VBERegisters.VBE_DISPI_ENABLED | VBERegisters.VBE_DISPI_LFB_ENABLED));
        }

        public static void Disable()
        {
            // Disable display
            Write(VBERegisters.VBE_DISPI_INDEX_ENABLE, (ushort) VBERegisters.VBE_DISPI_DISABLED);
        }

        public static void SetVRAM(byte[] value)
        {
            for (uint i = 0; i < value.Length; i++)
                IO.LinearFrameBuffer[i] = value[i];
        }

        public static void SetVRAM(uint index, byte value)
        {
            IO.LinearFrameBuffer[index] = value;
        }

        public static byte GetVRAM(uint index)
        {
            return (byte) IO.LinearFrameBuffer[index];
        }

        public static void Clear(Color c)
        {
            for (uint x = 0; x < Width; x++)
                for (uint y = 0; y < Height; y++)
                    SetPixel(x, y, c);
        }

        public static void SetPixel(uint x, uint y, Color c)
        {
            var location = x * ((uint)Depth / 8) + y * (uint)(Width * ((uint)Depth / 8));

            SetVRAM(location, c.B);
            SetVRAM(location + 1, c.G);
            SetVRAM(location + 2, c.R);
        }

        public Color GetPixel(uint x, uint y)
        {
            var location = x * ((uint)Depth / 8) + y * (uint)(Width * ((uint)Depth / 8));

            return Color.FromArgb(GetVRAM(location + 2), GetVRAM(location + 1), GetVRAM(location));
        }
    }
}
