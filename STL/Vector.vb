Imports System.Math
''' <summary>
''' 表示三维向量
''' </summary>
Public Class Vector
    ''' <summary>
    ''' 获取或设置向量的X坐标
    ''' </summary>
    Public Property X As Single
    ''' <summary>
    ''' 获取或设置向量的Y坐标
    ''' </summary>
    Public Property Y As Single
    ''' <summary>
    ''' 获取或设置向量的Z坐标
    ''' </summary>
    Public Property Z As Single
    ''' <summary>
    ''' 获取向量的长度
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Length = Sqrt(X ^ 2 + Y ^ 2 + Z ^ 2)

    ''' <summary>
    ''' 创建新的三维向量
    ''' </summary>
    ''' <param name="X">向量的X坐标</param>
    ''' <param name="Y">向量的Y坐标</param>
    ''' <param name="Z">向量的Z坐标</param>
    Public Sub New(X As Single, Y As Single, Z As Single)
        Me.X = X
        Me.Y = Y
        Me.Z = Z
    End Sub
    ''' <summary>
    ''' 创建新的零向量
    ''' </summary>
    Public Sub New()
        X = 0
        Y = 0
        Z = 0
    End Sub
    ''' <summary>
    ''' 将向量转换为单位向量
    ''' </summary>
    Public Sub Normalize()
        If Length = 0 Then
            X = 1
        Else
            X /= Length
            Y /= Length
            Z /= Length
        End If
    End Sub
End Class
