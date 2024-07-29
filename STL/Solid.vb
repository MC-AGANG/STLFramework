''' <summary>
''' 表示体
''' </summary>
Public Class Solid
    ''' <summary>
    ''' 包含这个体中的所有面
    ''' </summary>
    Public Faces As List(Of Face)
    ''' <summary>
    ''' 获取或设置这个体的标题<br/>
    ''' 标题不能以“solid”开头
    ''' </summary>
    ''' <returns></returns>
    Public Property Title As String
    ''' <summary>
    ''' 创建新的体
    ''' </summary>
    Public Sub New()
        Faces = New List(Of Face)
    End Sub
End Class
