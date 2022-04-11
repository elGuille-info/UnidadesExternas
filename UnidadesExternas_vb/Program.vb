'--------------------------------------------------------------------------------
' UnidadesExternas                                              (11/abr/22 20.39)
' Leer las letras de unidades externas, fijas o extraíbles.
'
' (c) Guillermo Som (Guille), 2022
'--------------------------------------------------------------------------------
Option Strict On
Option Infer On

Imports System
Imports System.IO

Module Program
    Sub Main(args As String())
        Console.Title = "Ejemplo de Visual Basic usando .NET 5.0 (net5.0-windows) y System.Management Version=5.0.0"

        Console.WriteLine("Mostrar las unidades usando DriveInfo.GetDrives().")
        For Each dr In DriveInfo.GetDrives()
            Console.WriteLine("Unidad: {0}, tipo: {1}", dr.Name, dr.DriveType)
        Next
        Console.WriteLine()

        Console.WriteLine("Mostrar las unidades extraíbles.")
        Dim usb = USBUtil.GetUsbDriveLetters()

        For Each s In usb
            Console.WriteLine("{0}", s)
        Next

        Console.WriteLine()
        Console.ReadLine()
    End Sub
End Module
