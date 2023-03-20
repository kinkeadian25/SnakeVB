Public Class Snake
    Public Property Segments As List(Of Point)
    Private _growthPending As Integer = 0

    Public Sub New(ByVal head As Point, ByVal length As Integer)
        Segments = New List(Of Point)

        For i As Integer = 0 To length - 1
            Segments.Add(New Point(head.X - (10 * i), head.Y))
        Next
    End Sub

    Public Sub Move(ByVal direction As Direction)
        Dim head As Point = Segments.First()

        Select Case direction
            Case Direction.Up
                head = New Point(head.X, head.Y - 10)
            Case Direction.Down
                head = New Point(head.X, head.Y + 10)
            Case Direction.Left
                head = New Point(head.X - 10, head.Y)
            Case Direction.Right
                head = New Point(head.X + 10, head.Y)
        End Select

        Segments.Insert(0, head)

        If _growthPending > 0 Then
            _growthPending -= 1
        Else
            Segments.RemoveAt(Segments.Count - 1)
        End If
    End Sub

    Public Sub Grow()
        _growthPending += 1
    End Sub

    Public Function IsOutOfBounds(ByVal bounds As Rectangle) As Boolean
        Return Segments.First().X < bounds.Left OrElse
               Segments.First().X + 10 > bounds.Right OrElse
               Segments.First().Y < bounds.Top OrElse
               Segments.First().Y + 10 > bounds.Bottom
    End Function

    Public Function IsIntersecting() As Boolean
        Return Segments.Skip(1).Contains(Segments.First())
    End Function
End Class


Public Enum Direction
    Left
    Right
    Up
    Down
End Enum
