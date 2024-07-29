Imports STL
Imports STL.WPF
Class MainWindow
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Dim sf As New STLFile("C:\Users\AGANG\Desktop\皮带轮.STL")
        Dim p As Media3D.Point3D = d.LoadSTL(sf)
        camera.Position = New Media3D.Point3D(p.X * 2, p.Y * 2, p.Z * 2)
        camera.LookDirection = New Media3D.Vector3D(-p.X, -p.Y, -p.Z)
        sf.Save("C:\Users\AGANG\Desktop\te.STL", STLFile.EncodeMode.Binary)
    End Sub
End Class
