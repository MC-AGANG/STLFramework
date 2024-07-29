''' <summary>
''' 表示顶点
''' </summary>
Public Class Vertex
    ''' <summary>
    ''' 获取或设置顶点的X坐标
    ''' </summary>
    Public Property X As Single
    ''' <summary>
    ''' 获取或设置顶点的Y坐标
    ''' </summary>
    Public Property Y As Single
    ''' <summary>
    ''' 获取或设置顶点的Z坐标
    ''' </summary>
    Public Property Z As Single
    ''' <summary>
    ''' 创建新的顶点
    ''' </summary>
    ''' <param name="X">顶点的X坐标</param>
    ''' <param name="Y">顶点的Y坐标</param>
    ''' <param name="Z">顶点的Z坐标</param>
    Public Sub New(X As Single, Y As Single, Z As Single)
        Me.X = X
        Me.Y = Y
        Me.Z = Z
    End Sub
End Class
