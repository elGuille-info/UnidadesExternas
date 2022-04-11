//--------------------------------------------------------------------------------
// USBUtil                                                      (11/abr/22 16.32)
// Utilidades para saber las unidades externas (extraíbles o fijas)
//
// Versión para .NET 5.0
//
// (c) Guillermo Som (Guille), 2022
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
// Hay que instalar System.Management de NuGet. versión 5.0 poara .NET Framework o .NET 5.0 o inferior.
using System.Management;


namespace UnidadesExternas_cs
{
    public class USBUtil
    {
        /// <summary>
        /// Comprueba si es una unidad externa.
        /// </summary>
        /// <param name="elPath">Path completo del que se extraerá la letra de unidad.</param>
        /// <returns></returns>
        public static bool EsUnidadExterna(string elPath)
        {
            var disco = Path.GetPathRoot(Path.GetFullPath(elPath));
            var usbD = GetUsbDriveLetters();
            return usbD.Contains(disco);
        }

        // Código basado en dos ejemplos de "la red".
        // https://stackoverflow.com/a/31560283/14338047
        // https://stackoverflow.com/a/10018438/14338047

        /// <summary>
        /// Las letras de las unidades externas conectadas por USB.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUsbDriveLetters()
        {
            // Hay que usar External para que también tenga en cuenta los USB de tipo fijo.
            var usbDrivesLetters = from drive in new ManagementObjectSearcher("select * from Win32_DiskDrive WHERE MediaType like '%External%' OR InterfaceType='USB'").Get().Cast<ManagementObject>()
                                   from o in drive.GetRelated("Win32_DiskPartition").Cast<ManagementObject>()
                                   from i in o.GetRelated("Win32_LogicalDisk").Cast<ManagementObject>()
                                   select string.Format("{0}\\", i["Name"]);

            return usbDrivesLetters.ToList();
        }

    }
}
