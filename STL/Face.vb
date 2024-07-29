''' <summary>
''' 表示面
''' </summary>
Public Class Face
    ''' <summary>
    ''' 获取或设置这个面的顶点
    ''' </summary>
    Public Vertexes(2) As Vertex
    ''' <summary>
    ''' 获取这个面的法向量
    ''' </summary>
    Public ReadOnly Property Normal As Vector = GetFacetNormal()

    ''' <summary>
    ''' 创建新的面
    ''' </summary>
    Public Sub New()
        Vertexes(0) = New Vertex(0, 0, 0)
        Vertexes(1) = New Vertex(0, 0, 0)
        Vertexes(2) = New Vertex(0, 0, 0)
    End Sub
    ''' <summary>
    ''' 获取这个面的法向量
    ''' </summary>
    ''' <returns>法向量</returns>
    Private Function GetFacetNormal() As Vector
        Dim normal As New Vector
        If Not IsNothing(Vertexes(2)) Then
            Dim v1x As Single = Vertexes(1).X - Vertexes(0).X
            Dim v1y As Single = Vertexes(1).Y - Vertexes(0).Y
            Dim v1z As Single = Vertexes(1).Z - Vertexes(0).Z
            Dim v2x As Single = Vertexes(2).X - Vertexes(1).X
            Dim v2y As Single = Vertexes(2).Y - Vertexes(1).Y
            Dim v2z As Single = Vertexes(2).Z - Vertexes(1).Z
            normal.X = v1y * v2z - v1z * v2y
            normal.Y = v1z * v2x - v1x * v2z
            normal.Z = v1x * v2y - v1y * v2x
            normal.Normalize()

        End If


        Return normal
    End Function
End Class
