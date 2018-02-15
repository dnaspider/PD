Public Class PD

    '@dnaspider

    Private Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort
    Private Declare Sub Keybd_event Lib "user32" Alias "keybd_event" (ByVal bVk As Integer, bScan As Integer, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)

    Dim g_drag As Boolean
    Dim g_drag_x As Integer
    Dim g_drag_y As Integer
    Sub DragformInit()
        g_drag = True
        g_drag_x = Cursor.Position.X - Me.Left
        g_drag_y = Cursor.Position.Y - Me.Top
    End Sub
    Sub DragForm()
        If g_drag = True Then
            Me.Top = Cursor.Position.Y - g_drag_y
            Me.Left = Cursor.Position.X - g_drag_x
        End If
    End Sub


    Sub Key(key As Integer)
        '48-57 0-9
        '65-90 a-z
        Keybd_event(key, 0, 0, 0)  'press char 
        Keybd_event(key, 0, 2, 0) 'release
        GetAsyncKeyState(key) 'clear
    End Sub
    Sub KeyKey(key As Integer, presses As Integer)
        For index = 1 To presses
            Keybd_event(key, 0, 0, 0)
            Keybd_event(key, 0, 2, 0)
        Next
        GetAsyncKeyState(key)
    End Sub
    Sub KeyRelease(key As Integer)
        Keybd_event(key, 0, &H2, 0)
    End Sub

    Sub TextClear()
        TextBox1.SelectAll()
        TextBox1.SelectedText = ""
    End Sub

    Sub Sleep(ms As Integer)
        System.Threading.Thread.Sleep(ms)
    End Sub

    Sub Timeout(ms As Double)
        Dim x = Now.AddMilliseconds(ms)
        Do While x > Now
            If CBool(GetAsyncKeyState(Keys.Pause)) Then Exit Sub
            Application.DoEvents()
        Loop
    End Sub

    Sub LoadDb()
        For Each item As String In My.Settings.SettingDB
            ListBox1.Items.Add(item)
        Next
    End Sub
    Sub AddDbItm()
        My.Settings.SettingDB.Add(TextBox1.Text)
        ListBox1.Items.Add(TextBox1.Text)
        TextClear()
        ListBox1.SelectedIndex() = ListBox1.Items.Count - 1
        KeyRelease(Keys.S)
        KeyRelease(g_specialKey)
        TextBox2.Clear()
    End Sub
    Sub RemoveDbItm()
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        Dim x As String = TextBox1.Text
        TextClear()
        TextBox1.AppendText(ListBox1.SelectedItem.ToString)
        My.Settings.SettingDB.RemoveAt(ListBox1.SelectedIndex)
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        Key(Keys.Down)
        TextClear()
        TextBox1.AppendText(x)
    End Sub
    Sub UpdateDbItm()
        If ListBox1.SelectedIndex < 0 Or TextBox1.Text = "" Then Exit Sub

        Dim x As Integer = TextBox1.SelectionStart
        Dim i As Integer = ListBox1.SelectedIndex

        ListBox1.Items.RemoveAt(i)
        My.Settings.SettingDB.RemoveAt(i)
        ListBox1.Items.Insert(i, TextBox1.Text)
        My.Settings.SettingDB.Insert(i, TextBox1.Text)

        ListBox1.SelectedItem() = ListBox1.Items.Item(i)
        TextBox1.SelectionStart = x
    End Sub
    Sub EditDbItm()
        If ListBox1.GetItemText(ListBox1.SelectedItem) > "" And TextBox1.Text = "" Then
            TextBox1.AppendText(ListBox1.SelectedItem.ToString)
        End If
    End Sub

    Sub ClearAllKeys()
        For i = 48 To 57
            GetAsyncKeyState(i)
        Next
        For i = 65 To 90
            GetAsyncKeyState(i)
        Next
    End Sub

    Sub TextMock()
        If TextBox2.TextLength > 20 Then
            TextBox2.Clear()
            Exit Sub
        End If
        If TextBox2.Text.StartsWith("«") Then
            Exit Sub
        End If
        TextBox2.Text = Microsoft.VisualBasic.Right(TextBox2.Text, g_length)
    End Sub




    Private Sub PD_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Timer1.Dispose()
    End Sub

    Private Sub PD_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadDb()
    End Sub

    Private Sub PD_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If MouseButtons = Windows.Forms.MouseButtons.Left Then DragformInit()
    End Sub

    Private Sub PD_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        g_drag = False
    End Sub

    Private Sub PD_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If MouseButtons = Windows.Forms.MouseButtons.Left Then DragForm()
    End Sub

    Dim g_specialKey As Integer = Keys.RControlKey
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If CBool(GetAsyncKeyState(Keys.Back)) Then If TextBox2.Text > "" Then TextBox2.Text = Microsoft.VisualBasic.Left(TextBox2.Text, Len(TextBox2.Text) - 1)

        If CBool(GetAsyncKeyState(g_specialKey)) Then If TextBox2.Text.StartsWith("«") Then TextBox2.Clear() Else TextBox2.Text = "«"

        'If My.Settings.SettingBracketModeOnly And TextBox2.Text.StartsWith("«") = False Then
        '    ClearAllKeys()
        '    Exit Sub
        'End If

        If CBool(GetAsyncKeyState(Keys.Z)) Then TextBox2.AppendText("z")
        If CBool(GetAsyncKeyState(Keys.X)) Then TextBox2.AppendText("x")
        If CBool(GetAsyncKeyState(Keys.C)) Then TextBox2.AppendText("c")
        If CBool(GetAsyncKeyState(Keys.V)) Then TextBox2.AppendText("v")
        If CBool(GetAsyncKeyState(Keys.B)) Then TextBox2.AppendText("b")
        If CBool(GetAsyncKeyState(Keys.N)) Then TextBox2.AppendText("n")
        If CBool(GetAsyncKeyState(Keys.M)) Then TextBox2.AppendText("m")

        If CBool(GetAsyncKeyState(Keys.A)) Then TextBox2.AppendText("a")
        If CBool(GetAsyncKeyState(Keys.S)) Then TextBox2.AppendText("s")
        If CBool(GetAsyncKeyState(Keys.D)) Then TextBox2.AppendText("d")
        If CBool(GetAsyncKeyState(Keys.F)) Then TextBox2.AppendText("f")
        If CBool(GetAsyncKeyState(Keys.G)) Then TextBox2.AppendText("g")
        If CBool(GetAsyncKeyState(Keys.H)) Then TextBox2.AppendText("h")
        If CBool(GetAsyncKeyState(Keys.J)) Then TextBox2.AppendText("j")
        If CBool(GetAsyncKeyState(Keys.K)) Then TextBox2.AppendText("k")
        If CBool(GetAsyncKeyState(Keys.L)) Then TextBox2.AppendText("l")

        If CBool(GetAsyncKeyState(Keys.Q)) Then TextBox2.AppendText("q")
        If CBool(GetAsyncKeyState(Keys.W)) Then TextBox2.AppendText("w")
        If CBool(GetAsyncKeyState(Keys.E)) Then TextBox2.AppendText("e")
        If CBool(GetAsyncKeyState(Keys.R)) Then TextBox2.AppendText("r")
        If CBool(GetAsyncKeyState(Keys.T)) Then TextBox2.AppendText("t")
        If CBool(GetAsyncKeyState(Keys.Y)) Then TextBox2.AppendText("y")
        If CBool(GetAsyncKeyState(Keys.U)) Then TextBox2.AppendText("u")
        If CBool(GetAsyncKeyState(Keys.I)) Then TextBox2.AppendText("i")
        If CBool(GetAsyncKeyState(Keys.O)) Then TextBox2.AppendText("o")
        If CBool(GetAsyncKeyState(Keys.P)) Then TextBox2.AppendText("p")

        If CBool(GetAsyncKeyState(Keys.D1)) Then TextBox2.AppendText("1")
        If CBool(GetAsyncKeyState(Keys.D2)) Then TextBox2.AppendText("2")
        If CBool(GetAsyncKeyState(Keys.D3)) Then TextBox2.AppendText("3")
        If CBool(GetAsyncKeyState(Keys.D4)) Then TextBox2.AppendText("4")
        If CBool(GetAsyncKeyState(Keys.D5)) Then TextBox2.AppendText("5")
        If CBool(GetAsyncKeyState(Keys.D6)) Then TextBox2.AppendText("6")
        If CBool(GetAsyncKeyState(Keys.D7)) Then TextBox2.AppendText("7")
        If CBool(GetAsyncKeyState(Keys.D8)) Then TextBox2.AppendText("8")
        If CBool(GetAsyncKeyState(Keys.D9)) Then TextBox2.AppendText("9")
        If CBool(GetAsyncKeyState(Keys.D0)) Then TextBox2.AppendText("0")

        If CBool(GetAsyncKeyState(Keys.Escape)) And CBool(GetAsyncKeyState(Keys.H)) Then 'toggle visibility 
            KeyRelease(Keys.H)
            KeyRelease(Keys.Escape)
            If Not Me.Visible Then
                Me.Show()
                If Me.Text > "" Then AppActivate("PD")
                Exit Sub
            End If
            If Me.Visible Then
                Me.Hide()
                Exit Sub
            End If
        End If

    End Sub

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles TextBox1.DoubleClick
        If TextBox1.SelectedText = "" Then
            SendKeys.Send("«»{left}") '«»
            Exit Sub
        End If

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = ChrW(19) Then 'ctrl + s
            AddDbItm()
        End If
        If e.KeyChar = ChrW(21) Then 'ctrl + u 
            UpdateDbItm()
        End If
        If e.KeyChar = ChrW(5) Then 'ctrl + e  
            TextBox1.Undo()
            EditDbItm()
        End If
    End Sub

    Dim g_length As Integer = My.Settings.Setting_g_length
    Dim g_i As Integer = 0
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text = "'" Then Exit Sub
        If My.Settings.SettingTitleTip = True Then Me.Text = "PD > " & TextBox2.Text


        If ((TextBox2.TextLength = g_length) Or (TextBox2.Text.StartsWith("«") And TextBox2.TextLength >= 1)) = False Then
            TextMock()
            Exit Sub
        End If

        For i = 0 To ListBox1.Items.Count - 1
            If TextBox2.Text = "«" Then Exit Sub
            If ListBox1.Items(i).ToString = "" Or
                ListBox1.Items(i).ToString.StartsWith("'") Then Continue For 'rem

            g_i = i
            If TextBox2.Text.StartsWith("«") Then '«x»|«x-»

                If ListBox1.Items(i).ToString.StartsWith(TextBox2.Text + "»") Then '«x»
                    Sk(1)
                    Exit For
                End If

                If ListBox1.Items(i).ToString.StartsWith(TextBox2.Text + "-»") Then '«x-»
                    Sk(2)
                    Exit For
                End If

                Continue For
            End If

            If Microsoft.VisualBasic.Left(TextBox2.Text, g_length) = Microsoft.VisualBasic.Left(ListBox1.Items(i).ToString, g_length) Then 'x
                Sk(3)
                Exit For
            End If
        Next

        TextMock()
    End Sub

    Sub Sk(opt As Integer)
        TextBox2.Text = "'"
        ListBox1.SelectedItem() = ListBox1.Items.Item(g_i)
        Select Case opt
            Case 1 '«x»
                'Console.WriteLine("code: " & ListBox1.SelectedItem.ToString.Substring(1, ListBox1.SelectedItem.ToString.IndexOf("»") - 1)) 'code
                'Console.WriteLine("string: " & ListBox1.SelectedItem.ToString.Substring(ListBox1.SelectedItem.ToString.IndexOf("»") + 1, ListBox1.SelectedItem.ToString.Length - ListBox1.SelectedItem.ToString.IndexOf("»") - 1))
                SendKeys.Send(Microsoft.VisualBasic.Right(ListBox1.SelectedItem.ToString, Len(ListBox1.Items.Item(g_i)) - ListBox1.SelectedItem.ToString.IndexOf("»") - 1))
            Case 2 '«x-»
                KeyKey(Keys.Back, ListBox1.SelectedItem.ToString.IndexOf("»") - 2)
                SendKeys.Send(Microsoft.VisualBasic.Right(ListBox1.SelectedItem.ToString, Len(ListBox1.Items.Item(g_i)) - ListBox1.SelectedItem.ToString.IndexOf("»") - 1))
            Case 3 'x
                SendKeys.Send(Microsoft.VisualBasic.Right(ListBox1.SelectedItem.ToString, ListBox1.SelectedItem.ToString.Length - g_length))
        End Select
        ClearAllKeys()
        TextBox2.Clear()
    End Sub

    Private Sub ListBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyUp
        'Console.WriteLine(e.KeyValue)
        If (e.KeyValue) = 46 Then RemoveDbItm() 'delete

    End Sub

    Private Sub ListBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ListBox1.KeyPress
        If e.KeyChar = ChrW(5) Then 'ctrl + e  
            TextClear()
            EditDbItm()
            TextBox1.Focus()
            Timeout(1000)
            ListBox1.SelectedIndex() = ListBox1.SelectedIndex
        End If
        If e.KeyChar = ChrW(21) Then   'ctrl + u 
            UpdateDbItm()
            Timeout(1000)
            ListBox1.SelectedIndex() = ListBox1.SelectedIndex
        End If
    End Sub
End Class
