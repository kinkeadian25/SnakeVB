Imports System.Threading

Public Class SnakeForm
    Private _snake As Snake
    Private _direction As Direction
    Private _food As Point
    Private _rand As New Random()
    Private _score As Integer = 0

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Controls.Add(PictureBox1)
        Me.Controls.Add(ScoreLabel)

        _snake = New Snake(New Point(80, 80), 5)
        _direction = Direction.Right
        _food = GenerateFood()

        UpdateScoreLabel()

        Timer1.Start()
    End Sub

    Private Function GenerateFood() As Point
        Dim x As Integer = _rand.Next(PictureBox1.Width \ 10) * 10
        Dim y As Integer = _rand.Next(PictureBox1.Height \ 10) * 10
        Return New Point(x, y)
    End Function

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        _snake.Move(_direction)

        If _snake.Segments.First() = _food Then
            _snake.Grow()
            _food = GenerateFood()
            AddScore(10)
        End If

        If _snake.IsOutOfBounds(PictureBox1.ClientRectangle) OrElse _snake.IsIntersecting() Then
            Timer1.Stop()
            MessageBox.Show("Game Over!")
        End If

        PictureBox1.Invalidate()
    End Sub


    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles PictureBox1.Paint
        Dim graphics As Graphics = e.Graphics

        DrawSnake(graphics)
        DrawFood(graphics)
    End Sub

    Private Sub DrawSnake(ByVal graphics As Graphics)
        For Each segment As Point In _snake.Segments
            graphics.FillRectangle(Brushes.Green, segment.X, segment.Y, 10, 10)
        Next
    End Sub

    Private Sub DrawFood(ByVal graphics As Graphics)
        graphics.FillRectangle(Brushes.Red, _food.X, _food.Y, 10, 10)
    End Sub

    Private Sub AddScore(ByVal points As Integer)
        _score += points
        UpdateScoreLabel()
    End Sub

    Private Sub UpdateScoreLabel()
        ScoreLabel.Text = $"Score: {_score}"
    End Sub

    Private Sub SnakeForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.W
                _direction = Direction.Up
            Case Keys.S
                _direction = Direction.Down
            Case Keys.A
                _direction = Direction.Left
            Case Keys.D
                _direction = Direction.Right
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Start()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Timer1.Enabled Then
            Timer1.Stop()
            Button3.Text = "Resume"
        Else
            Timer1.Start()
            Button3.Text = "Pause"
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Stop the timer to prevent the game from running while resetting the state
        Timer1.Stop()

        ' Reset the game state
        _snake = New Snake(New Point(10, 10), 5)
        _direction = Direction.Right
        _food = GenerateFood()
        _score = 0

        ' Update the score label
        UpdateScoreLabel()

        ' Restart the game by starting the timer
        Timer1.Start()
    End Sub

End Class
