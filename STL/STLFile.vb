Imports System.IO
''' <summary>
''' 用于表示STL模型文件
''' </summary>
Public Class STLFile
    ''' <summary>
    ''' 包含这个STL模型文件中所有的体
    ''' </summary>
    Public Solids As List(Of Solid)
    ''' <summary>
    ''' 创建新的空白STL模型
    ''' </summary>
    Public Sub New()
        Solids = New List(Of Solid)
    End Sub
    ''' <summary>
    ''' 从现有文件读取STL模型，自动识别编码方式
    ''' </summary>
    ''' <param name="Path">STL文件的路径</param>
    Public Sub New(Path As String)
        Solids = New List(Of Solid)
        Dim reader As New StreamReader(Path)
        Dim s As String = reader.ReadLine
        Dim ss As String()
        Dim temp_face As New Face
        Dim temp_solid As New Solid
        Dim vertex_count As Integer = 0
        Dim sss As String
        If Mid(s, 1, 5) = "solid" Then
            reader.Close()
            reader = New StreamReader(Path)
            Do Until reader.EndOfStream
                sss = reader.ReadLine
                s = LTrim(sss)
                ss = Split(s)
                Select Case ss(0)
                    Case "solid"
                        temp_solid = New Solid With {.Title = ss(1)}
                        Exit Select
                    Case "facet"
                        temp_face = New Face
                        Exit Select
                    Case "vertex"
                        temp_face.Vertexes(vertex_count) = New Vertex(Val(ss(1)), Val(ss(2)), Val(ss(3)))
                        vertex_count = (vertex_count + 1) Mod 3
                        Exit Select
                    Case "endfacet"
                        temp_solid.Faces.Add(temp_face)
                        Exit Select
                    Case "endsolid"
                        Solids.Add(temp_solid)
                        Exit Select
                End Select
            Loop
            reader.Close()
            If Solids.Count = 0 Then
                '达索solidworks拉的屎
                'STL开头5个字用于识别编码方式（二进制模式不能以solid开头）
                '但solidworks不遵循这个规则
                '因此在这里被迫添加这一段冗余代码
                Dim fs As New FileStream(Path, FileMode.Open)
                Dim reader2 As New BinaryReader(fs)
                Dim face_count As UInteger
                Do Until fs.Position + 80 > fs.Length
                    temp_solid = New Solid With {.Title = reader2.ReadChars(79)}
                    face_count = reader2.ReadUInt32
                    For i As UInteger = 1 To face_count
                        fs.Position += 12
                        temp_face = New Face
                        temp_face.Vertexes(0) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                        temp_face.Vertexes(1) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                        temp_face.Vertexes(2) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                        temp_solid.Faces.Add(temp_face)
                        fs.Position += 2
                    Next
                    Solids.Add(temp_solid)
                Loop
                reader2.Close()
            End If
        Else
            reader.Close()
            Dim fs As New FileStream(Path, FileMode.Open)
            Dim reader2 As New BinaryReader(fs)
            Dim face_count As UInteger
            Do Until fs.Position + 80 > fs.Length
                temp_solid = New Solid With {.Title = reader2.ReadChars(79)}
                face_count = reader2.ReadUInt32
                For i As UInteger = 1 To face_count
                    fs.Position += 12
                    temp_face = New Face
                    temp_face.Vertexes(0) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                    temp_face.Vertexes(1) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                    temp_face.Vertexes(2) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                    temp_solid.Faces.Add(temp_face)
                    fs.Position += 2
                Next
                Solids.Add(temp_solid)
            Loop
            reader2.Close()
        End If
    End Sub
    ''' <summary>
    ''' 通过指定的编码模式从现有文件读取STL模型
    ''' </summary>
    ''' <param name="Path">STL文件的路径</param>
    ''' <param name="EncodeMode">编码方式</param>
    Public Sub New(Path As String, EncodeMode As EncodeMode)
        Solids = New List(Of Solid)
        Dim reader As New StreamReader(Path)
        Dim s As String
        Dim ss As String()
        Dim temp_face As New Face
        Dim temp_solid As New Solid
        Dim vertex_count As Integer = 0
        If EncodeMode = EncodeMode.Binary Then
            reader.Close()
            Dim fs As New FileStream(Path, FileMode.Open)
            Dim reader2 As New BinaryReader(fs)
            Dim face_count As UInteger
            Do Until fs.Position + 80 > fs.Length
                temp_solid = New Solid With {.Title = reader2.ReadChars(79)}
                face_count = reader2.ReadUInt32
                For i As UInteger = 1 To face_count
                    fs.Position += 12
                    temp_face = New Face
                    temp_face.Vertexes(0) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                    temp_face.Vertexes(1) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                    temp_face.Vertexes(2) = New Vertex(reader2.ReadSingle, reader2.ReadSingle, reader2.ReadSingle)
                    temp_solid.Faces.Add(temp_face)
                    fs.Position += 2
                Next
                Solids.Add(temp_solid)
            Loop
            reader2.Close()
        Else
            Do Until reader.EndOfStream
                s = LTrim(reader.ReadLine)
                ss = Split(s)
                Select Case ss(0)
                    Case "solid"
                        temp_solid = New Solid With {.Title = ss(1)}
                        Exit Select
                    Case "facet"
                        temp_face = New Face
                        Exit Select
                    Case "vertex"
                        temp_face.Vertexes(vertex_count) = New Vertex(ss(1), ss(2), ss(3))
                        vertex_count = (vertex_count + 1) Mod 3
                        Exit Select
                    Case "endfacet"
                        temp_solid.Faces.Add(temp_face)
                        Exit Select
                    Case "endsolid"
                        Solids.Add(temp_solid)
                        Exit Select
                End Select
            Loop
            reader.Close()
        End If
    End Sub
    ''' <summary>
    ''' 将模型保存为STL文件
    ''' </summary>
    ''' <param name="Path">STL文件的路径</param>
    ''' <param name="EncodeMode">编码方式</param>
    Public Sub Save(Path As String, Optional EncodeMode As EncodeMode = EncodeMode.Binary)
        If EncodeMode = EncodeMode.Ascii Then
            Dim writer As New StreamWriter(Path)
            For Each sld In Solids
                writer.WriteLine("solid " + sld.Title)
                For Each fc In sld.Faces
                    writer.WriteLine("    facet normal " + fc.Normal.X.ToString("e") + " " + fc.Normal.Y.ToString("e") + " " + fc.Normal.Z.ToString("e"))
                    writer.WriteLine("        outer loop")
                    For Each po In fc.Vertexes
                        writer.WriteLine("            vertex " + po.X.ToString("e") + " " + po.Y.ToString("e") + " " + po.Z.ToString("e"))
                    Next
                    writer.WriteLine("        endloop")
                    writer.WriteLine("    endfacet")
                Next
                writer.WriteLine("endsolid")
            Next
            writer.Flush()
            writer.Close()
        Else
            Dim fs As New FileStream(Path, FileMode.Create)
            Dim writer As New BinaryWriter(fs)
            Dim zb As Byte = 0
            Dim count_face As UInteger
            For Each sld In Solids
                For i = 0 To 79
                    writer.Write(sld.Title(i))
                    If fs.Position >= 79 Then
                        Exit For
                    End If
                Next
                For i = 1 To 80 - fs.Position
                    writer.Write(zb)
                    count_face = sld.Faces.Count
                    writer.Write(count_face)
                Next
                For Each fc In sld.Faces
                    writer.Write(fc.Normal.X)
                    writer.Write(fc.Normal.Y)
                    writer.Write(fc.Normal.Z)
                    For Each po In fc.Vertexes
                        writer.Write(po.X)
                        writer.Write(po.Y)
                        writer.Write(po.Z)
                    Next
                    writer.Write(zb)
                    writer.Write(zb)
                Next
            Next
            writer.Flush()
            writer.Close()
        End If
    End Sub
    ''' <summary>
    ''' 枚举STL文件编码模式
    ''' </summary>
    Public Enum EncodeMode As Byte
        ''' <summary>
        ''' 二进制模式
        ''' </summary>
        Binary = 0
        ''' <summary>
        ''' Ascii模式
        ''' </summary>
        Ascii = 1
    End Enum
End Class