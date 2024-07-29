Imports System.Runtime.CompilerServices
Imports System.Windows.Controls
Imports STL
Imports System.Windows.Media.Media3D
Public Module Module1
    ''' <summary>
    ''' 将STL模型加载到WPF模型当中
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="Model">要加载的STL模型</param>
    ''' <returns>三个坐标最大值的点，便于调节镜头视角</returns>
    <Extension>
    Public Function LoadSTL(e As MeshGeometry3D, Model As STLFile) As Point3D
        e.Positions.Clear()
        Dim mx, my, mz As Double
        For Each SLD In Model.Solids
            For Each fc In SLD.Faces
                For Each pt In fc.Vertexes
                    e.Positions.Add(New Point3D(pt.X, pt.Y, pt.Z))
                    If pt.X > mx Then
                        mx = pt.X
                    End If
                    If pt.Y > my Then
                        my = pt.Y
                    End If
                    If pt.Z > mz Then
                        mz = pt.Z
                    End If
                Next
            Next
        Next
        Return New Point3D(mx, my, mz)
    End Function
End Module
