'--------------------------------------------------------------------------------
' USBUtil                                                       (11/abr/22 16.32)
' Versión para Visual Basic.                                    (11/abr/22 20.42)
' Utilidades para saber las unidades externas (extraíbles o fijas)
'
' Versión para .NET 5.0
'
' (c) Guillermo Som (Guille), 2022
'--------------------------------------------------------------------------------
Option Strict On
Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
' Hay que instalar System.Management de NuGet. versión 5.0 poara .NET Framework o .NET 5.0 o inferior.
Imports System.Management

Public Class USBUtil

    ''' <summary>
    ''' Comprueba si es una unidad externa.
    ''' </summary>
    ''' <param name="elPath">Path completo del que se extraerá la letra de unidad.</param>
    ''' <returns></returns>
    Public Shared Function EsUnidadExterna(ByVal elPath As String) As Boolean
        Dim disco = Path.GetPathRoot(Path.GetFullPath(elPath))
        Dim usbD = GetUsbDriveLetters()
        Return usbD.Contains(disco)
    End Function

    ''' <summary>
    ''' Las letras de las unidades externas conectadas por USB.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetUsbDriveLetters() As List(Of String)
        ' Hay que usar External para que también tenga en cuenta los USB de tipo fijo.
        Dim usbDrivesLetters = From drive In New ManagementObjectSearcher("select * from Win32_DiskDrive WHERE MediaType like '%External%' OR InterfaceType='USB'").[Get]().Cast(Of ManagementObject)()
                               From o In drive.GetRelated("Win32_DiskPartition").Cast(Of ManagementObject)()
                               From i In o.GetRelated("Win32_LogicalDisk").Cast(Of ManagementObject)()
                               Select String.Format("{0}\", i("Name"))
        Return usbDrivesLetters.ToList()
    End Function

End Class