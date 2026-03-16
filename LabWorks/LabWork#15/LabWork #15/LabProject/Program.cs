namespace LabProject;

public class Program
{
    static Test globalRef;
    static void CreateObject()
    {
        Test t = new();
        globalRef = t;
    }

    static void ModifyClass(PointClass point)
    {
        point.X = 6;
    }

    static void ModifyStruct(ref PointStruct point)
    {
        point.X = 6;
    }

    unsafe static DataStruct UncodePacket(byte[] packet)
    {
        DataStruct data = new();
        fixed (byte* ptr = packet)
        {
            ushort* idPtr = (ushort*)ptr;
            data.ID = *idPtr;

            int* timestampPtr = (int*)((byte*)idPtr + sizeof(ushort));
            data.Timestamp = *timestampPtr;

            float* temperaturePtr = (float*)((byte*)timestampPtr + sizeof(int));
            data.Temperature = *temperaturePtr;

            byte* statusPtr = (byte*)temperaturePtr + sizeof(float);
            data.Status = *statusPtr;

            int* checksumPtr = (int*)(statusPtr + sizeof(byte));
            data.Checksum = *checksumPtr;
        }
        return data;
    }

    static void Main(string[] args)
    {
        
        #region Task 1
        CreateObject();
        GC.Collect();

        Console.WriteLine(GC.GetGeneration(globalRef));
        #endregion
        

        Console.WriteLine("\n-----\n");

        #region Task 2
        PointClass pointClass = new()
        {
            X = 5,
            Y = 10
        };

        PointStruct pointStruct = new() 
        { 
            X = 5,
            Y = 10
        };

        ModifyClass(pointClass);
        ModifyStruct(ref pointStruct);

        Console.WriteLine($"Class: {pointClass.X}");
        Console.WriteLine($"Struct: {pointStruct.X}");
        #endregion

        Console.WriteLine("\n-----\n");

        #region Task 3
        var seventyThousandSizeByteArray = new byte[70000];
        var ninetyThousandSizeByteArray = new byte[90000];
        var oneHundredThousandSizeByteArray = new byte[100000];

        Console.WriteLine($"70000: {GC.GetGeneration(seventyThousandSizeByteArray)}");
        Console.WriteLine($"90000: {GC.GetGeneration(ninetyThousandSizeByteArray)}");
        Console.WriteLine($"100000: {GC.GetGeneration(oneHundredThousandSizeByteArray)}");
        #endregion

        Console.WriteLine("\n-----\n");
        
        #region Task 4
        using FileLogger loggerWithUsing = new("using");
        loggerWithUsing.Log();
        Console.WriteLine("Конец using лога");

        FileLogger loggerWithoutUsing = new("without using");
        loggerWithoutUsing.Log();
        Console.WriteLine("Конец просто лога");
        #endregion

        Console.WriteLine("\n-----\n");

        #region Task 5
        byte[] packet =
        {
        0x01, 0x00, // ushort ID = 1
        0x10, 0x27, 0x00, 0x00, // int timestamp = 10000
        0x00, 0x00, 0x48, 0x42, // float temperature = 50.0
        0x01, // byte status
        0x00, 0x00, 0x00, 0x00 // int checksum
        };

        var checkSum = 0;
        for (int i = 0; i < 11; i++) 
            checkSum += packet[i];

        var data = UncodePacket(packet);
        Console.WriteLine($"Сумма данных = {checkSum}\tСумма внутри пакета = {data.Checksum}");
        Console.WriteLine($"ID: {data.ID}\nTimestamp: {data.Timestamp}\n" +
            $"Temperature: {data.Temperature}\nStatus: {data.Status}");
        #endregion

    }
}
