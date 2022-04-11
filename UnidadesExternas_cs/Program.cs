//--------------------------------------------------------------------------------
// UnidadesExternas                                             (11/abr/22 20.26)
// Leer las letras de unidades externas, fijas o extraíbles.
//
// (c) Guillermo Som (Guille), 2022
//--------------------------------------------------------------------------------

using System;
using System.IO;

namespace UnidadesExternas_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ejemplo de Visual C# usando .NET 5.0 (net5.0-windows) y System.Management Version=5.0.0";

            Console.WriteLine("Mostrar las unidades usando DriveInfo.GetDrives().");
            foreach (var dr in DriveInfo.GetDrives())
                Console.WriteLine("Unidad: {0}, tipo: {1}", dr.Name, dr.DriveType);

            Console.WriteLine();

            Console.WriteLine("Mostrar las unidades extraíbles.");
            var usb = USBUtil.GetUsbDriveLetters();
            foreach (var s in usb)
                Console.WriteLine("{0}", s);

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
