Public Class PD

    '@dnaspider

    Private Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort
    Private Declare Sub Keybd_event Lib "user32" Alias "keybd_event" (ByVal bVk As Integer, bScan As Integer, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
    Private Declare Function SetCursorPos Lib "user32.dll" (ByVal X As Int32, ByVal Y As Int32) As UShort
    Private Declare Sub Mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)

    Sub LeftClick()
        Mouse_event(&H2, 0, 0, 0, 0)
        Mouse_event(&H4, 0, 0, 0, 0)
    End Sub
    Sub LeftHold()
        Mouse_event(&H2, 0, 0, 0, 0)
    End Sub
    Sub LeftRelease()
        Mouse_event(&H4, 0, 0, 0, 0)
    End Sub
    Sub MiddleClick()
        Mouse_event(&H20, 0, 0, 0, 0)
        Mouse_event(&H40, 0, 0, 0, 0)
    End Sub
    Sub MiddleHold()
        Mouse_event(&H20, 0, 0, 0, 0)
    End Sub
    Sub MiddleRelease()
        Mouse_event(&H40, 0, 0, 0, 0)
    End Sub
    Sub RightClick()
        Mouse_event(&H8, 0, 0, 0, 0) '&H2
        Mouse_event(&H10, 0, 0, 0, 0) '&H4
    End Sub
    Sub RightHold()
        Mouse_event(&H8, 0, 0, 0, 0)
    End Sub
    Sub RightRelease()
        Mouse_event(&H10, 0, 0, 0, 0)
    End Sub



    Dim g_drag As Boolean
    Dim g_drag_x As Integer
    Dim g_drag_y As Integer
    Sub DragFormInit()
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


    Sub Key(key As Integer, shft As Boolean, presses As Integer)
        If shft Then Keybd_event(Keys.RShiftKey, 0, 1, 0)
        If CInt(g_presses) > 1 Then presses = CInt(g_presses)
        For i = 1 To presses
            Keybd_event(key, 0, 0, 0)
            Keybd_event(key, 0, 2, 0)
        Next
        If shft Then Keybd_event(Keys.RShiftKey, 0, 2, 0)
        GetAsyncKeyState(key)
    End Sub
    Sub KeyHold(key As Integer)
        Keybd_event(key, 0, 1, 0)
    End Sub
    Sub KeyRelease(key As Integer)
        Keybd_event(key, 0, 2, 0)
    End Sub
    Sub TextClear()
        TextBox1.SelectAll()
        TextBox1.SelectedText = ""
    End Sub

    Sub SleepMinutes(m As Integer)
        Dim x = Now.AddMinutes(m)
        Application.DoEvents()
        Do While x > Now
            If CBool(GetAsyncKeyState(Keys.Pause)) Or CBool(GetAsyncKeyState(Keys.Escape)) Then Exit Do
            System.Threading.Thread.Sleep(m)
        Loop
        Application.DoEvents()
    End Sub

    Sub SleepMS(ms As Integer)
        Dim x = Now.AddMilliseconds(ms)
        Application.DoEvents()
        Do While x > Now
            If CBool(GetAsyncKeyState(Keys.Pause)) Or CBool(GetAsyncKeyState(Keys.Escape)) Then Exit Do
            System.Threading.Thread.Sleep(ms)
        Loop
        Application.DoEvents()
    End Sub

    Sub Sleep(ms As Integer)
        System.Threading.Thread.Sleep(ms)
        Application.DoEvents()
    End Sub

    Sub Timeout(ms As Integer)
        Dim x = Now.AddMilliseconds(ms)
        Do While x > Now
            If CBool(GetAsyncKeyState(Keys.Pause)) Or CBool(GetAsyncKeyState(Keys.Escape)) Then Exit Sub
            Application.DoEvents()
        Loop
    End Sub

    Sub LoadArray()
        ar.Clear()
        For x = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(x).ToString.StartsWith(p_) Then ar.Add(x & ":" & ListBox1.Items(x).ToString.Substring(1, ListBox1.Items(x).ToString.IndexOf(_p) - 1))
        Next
        'For x = 0 To ar.Count - 1
        '    Console.WriteLine(ar.Count & ": ar: " & ar(x).ToString)
        'Next
    End Sub

    Sub LoadDb()
        For Each item As String In My.Settings.SettingDB
            ListBox1.Items.Add(CStr(item))
        Next
        LoadArray()
    End Sub
    Sub AddDbItm()
        My.Settings.SettingDB.Add(TextBox1.Text)
        ListBox1.Items.Add(TextBox1.Text)
        TextClear()
        ListBox1.SelectedIndex() = ListBox1.Items.Count - 1
        KeyRelease(Keys.S)
        KeyRelease(g_specialKey)
        CleanMock()
        LoadArray()
    End Sub
    Sub RemoveDbItm()
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        Dim x As String = TextBox1.Text
        TextClear()
        TextBox1.AppendText(ListBox1.SelectedItem.ToString)
        My.Settings.SettingDB.RemoveAt(ListBox1.SelectedIndex)
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        Key(Keys.Down, False, 1)
        TextClear()
        TextBox1.AppendText(x)
        LoadArray()
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

        CleanMock()
        LoadArray()
    End Sub
    Sub CleanMock()
        Sleep(333)
        TextBox2.Clear()
    End Sub
    Sub CleanSelect()
        Sleep(333)
        ListBox1.SelectedIndex() = ListBox1.SelectedIndex
        CleanMock()
    End Sub
    Sub EditDbItm()
        If ListBox1.GetItemText(ListBox1.SelectedItem) > "" And TextBox1.Text = "" Then
            TextBox1.AppendText(ListBox1.SelectedItem.ToString)
            CleanMock()
        End If
    End Sub

    Sub ClearAllKeys()
        For i = 48 To 57 '0-9
            GetAsyncKeyState(i)
        Next
        For i = 65 To 90 'a-z
            GetAsyncKeyState(i)
        Next
        GetAsyncKeyState(g_specialKey)
        GetAsyncKeyState(Keys.Insert)
        GetAsyncKeyState(Keys.Space)
#Region "rem"
        'GetAsyncKeyState(Keys.Escape)
        'GetAsyncKeyState(Keys.F1)
        'GetAsyncKeyState(Keys.F2)
        'GetAsyncKeyState(Keys.F3)
        'GetAsyncKeyState(Keys.F4)
        'GetAsyncKeyState(Keys.F5)
        'GetAsyncKeyState(Keys.F6)
        'GetAsyncKeyState(Keys.F7)
        'GetAsyncKeyState(Keys.F8)
        'GetAsyncKeyState(Keys.F9)
        'GetAsyncKeyState(Keys.F10)
        'GetAsyncKeyState(Keys.F11)
        'GetAsyncKeyState(Keys.F12)
        'GetAsyncKeyState(Keys.PrintScreen)
        'GetAsyncKeyState(Keys.Scroll)
        'GetAsyncKeyState(Keys.Pause)

        'GetAsyncKeyState(Keys.LWin)
        'GetAsyncKeyState(Keys.RWin)
        'GetAsyncKeyState(Keys.LShiftKey)
        'GetAsyncKeyState(Keys.RShiftKey)
        'GetAsyncKeyState(Keys.LMenu) 'alt
        'GetAsyncKeyState(Keys.RMenu)
        'GetAsyncKeyState(Keys.LControlKey)
        'GetAsyncKeyState(Keys.RControlKey)

        'GetAsyncKeyState(Keys.Enter)
        'GetAsyncKeyState(Keys.Tab)
        'GetAsyncKeyState(Keys.CapsLock)
        'GetAsyncKeyState(Keys.Space)
        'GetAsyncKeyState(Keys.OemQuestion)
        'GetAsyncKeyState(Keys.OemOpenBrackets)
        'GetAsyncKeyState(Keys.OemCloseBrackets)
        'GetAsyncKeyState(Keys.OemBackslash)
        'GetAsyncKeyState(Keys.OemPipe)
        'GetAsyncKeyState(Keys.OemSemicolon)
        'GetAsyncKeyState(Keys.OemQuotes)
        'GetAsyncKeyState(Keys.OemMinus)
        'GetAsyncKeyState(Keys.Oemplus)
        'GetAsyncKeyState(Keys.Oemcomma)
        'GetAsyncKeyState(Keys.OemPeriod)
        'GetAsyncKeyState(Keys.Up)
        'GetAsyncKeyState(Keys.Down)
        'GetAsyncKeyState(Keys.Left)
        'GetAsyncKeyState(Keys.Right)
        'GetAsyncKeyState(Keys.PageUp)
        'GetAsyncKeyState(Keys.PageDown)
        'GetAsyncKeyState(Keys.Home)
        'GetAsyncKeyState(Keys.End)
        'GetAsyncKeyState(Keys.Delete)
        'GetAsyncKeyState(Keys.Back)
        'GetAsyncKeyState(Keys.Menu)

        'GetAsyncKeyState(Keys.NumLock)
        'GetAsyncKeyState(Keys.NumPad0)
        'GetAsyncKeyState(Keys.NumPad1)
        'GetAsyncKeyState(Keys.NumPad2)
        'GetAsyncKeyState(Keys.NumPad3)
        'GetAsyncKeyState(Keys.NumPad4)
        'GetAsyncKeyState(Keys.NumPad5)
        'GetAsyncKeyState(Keys.NumPad6)
        'GetAsyncKeyState(Keys.NumPad7)
        'GetAsyncKeyState(Keys.NumPad8)
        'GetAsyncKeyState(Keys.NumPad9)
        'GetAsyncKeyState(Keys.Multiply)
        'GetAsyncKeyState(Keys.Add)
        'GetAsyncKeyState(Keys.Divide)
        'GetAsyncKeyState(Keys.Subtract)
        'GetAsyncKeyState(Keys.Return)

        'GetAsyncKeyState(Keys.Oem1) ';
        'GetAsyncKeyState(Keys.Oem2) '/
        'GetAsyncKeyState(Keys.Oem3) '`
        'GetAsyncKeyState(Keys.Oem4) '[
        'GetAsyncKeyState(Keys.Oem5) '\
        'GetAsyncKeyState(Keys.Oem6) ']
        'GetAsyncKeyState(Keys.Oem7) '

        'GetAsyncKeyState(Keys.VolumeUp)
        'GetAsyncKeyState(Keys.VolumeDown)
        'GetAsyncKeyState(Keys.VolumeMute)
        'GetAsyncKeyState(Keys.MediaPlayPause)
        'GetAsyncKeyState(Keys.MediaStop)
        'GetAsyncKeyState(Keys.MediaPreviousTrack)
        'GetAsyncKeyState(Keys.MediaNextTrack)
#End Region
    End Sub

    Sub TextMock()
        If TextBox2.TextLength > 20 Then TextBox2.Clear() : Exit Sub
        If TextBox2.Text.StartsWith(p_) Then Exit Sub
        TextBox2.Text = Microsoft.VisualBasic.Right(TextBox2.Text, g_length)
    End Sub

    Sub DarkMode()
        If My.Settings.SettingDarkMode = True Then
            ListBox1.BackColor = Color.Black
            ListBox1.ForeColor = My.Settings.SettingDarkModeText
            TextBox1.BackColor = Color.Black
            TextBox1.ForeColor = My.Settings.SettingDarkModeText
            Me.BackColor = Color.Black
        End If
    End Sub

    Sub SaveSettings()
        If Me.Top <> -32000 Then My.Settings.SettingLocationTop = Me.Top
        If Me.Left <> -32000 Then My.Settings.SettingLocationLeft = Me.Left
        If WindowState = 0 And ControlBox = True Then
            My.Settings.SettingHeight = Me.Height
            My.Settings.SettingWidth = Me.Width
        End If
        If ListBox1.Focused Then My.Settings.SettingTabIndex = 1
        If TextBox1.Focused Then My.Settings.SettingTabIndex = 5
        My.Settings.SettingSelectionStart = TextBox1.SelectionStart
        My.Settings.SettingSelectionLength = TextBox1.SelectionLength
        My.Settings.SettingTextBoxZoomFactor = TextBox1.ZoomFactor
        My.Settings.SettingTextBox = TextBox1.Text
        My.Settings.SettingListboxSelectedIndex = ListBox1.SelectedIndex
        My.Settings.SettingListBoxFontSize = Me.ListBox1.Font.Size
        My.Settings.SettingSplitterDistance = SplitContainer1.SplitterDistance

        'config
        If My.Settings.SettingFirstLoad = 0 Then
            My.Settings.SettingIcon = My.Settings.SettingIcon
            My.Settings.SettingTitleText = My.Settings.SettingTitleText
            My.Settings.SettingWordWrap = My.Settings.SettingWordWrap
            My.Settings.SettingCodeLength = My.Settings.SettingCodeLength
            My.Settings.SettingSpecialKey = My.Settings.SettingSpecialKey
            My.Settings.SettingTitleTip = My.Settings.SettingTitleTip
            My.Settings.SettingBracketModeOnlyScan = My.Settings.SettingBracketModeOnlyScan
            My.Settings.SettingInterval = My.Settings.SettingInterval
            My.Settings.SettingDarkMode = My.Settings.SettingDarkMode
            My.Settings.SettingOpacity = My.Settings.SettingOpacity
            My.Settings.SettingTopMost = My.Settings.SettingTopMost
            My.Settings.SettingSendkeysOnlyMode = My.Settings.SettingSendkeysOnlyMode
            My.Settings.SettingBracketOpen = My.Settings.SettingBracketOpen
            My.Settings.SettingBracketClose = My.Settings.SettingBracketClose
            My.Settings.SettingBackgroundImage = My.Settings.SettingBackgroundImage
            My.Settings.SettingInfiniteLoop = My.Settings.SettingInfiniteLoop
            My.Settings.SettingIgnoreWhiteSpaceOpen = My.Settings.SettingIgnoreWhiteSpaceOpen
            My.Settings.SettingIgnoreWhiteSpaceClose = My.Settings.SettingIgnoreWhiteSpaceClose
            My.Settings.SettingInsertSymbol = My.Settings.SettingInsertSymbol
            My.Settings.SettingFirstLoad += 1
        End If

        My.Settings.Save()
    End Sub



    Private Sub PD_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Timer1.Dispose()
        SaveSettings()
    End Sub

    Private Sub PD_Load(sender As Object, e As EventArgs) Handles Me.Load
        If My.Settings.SettingFirstLoad = 0 Then Application.Restart()
        If My.Settings.SettingIcon > "" Then Me.Icon = New Icon(My.Settings.SettingIcon)
        TextBox1.WordWrap = My.Settings.SettingWordWrap
        LoadDb()
        DarkMode()
        Timer1.Interval = My.Settings.SettingInterval
        Me.Opacity = My.Settings.SettingOpacity
        Me.TopMost = My.Settings.SettingTopMost
        Me.Top = My.Settings.SettingLocationTop
        Me.Left = My.Settings.SettingLocationLeft
        Me.Height = My.Settings.SettingHeight
        Me.Width = My.Settings.SettingWidth

        If ListBox1.Items.Count > 0 Then ListBox1.SelectedIndex = My.Settings.SettingListboxSelectedIndex
        Me.ListBox1.Font = New System.Drawing.Font(TextBox1.Font.Name, My.Settings.SettingListBoxFontSize)

        Select Case My.Settings.SettingTabIndex
            Case 1
                ListBox1.Focus()
            Case 5
                TextBox1.Focus()
            Case Else
                ListBox1.Focus()
        End Select

        TextBox1.Text = My.Settings.SettingTextBox
        If TextBox1.Focused Then
            TextBox1.SelectionStart = My.Settings.SettingSelectionStart
            TextBox1.SelectionLength = My.Settings.SettingSelectionLength
        End If
        TextBox1.ZoomFactor = My.Settings.SettingTextBoxZoomFactor
        SplitContainer1.SplitterDistance = My.Settings.SettingSplitterDistance
        SplitContainer1.SplitterWidth = My.Settings.SettingSplitterWidth

        If My.Settings.SettingBackgroundImage > "" Then
            Me.BackgroundImage = Image.FromFile(My.Settings.SettingBackgroundImage)
            FixedSize()
        End If
    End Sub

    Private Sub PD_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If MouseButtons = Windows.Forms.MouseButtons.Left Then DragFormInit()
    End Sub

    Private Sub PD_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        g_drag = False
    End Sub

    Private Sub PD_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If MouseButtons = Windows.Forms.MouseButtons.Left Then DragForm()
    End Sub

    Dim g_specialKey As Integer = My.Settings.SettingSpecialKey
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If CBool(GetAsyncKeyState(Keys.Back)) Then If TextBox2.Text > "" Then TextBox2.Text = Microsoft.VisualBasic.Left(TextBox2.Text, Len(TextBox2.Text) - 1)

        If CBool(GetAsyncKeyState(Keys.Scroll)) Then
            If TextBox1.ContainsFocus Then Exit Sub
            TextBox2.Text = "'"
            If My.Settings.SettingTitleTip = True And ControlBox = True Then Me.Text = My.Settings.SettingTitleText & " > " & g_s
            If TextBox1.Text > "" Then
                If TextBox1.SelectedText.Length > 0 Then g_s = TextBox1.SelectedText
                If TextBox1.SelectedText.Length = 0 Then g_s = TextBox1.Text
                If My.Settings.SettingTitleTip = True And ControlBox = True Then Me.Text = My.Settings.SettingTitleText & " > " & g_s
                PD()
            End If
            If TextBox1.Text = "" And ListBox1.Items.Count > 0 Then PD()
            If My.Settings.SettingTitleTip = True And ControlBox = True Then Me.Text = My.Settings.SettingTitleText
            ClearAllKeys()
            TextBox2.Clear()
        End If

        If TextBox1.ContainsFocus And CBool(GetAsyncKeyState(Keys.F5)) Then
            If TextBox1.Text = "" Then Exit Sub
            TextBox2.Text = "'"
            Dim x As Boolean = Me.Visible
            Me.Visible = False
            Sleep(1)
            If TextBox1.SelectedText.Length > 0 Then
                g_s = TextBox1.SelectedText
                PD()
            Else
                If TextBox1.Text > "" Then g_s = TextBox1.Text : PD()
            End If
            Me.Visible = x
            ClearAllKeys()
            TextBox2.Clear()
        End If

        If CBool(GetAsyncKeyState(g_specialKey)) Then If TextBox2.Text.StartsWith(p_) Then TextBox2.Clear() Else ClearAllKeys() : TextBox2.Text = p_

        If My.Settings.SettingBracketModeOnlyScan And TextBox2.Text.StartsWith(p_) = False Then Exit Sub


        If CBool(GetAsyncKeyState(Keys.Insert)) Then TextBox2.AppendText(My.Settings.SettingInsertSymbol)

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

        If CBool(GetAsyncKeyState(Keys.Space)) Then TextBox2.AppendText(" ")

#Region "rem"
        'If CBool(GetAsyncKeyState(Keys.Escape)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F1)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F2)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F3)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F4)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F5)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F6)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F7)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F8)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F9)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F10)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F11)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.F12)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.PrintScreen)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Scroll)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Pause)) Then TextBox2.AppendText("")

        'If CBool(GetAsyncKeyState(Keys.LWin)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.RWin)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.LShiftKey)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.RShiftKey)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.LMenu)) Then TextBox2.AppendText("") 'alt
        'If CBool(GetAsyncKeyState(Keys.RMenu)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.LControlKey)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.RControlKey)) Then TextBox2.AppendText("")

        'If CBool(GetAsyncKeyState(Keys.Enter)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Tab)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.CapsLock)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Space)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemQuestion)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemOpenBrackets)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemCloseBrackets)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemBackslash)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemPipe)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemSemicolon)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemQuotes)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemMinus)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Oemplus)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Oemcomma)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.OemPeriod)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Up)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Down)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Left)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Right)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.PageDown)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.PageUp)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Home)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.End)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Delete)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Menu)) Then TextBox2.AppendText("")

        'If CBool(GetAsyncKeyState(Keys.NumLock)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad0)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad1)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad2)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad3)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad4)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad5)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad6)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad7)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad8)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.NumPad9)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Multiply)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Add)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Divide)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Subtract)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.Return)) Then TextBox2.AppendText("")

        'If CBool(GetAsyncKeyState(Keys.Oem1)) Then TextBox2.AppendText("") ';
        'If CBool(GetAsyncKeyState(Keys.Oem2)) Then TextBox2.AppendText("") '/
        'If CBool(GetAsyncKeyState(Keys.Oem3)) Then TextBox2.AppendText("") '`
        'If CBool(GetAsyncKeyState(Keys.Oem4)) Then TextBox2.AppendText("") '[
        'If CBool(GetAsyncKeyState(Keys.Oem5)) Then TextBox2.AppendText("") '\
        'If CBool(GetAsyncKeyState(Keys.Oem6)) Then TextBox2.AppendText("") ']
        'If CBool(GetAsyncKeyState(Keys.Oem7)) Then TextBox2.AppendText("") '

        'If CBool(GetAsyncKeyState(Keys.VolumeUp)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.VolumeDown)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.VolumeMute)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.MediaPlayPause)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.MediaStop)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.MediaPreviousTrack)) Then TextBox2.AppendText("")
        'If CBool(GetAsyncKeyState(Keys.MediaNextTrack)) Then TextBox2.AppendText("")
#End Region
        If CBool(GetAsyncKeyState(Keys.Escape)) And CBool(GetAsyncKeyState(Keys.X)) Then
            Clipboard.SetText(p_ & "xy:" & MousePosition.X & "-" & MousePosition.Y & _p)
        End If

        If CBool(GetAsyncKeyState(Keys.Escape)) And CBool(GetAsyncKeyState(Keys.H)) Then 'toggle visibility 
            KeyRelease(Keys.H)
            KeyRelease(Keys.Escape)
            If Not Me.Visible Then
                Me.Show()
                If Me.Text > "" Then AppActivate(My.Settings.SettingTitleText)
                Exit Sub
            End If
            If Me.Visible Then
                Me.Hide()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles TextBox1.DoubleClick
        If TextBox1.Text = "" And TextBox2.TextLength = g_length Then
            TextBox1.AppendText(TextBox2.Text) 'get code
            Exit Sub
        End If
        If TextBox1.SelectedText = "" Then
            SendKeys.Send(p_ & _p & "{left}") '«»
            Exit Sub
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If CBool(GetAsyncKeyState(Keys.LControlKey)) And CBool(GetAsyncKeyState(Keys.X)) Then GetAsyncKeyState(Keys.X) 'clear
        If CBool(GetAsyncKeyState(Keys.LControlKey)) And CBool(GetAsyncKeyState(Keys.C)) Then GetAsyncKeyState(Keys.C)

        If CBool(GetAsyncKeyState(Keys.LControlKey)) And CBool(GetAsyncKeyState(Keys.A)) And TextBox1.TextLength > 1 Then
            TextBox1.SelectionStart = 0
            TextBox1.SelectionLength = TextBox1.TextLength
        End If
        If e.KeyChar = ChrW(19) Then AddDbItm() 'ctrl + s
        If e.KeyChar = ChrW(21) Then UpdateDbItm() 'ctrl + u
        If e.KeyChar = ChrW(5) Then 'ctrl + e
            TextBox1.Undo()
            EditDbItm()
        End If
        If e.KeyChar = ChrW(22) Then  'ctrl + v
            GetAsyncKeyState(Keys.V)
            If Clipboard.GetText > "" Then Clipboard.SetText(Clipboard.GetText.ToString) 'double paste, raw txt
            TextBox1.Undo()
            TextBox1.Paste()
            CleanMock()
        End If

        If CBool(GetAsyncKeyState(Keys.Tab)) Then
            If My.Settings.SettingSendkeysOnlyMode Then Exit Sub
            KeyRelease(Keys.Tab)

            If TextBox1.SelectionStart = 0 Or TextBox1.SelectionStart = TextBox1.TextLength Then
                Key(Keys.Back, False, 1)
                SendKeys.Send(p_ & _p & "{left}")
                Exit Sub
            End If

            Dim x As String
            'If TextBox1.SelectionStart > 1 Then Console.WriteLine(Microsoft.VisualBasic.Mid(TextBox1.Text, TextBox1.SelectionStart - 1))
            If TextBox1.SelectionStart > 1 Then
                x = (Microsoft.VisualBasic.Mid(TextBox1.Text, TextBox1.SelectionStart - 1))
                Select Case Microsoft.VisualBasic.Left(x, 3)

                    Case "ap" & _p
                        AutoComplete("p:", "", 0)
                        Exit Sub
                    Case "au" & _p, "Au" & _p
                        AutoComplete("dio:", "", 0)
                        Exit Sub
                    Case "da" & _p
                        AutoComplete("te", "", 1)
                        Exit Sub
                    Case "de" & _p
                        AutoComplete("lete*", "", 0)
                        Exit Sub
                    Case "en" & _p
                        AutoComplete("d", "", 1)
                        Exit Sub
                    Case "es" & _p
                        AutoComplete("c", "", 1)
                        Exit Sub
                    Case "iw" & _p
                        AutoComplete("", "-iw", 1) 'ignore whitespace 
                        Exit Sub
                    Case "me" & _p
                        AutoComplete("nu", "", 1)
                        Exit Sub
                    Case "mi" & _p
                        AutoComplete("nute:", "", 0)
                        Exit Sub
                    Case "re" & _p
                        AutoComplete("place:|", "", 0)
                        Key(Keys.Left, False, 1)
                        Exit Sub
                    Case "se" & _p 'exe settings
                        g_s = p_ & "win" & _p & "r" & p_ & "-win" & _p & p_ & "app:run" & _p & Application.LocalUserAppDataPath.ToString.Replace("\PD\" & Application.ProductVersion, "") & p_ & "enter" & _p
                        PD()
                        AppActivate(My.Settings.SettingTitleText)
                        Key(Keys.Right, False, 1)
                        Key(Keys.Back, False, 5)
                        TextBox2.Clear()
                        Exit Sub
                    Case "sl" & _p
                        AutoComplete("eep:", "", 0)
                        Exit Sub
                    Case "sp" & _p
                        AutoComplete("ace*", "", 0)
                        Exit Sub
                    Case "st" & _p
                        AutoComplete("op-audio", "", 1)
                        Exit Sub
                    Case "ti" & _p
                        AutoComplete("me", "", 1)
                        Exit Sub
                    Case "to" & _p 'timeout
                        AutoComplete(":", "", 0)
                        Exit Sub
                    Case "wr" & _p 'run
                        TextBox2.Clear()
                        Key(Keys.Right, False, 1)
                        Key(Keys.Back, False, 5)
                        SendKeys.Send(p_ & "win" & _p & "r" & p_ & "-win" & _p & p_ & "app:run" & _p & p_ & "enter" & _p)
                        Key(Keys.Left, False, 7)
                        Exit Sub
                    Case "ws" & _p 'ignore whitespace
                        Key(Keys.Right, False, 1)
                        Key(Keys.Back, False, 5)
                        SendKeys.Send(ws_ & _ws & "{left}")
                        Exit Sub
                    Case "xy" & _p
                        For i = 3 To 1 Step -1
                            Me.Text = My.Settings.SettingTitleText & " > " & p_ & "xy:" & i.ToString & _p
                            Sleep(1000)
                        Next
                        Key(Keys.Back, False, 1)
                        TextBox2.Clear()
                        g_s = (":" & MousePosition.X & "-" & MousePosition.Y)
                        PD()
                        Key(Keys.Right, False, 1)
                        Exit Sub
                    Case "ye" & _p
                        AutoComplete("sno:", "", 0)
                        Exit Sub
                End Select


                If TextBox1.SelectionStart > 0 Then
                    x = (Microsoft.VisualBasic.Mid(TextBox1.Text, TextBox1.SelectionStart))
                    Select Case Microsoft.VisualBasic.Left(x, 2)
                        Case "a" & _p
                            AutoComplete("lt", "-alt", 1)
                            Exit Sub
                        Case "b"
                            AutoComplete("s*", "", 0)
                            Exit Sub
                        Case "c" & _p
                            AutoComplete("trl", "-ctrl", 1)
                            Exit Sub
                        Case "d" & _p
                            AutoComplete("own*", "", 0)
                            Exit Sub
                        Case "e" & _p
                            AutoComplete("nter*", "", 0)
                            Exit Sub
                        Case "h" & _p
                            AutoComplete("ome", "", 1)
                            Exit Sub
                        Case "i" & _p
                            AutoComplete("nsert", "", 1)
                            Exit Sub
                        Case "l" & _p
                            AutoComplete("eft*", "", 0)
                            Exit Sub
                        Case "m" & _p
                            AutoComplete("enu", "", 1)
                            Exit Sub
                        Case "r" & _p
                            AutoComplete("ight*", "", 0)
                            Exit Sub
                        Case "s" & _p
                            AutoComplete("hift", "-shift", 1)
                            Exit Sub
                        Case "t" & _p
                            AutoComplete("ab*", "", 0)
                            Exit Sub
                        Case "u" & _p
                            AutoComplete("p*", "", 0)
                            Exit Sub
                        Case "w" & _p
                            AutoComplete("in", "-win", 1)
                            Exit Sub
                        Case "x" & _p, "y" & _p, "," & _p
                            AutoComplete(":", "", 0)
                            Exit Sub

                        Case p_ & _p
                            Key(Keys.Back, False, 2)
                            Key(Keys.Delete, False, 1)
                            Key(Keys.Tab, False, 1)
                            Exit Sub
                        Case _p & p_, Chr(9), _p
                            Key(Keys.Back, False, 1)
                            SendKeys.Send(p_ & _p & "{left}")
                            Exit Sub
                        Case "*" & _p
                            Key(Keys.Back, False, 2)
                            Key(Keys.Right, True, 1)
                            Exit Sub
                        Case "0" & _p, "1" & _p, "2" & _p, "3" & _p, "4" & _p, "5" & _p, "6" & _p, "7" & _p, "8" & _p, "9" & _p
                            Key(Keys.Back, False, 1)
                            Key(Keys.Right, False, 1)
                            Exit Sub
                    End Select
                End If

            End If
            Key(Keys.Back, False, 1)
            SendKeys.Send(p_ & _p & "{left}")
        End If

    End Sub

    Dim g_length As Integer = My.Settings.SettingCodeLength
    Dim g_i As Integer = 0 'listbox1 item
    Dim g_kb_i As Integer = 0 'kb item c

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text = "'" Then Exit Sub
        If My.Settings.SettingTitleTip = True And ControlBox = True Then Me.Text = My.Settings.SettingTitleText & " > " & TextBox2.Text


        If ((TextBox2.TextLength = g_length) Or (TextBox2.Text.StartsWith(p_) And TextBox2.TextLength >= 1)) = False Then
            TextMock()
            Exit Sub
        End If

        For i = 0 To ListBox1.Items.Count - 1
            If CBool(GetAsyncKeyState(Keys.Escape)) Then Exit For
            If TextBox2.Text = p_ Then Exit Sub
            If ListBox1.Items(i).ToString = "" Or
                ListBox1.Items(i).ToString.StartsWith("'") Then Continue For 'rem

            g_i = i
            If TextBox2.Text.StartsWith(p_) Then '«x»|«x-»
                If ListBox1.Items(i).ToString.StartsWith(TextBox2.Text + _p) Then '«x»
                    Sk(1)
                    Exit For
                End If

                If ListBox1.Items(i).ToString.StartsWith(TextBox2.Text + "-" & _p) Then '«x-»
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

    Sub AutoComplete(fill As String, fill2 As String, right As Integer)
        Key(Keys.Back, False, 1)
        g_s = fill
        PD()
        For i = 1 To right
            Key(Keys.Right, False, 1)
        Next

        If fill2.Length > 0 Then
            SendKeys.Send(p_)
            g_s = fill2
            PD()
            SendKeys.Send(_p)
            Key(Keys.Left, False, fill2.Length + 2)
        End If
    End Sub

    Sub Kb(c As String)
        'On Error GoTo err
        Select Case c
            Case "0"
                Key(Keys.D0, False, 1)
            Case "9"
                Key(Keys.D9, False, 1)
            Case "8"
                Key(Keys.D8, False, 1)
            Case "7"
                Key(Keys.D7, False, 1)
            Case "6"
                Key(Keys.D6, False, 1)
            Case "5"
                Key(Keys.D5, False, 1)
            Case "4"
                Key(Keys.D4, False, 1)
            Case "3"
                Key(Keys.D3, False, 1)
            Case "2"
                Key(Keys.D2, False, 1)
            Case "1"
                Key(Keys.D1, False, 1)

            Case "a"
                Key(Keys.A, False, 1)
            Case "b"
                Key(Keys.B, False, 1)
            Case "c"
                Key(Keys.C, False, 1)
            Case "d"
                Key(Keys.D, False, 1)
            Case "e"
                Key(Keys.E, False, 1)
            Case "f"
                Key(Keys.F, False, 1)
            Case "g"
                Key(Keys.G, False, 1)
            Case "h"
                Key(Keys.H, False, 1)
            Case "i"
                Key(Keys.I, False, 1)
            Case "j"
                Key(Keys.J, False, 1)
            Case "k"
                Key(Keys.K, False, 1)
            Case "l"
                Key(Keys.L, False, 1)
            Case "m"
                Key(Keys.M, False, 1)
            Case "n"
                Key(Keys.N, False, 1)
            Case "o"
                Key(Keys.O, False, 1)
            Case "p"
                Key(Keys.P, False, 1)
            Case "q"
                Key(Keys.Q, False, 1)
            Case "r"
                Key(Keys.R, False, 1)
            Case "s"
                Key(Keys.S, False, 1)
            Case "t"
                Key(Keys.T, False, 1)
            Case "u"
                Key(Keys.U, False, 1)
            Case "v"
                Key(Keys.V, False, 1)
            Case "w"
                Key(Keys.W, False, 1)
            Case "x"
                Key(Keys.X, False, 1)
            Case "y"
                Key(Keys.Y, False, 1)
            Case "z"
                Key(Keys.Z, False, 1)

            Case "A"
                Key(Keys.A, True, 1)
            Case "B"
                Key(Keys.B, True, 1)
            Case "C"
                Key(Keys.C, True, 1)
            Case "D"
                Key(Keys.D, True, 1)
            Case "E"
                Key(Keys.E, True, 1)
            Case "F"
                Key(Keys.F, True, 1)
            Case "G"
                Key(Keys.G, True, 1)
            Case "H"
                Key(Keys.H, True, 1)
            Case "I"
                Key(Keys.I, True, 1)
            Case "J"
                Key(Keys.J, True, 1)
            Case "K"
                Key(Keys.K, True, 1)
            Case "L"
                Key(Keys.L, True, 1)
            Case "M"
                Key(Keys.M, True, 1)
            Case "N"
                Key(Keys.N, True, 1)
            Case "O"
                Key(Keys.O, True, 1)
            Case "P"
                Key(Keys.P, True, 1)
            Case "Q"
                Key(Keys.Q, True, 1)
            Case "R"
                Key(Keys.R, True, 1)
            Case "S"
                Key(Keys.S, True, 1)
            Case "T"
                Key(Keys.T, True, 1)
            Case "U"
                Key(Keys.U, True, 1)
            Case "V"
                Key(Keys.V, True, 1)
            Case "W"
                Key(Keys.W, True, 1)
            Case "X"
                Key(Keys.X, True, 1)
            Case "Y"
                Key(Keys.Y, True, 1)
            Case "Z"
                Key(Keys.Z, True, 1)

            Case vbLf
                If Not g_ignoreWhiteSpace Then Key(Keys.Enter, False, 1)
            Case vbTab
                If Not g_ignoreWhiteSpace Then Key(Keys.Tab, False, 1)
            Case " "
                If Not g_ignoreWhiteSpace Then Key(Keys.Space, False, 1)

            Case "?"
                Key(Keys.OemQuestion, True, 1)
            Case "/"
                Key(Keys.OemQuestion, False, 1)
            Case "~"
                Key(Keys.Oem3, True, 1)
            Case "`"
                Key(Keys.Oem3, False, 1)
            Case ")"
                Key(Keys.D0, True, 1)
            Case "("
                Key(Keys.D9, True, 1)
            Case "*"
                Key(Keys.D8, True, 1)
            Case "&"
                Key(Keys.D7, True, 1)
            Case "^"
                Key(Keys.D6, True, 1)
            Case "%"
                Key(Keys.D5, True, 1)
            Case "$"
                Key(Keys.D4, True, 1)
            Case "#"
                Key(Keys.D3, True, 1)
            Case "@"
                Key(Keys.D2, True, 1)
            Case "!"
                Key(Keys.D1, True, 1)

            Case "_"
                Key(Keys.OemMinus, True, 1)
            Case "-"
                Key(Keys.OemMinus, False, 1)
            Case "+"
                Key(Keys.Oemplus, True, 1)
            Case "="
                Key(Keys.Oemplus, False, 1)
            Case "{"
                Key(Keys.OemOpenBrackets, True, 1)
            Case "["
                Key(Keys.OemOpenBrackets, False, 1)
            Case "}"
                Key(Keys.OemCloseBrackets, True, 1)
            Case "]"
                Key(Keys.OemCloseBrackets, False, 1)
            Case "|"
                Key(Keys.OemBackslash, True, 1)
            Case "\"
                Key(Keys.OemBackslash, False, 1)
            Case ":"
                Key(Keys.OemSemicolon, True, 1)
            Case ";"
                Key(Keys.OemSemicolon, False, 1)
            Case """"
                Key(Keys.OemQuotes, True, 1)
            Case "'"
                Key(Keys.OemQuotes, False, 1)
            Case "<"
                Key(Keys.Oemcomma, True, 1)
            Case ","
                Key(Keys.Oemcomma, False, 1)
            Case ">"
                Key(Keys.OemPeriod, True, 1)
            Case "."
                Key(Keys.OemPeriod, False, 1)
            Case ws_
                g_ignoreWhiteSpace = True
            Case _ws
                g_ignoreWhiteSpace = False

            Case p_
                'Console.WriteLine("indexof «:" & g_s.IndexOf(p_) + 1)
                'Console.WriteLine("indexof »:" & g_s.IndexOf(_p) - 2)

                Dim middle As String = g_s.Substring(g_s.IndexOf(p_) + 1, g_s.IndexOf(_p) + 1 - g_s.IndexOf(p_) - 2) 'grab middle value
                'Console.WriteLine("middle: " + middle)


                If middle.Contains(":") Then 'grab x:#
                    g_n = middle.Substring(middle.IndexOf(":") + 1)
                    middle = middle.Substring(middle.IndexOf(p_) + 1, middle.IndexOf(":"))
                End If
                If middle.Contains("*") Then 'grab x*#
                    g_presses = middle.Substring(middle.IndexOf("*") + 1)
                    middle = middle.Substring(middle.IndexOf(p_) + 1, middle.IndexOf("*"))
                    'Console.WriteLine("middle: " + middle)
                    'Console.WriteLine("g_presses: " + g_presses)
                End If


                g_s = Microsoft.VisualBasic.Right(g_s, g_s.Length - g_s.IndexOf(_p) - 1) 'make new string
                'Console.WriteLine("new string: " & g_s)

                Select Case middle
                    Case "x"
                        SetCursorPos(CInt(g_n) + Cursor.Position.X, Cursor.Position.Y)
                    Case "y"
                        SetCursorPos(Cursor.Position.X, CInt(g_n) + Cursor.Position.Y)
                    Case "date"
                        Dim d As String = (Date.Now.Month.ToString & "/" & Date.Now.Day.ToString & "/" & Date.Now.Year.ToString)
                        If g_n <> "0" Then
                            d = (Date.Now.Month.ToString & "/" & Date.Now.Day.ToString & "/" & Date.Now.Year.ToString)
                            d = Replace(d, "/", g_n)
                        End If
                        If middle = "Date" Then
                            SendKeys.Send(d)
                        Else
                            Clipboard.SetText(d)
                        End If
                    Case "time"
                        Dim h As String = Date.Now.Hour.ToString
                        Dim hh As Integer = CInt(h)
                        Dim m As String = "AM"
                        If CInt(h) > 12 Then m = "PM" : hh -= 12
                        Dim t As String = hh & ":" & Date.Now.Minute.ToString & ":" & Date.Now.Second.ToString & ":" & m
                        If g_n <> "0" Then t = Replace(t, ":", g_n)
                        Clipboard.SetText(t)
                        If middle = "Time" Then
                            SendKeys.Send(t)
                        Else
                            Clipboard.SetText(t)
                        End If
                    Case "to"
                        Timeout(CInt(g_n))
                    Case "replace"
                        Clipboard.SetText(Clipboard.GetText.Replace(CType(Split(g_n, "|").GetValue(0), String), CType(Split(g_n, "|").GetValue(1), String)))
                    Case "yesno"
                        Dim yn = MsgBox(g_n, vbYesNo, "Verify")
                        If yn = vbYes Then Else g_s = "" : Exit Sub
                    Case "audio"
                        My.Computer.Audio.Play(g_n)
                    Case "Audio"
                        My.Computer.Audio.Play(g_n, AudioPlayMode.WaitToComplete)
                    Case "stop-audio"
                        My.Computer.Audio.Stop()
                    Case "" '«:»
                        SendKeys.Send(g_n)
                    Case "<<"
                        SendKeys.Send(p_) 'print open bracket
                    Case ">>"
                        SendKeys.Send(_p)
                    Case "iw"
                        g_ignoreWhiteSpace = True
                    Case "-iw"
                        g_ignoreWhiteSpace = False
                    Case "cb"
                        Clipboard.SetText(g_n)
                    Case "minute"
                        SleepMinutes(CInt(g_n))
                    Case "sleep"
                        SleepMS(CInt(g_n))
                    Case "Sleep"
                        Sleep(CInt(g_n))
                    Case ","
                        If g_n <> "0" Then
                            If g_n <> "" Then Sleep(CInt(g_n))
                        Else
                            Sleep(77)
                        End If
                    Case "App"
                        AppActivate(g_n)
                    Case "app"
                        Sleep(1)
                        Dim x As Integer = 0
App:
                        Try
                            x += 1
                            AppActivate(g_n)
                        Catch ex As Exception
                            If x = 200 Or CBool(GetAsyncKeyState(Keys.Escape)) Then
                                MsgBox(p_ & "app:" & g_n & _p & " " & " not found", vbExclamation)
                                g_kb_i = -1
                                g_s = ""
                                Exit Sub
                            End If
                            Sleep(77)
                            GoTo App
                        End Try
                    Case "win"
                        KeyHold(Keys.LWin)
                    Case "-win"
                        KeyRelease(Keys.LWin)
                    Case "shift"
                        KeyHold(Keys.LShiftKey)
                    Case "-shift"
                        KeyRelease(Keys.LShiftKey)
                        Keybd_event(Keys.RShiftKey, 0, 2, 0)
                    Case "alt"
                        Keybd_event(Keys.LMenu, 0, 0, 0)
                    Case "-alt"
                        Keybd_event(Keys.LMenu, 0, 2, 0)
                    Case "ctrl"
                        Keybd_event(Keys.LControlKey, 0, 0, 0)
                    Case "-ctrl"
                        KeyRelease(Keys.LControlKey)
                        Keybd_event(Keys.LControlKey, 0, 2, 0)
                    Case "up"
                        Key(Keys.Up, False, CInt(g_presses))
                    Case "right"
                        Key(Keys.Right, False, CInt(g_presses))
                    Case "down"
                        Key(Keys.Down, False, CInt(g_presses))
                    Case "left"
                        Key(Keys.Left, False, CInt(g_presses))
                    Case "tab"
                        Key(Keys.Tab, False, CInt(g_presses))
                    Case "space"
                        Key(Keys.Space, False, CInt(g_presses))
                    Case "menu"
                        Key(93, False, 1)
                    Case "enter"
                        Key(Keys.Enter, False, CInt(g_presses))
                    Case "bs", "backspace"
                        Key(Keys.Back, False, CInt(g_presses))
                    Case "esc", "escape"
                        Key(Keys.Escape, False, 1)
                    Case "home"
                        Key(Keys.Home, False, CInt(g_presses))
                    Case "end"
                        Key(Keys.End, False, CInt(g_presses))
                    Case "pu"
                        Key(Keys.PageUp, False, CInt(g_presses))
                    Case "pd"
                        Key(Keys.PageDown, False, CInt(g_presses))
                    Case "insert"
                        Key(Keys.Insert, False, CInt(g_presses))
                    Case "delete"
                        Key(Keys.Delete, False, CInt(g_presses))

                    Case "f1"
                        Key(Keys.F1, False, CInt(g_presses))
                    Case "f2"
                        Key(Keys.F2, False, CInt(g_presses))
                    Case "f3"
                        Key(Keys.F3, False, CInt(g_presses))
                    Case "f4"
                        Key(Keys.F4, False, CInt(g_presses))
                    Case "f5"
                        Key(Keys.F5, False, CInt(g_presses))
                    Case "f6"
                        Key(Keys.F6, False, CInt(g_presses))
                    Case "f7"
                        Key(Keys.F7, False, CInt(g_presses))
                    Case "f8"
                        Key(Keys.F8, False, CInt(g_presses))
                    Case "f9"
                        Key(Keys.F9, False, CInt(g_presses))
                    Case "f10"
                        Key(Keys.F10, False, CInt(g_presses))
                    Case "f11"
                        Key(Keys.F11, False, CInt(g_presses))
                    Case "f12"
                        Key(Keys.F12, False, CInt(g_presses))

                    Case "pause", "break"
                        Key(Keys.Pause, False, CInt(g_presses))
                    Case "ps", "printscreen"
                        Key(Keys.PrintScreen, False, CInt(g_presses))
                    Case "vu"
                        Key(Keys.VolumeUp, False, CInt(g_presses))
                    Case "vd"
                        Key(Keys.VolumeDown, False, CInt(g_presses))
                    Case "vm"
                        Key(Keys.VolumeMute, False, 1)

                    Case "caps"
                        Key(Keys.CapsLock, False, CInt(g_presses))

                    Case "nl"
                        Key(Keys.NumLock, False, CInt(g_presses))
                    Case "sl"
                        Key(Keys.Scroll, False, CInt(g_presses))

                    Case "MediaStop"
                        Key(Keys.MediaStop, False, 1)
                    Case "MediaPlayPause"
                        Key(Keys.MediaPlayPause, False, 1)
                    Case "MediaNextTrack"
                        Key(Keys.MediaNextTrack, False, 1)
                    Case "MediaPreviousTrack"
                        Key(Keys.MediaPreviousTrack, False, 1)
                    Case "SelectMedia"
                        Key(Keys.SelectMedia, False, 1)

                    Case "xy"
                        SetCursorPos(CType(Split(g_n, "-").GetValue(0), Integer), CType(Split(g_n, "-").GetValue(1), Integer))
                    Case "rp", "rm" 'return pointer
                        SetCursorPos(g_x, g_y)

                    Case "lc"
                        LeftClick()
                    Case "lh"
                        LeftHold()
                    Case "lr"
                        LeftRelease()
                    Case "mc"
                        MiddleClick()
                    Case "mh"
                        MiddleHold()
                    Case "mr"
                        MiddleRelease()
                    Case "rc"
                        RightClick()
                    Case "rh"
                        RightHold()
                    Case "rr"
                        RightRelease()

                    Case "lb"
                        Key(Keys.LButton, False, CInt(g_presses))
                    Case "rb"
                        Key(Keys.RButton, False, CInt(g_presses))
                    Case "mb"
                        Key(Keys.MButton, False, CInt(g_presses))


                    Case "n0"
                        Key(Keys.NumPad0, False, 1)
                    Case "n1"
                        Key(Keys.NumPad2, False, 1)
                    Case "n2"
                        Key(Keys.NumPad2, False, 1)
                    Case "n3"
                        Key(Keys.NumPad3, False, 1)
                    Case "n4"
                        Key(Keys.NumPad4, False, 1)
                    Case "n5"
                        Key(Keys.NumPad5, False, 1)
                    Case "n6"
                        Key(Keys.NumPad6, False, 1)
                    Case "n7"
                        Key(Keys.NumPad7, False, 1)
                    Case "n8"
                        Key(Keys.NumPad8, False, 1)
                    Case "n9"
                        Key(Keys.NumPad9, False, 1)



                    Case Else
                        'connect
                        If middle.StartsWith("'") Then Exit Select
                        For i = 0 To ar.Count - 1
                            If CBool(GetAsyncKeyState(Keys.Escape)) Then Exit For
                            If Split(ar(i).ToString, ":").GetValue(1).ToString.Contains(middle) Then
                                g_i = CInt(Split(ar(i).ToString, ":").GetValue(0))
                                g_s = ListBox1.Items(g_i).ToString.Substring(ListBox1.Items(g_i).ToString.IndexOf(_p) + 1, ListBox1.Items(g_i).ToString.Length - ListBox1.Items(g_i).ToString.IndexOf(_p) - 1) & g_s
                                'Console.WriteLine("connect: " & Split(ar(i).ToString, ":").GetValue(1).ToString)
                                If My.Settings.SettingInfiniteLoop = False Then
                                    If g_s.Contains(p_ & g_code & _p) Or g_s.Contains(middle) And Split(ar(i).ToString, ":").GetValue(1).ToString <> middle Or g_code = middle And g_s.Length = 0 Then
                                        MsgBox("Infinite loop" & vbNewLine & p_ & g_code & _p & " >" & g_s, vbExclamation)
                                        g_kb_i = -1
                                        g_s = ""
                                        Exit Sub
                                    End If
                                End If
                                PD()
                                g_s = ""
                                Exit For
                                'Console.WriteLine("get value: " & Split(ar(i).ToString, ":").GetValue(1))
                                'Console.WriteLine(Split("get index: " & ar(i).ToString, ":").GetValue(0))
                            End If
                        Next
                End Select
                g_kb_i = -1 'update to 0
                g_presses = "1"
                g_n = "0"
                middle = ""
            Case Else
                SendKeys.Send(c)
        End Select

        '        Exit Sub
        'Err:
        '        MsgBox(Err.Description & vbNewLine & Err.Number, vbExclamation, "Error")
    End Sub

    Sub PD()
        'Console.WriteLine(vbNewLine & "#####start#####")
        'Console.WriteLine("string: " & g_s)
        If g_s.Contains(p_ & "rp" & _p) Or g_s.Contains(p_ & "rm" & _p) Then g_x = MousePosition.X : g_y = MousePosition.Y
        If My.Settings.SettingSendkeysOnlyMode Then
            SendKeys.Send(g_s)
        Else
            For g_kb_i = 0 To g_s.Length
                If CBool(GetAsyncKeyState(Keys.Escape)) Then Exit For 'stop
                If g_kb_i >= g_s.Length Then Exit For
                'Console.WriteLine("print: " & g_s(g_kb_i))
                Kb(g_s(g_kb_i))
            Next
        End If
        '$repeat
        If ListBox1.Items.Count > 0 Then
            g_s = Nothing
            If ListBox1.SelectedItem.ToString.StartsWith(p_) Then
                g_s = ListBox1.SelectedItem.ToString.Substring(ListBox1.SelectedItem.ToString.IndexOf(_p) + 1, ListBox1.SelectedItem.ToString.Length - ListBox1.SelectedItem.ToString.IndexOf(_p) - 1)
            Else
                g_s = ListBox1.SelectedItem.ToString
            End If
        End If
        'Console.WriteLine("#####finish#####" & vbNewLine)
    End Sub

    'global g_
    Dim g_presses As String = "1" 'default press | «up»
    Dim g_n As String = "0" 'number | *# or :#
    Dim g_s As String = "" 'string | «code-» GlobalString
    Dim p_ As String = My.Settings.SettingBracketOpen '«
    Dim _p As String = My.Settings.SettingBracketClose '»
    Dim ws_ As String = My.Settings.SettingIgnoreWhiteSpaceOpen '‹
    Dim _ws As String = My.Settings.SettingIgnoreWhiteSpaceClose '›

    Dim g_ignoreWhiteSpace As Boolean = False
    Dim g_x As Integer, g_y As Integer
    Dim ar As New ArrayList
    Dim g_code As String

    Sub Sk(opt As Integer)
        TextBox2.Text = "'"
        ListBox1.SelectedItem() = ListBox1.Items.Item(g_i)

        'Console.WriteLine("code: " & ListBox1.SelectedItem.ToString.Substring(1, ListBox1.SelectedItem.ToString.IndexOf(_p) - 1)) 'code
        'Console.WriteLine("string: " & ListBox1.SelectedItem.ToString.Substring(ListBox1.SelectedItem.ToString.IndexOf(_p) + 1, ListBox1.SelectedItem.ToString.Length - ListBox1.SelectedItem.ToString.IndexOf(_p) - 1))
        g_s = ListBox1.SelectedItem.ToString.Substring(ListBox1.SelectedItem.ToString.IndexOf(_p) + 1, ListBox1.SelectedItem.ToString.Length - ListBox1.SelectedItem.ToString.IndexOf(_p) - 1)
        Select Case opt
            Case 1 '«code»
                g_code = ListBox1.SelectedItem.ToString.Substring(1, ListBox1.SelectedItem.ToString.IndexOf(_p) - 1)
                PD()
            Case 2 '«code-»
                g_code = ListBox1.SelectedItem.ToString.Substring(1, ListBox1.SelectedItem.ToString.IndexOf(_p) - 1)
                Key(Keys.Back, False, (g_code.Replace(My.Settings.SettingInsertSymbol, "").Replace("-", "").Length)) 'auto bs*#
                PD()
            Case 3 'code | g_length code
                g_s = Microsoft.VisualBasic.Right(ListBox1.SelectedItem.ToString, ListBox1.SelectedItem.ToString.Length - g_length)
                If My.Settings.SettingSendkeysOnlyMode Then
                    SendKeys.Send(g_s)
                Else
                    PD()
                End If
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
            CleanSelect()
        End If
        If e.KeyChar = ChrW(21) Then   'ctrl + u 
            UpdateDbItm()
            CleanSelect()
        End If
        If CBool(GetAsyncKeyState(Keys.LControlKey)) And CBool(GetAsyncKeyState(Keys.X)) Then
            Clipboard.SetText(ListBox1.Text)
            Key(Keys.Delete, False, 1)
            CleanSelect()
        End If
        If CBool(GetAsyncKeyState(Keys.LControlKey)) And CBool(GetAsyncKeyState(Keys.C)) Then
            Clipboard.SetText(ListBox1.Text)
            CleanSelect()
        End If
        If CBool(GetAsyncKeyState(Keys.LControlKey)) And CBool(GetAsyncKeyState(Keys.V)) And Clipboard.GetText > "" And ListBox1.Items.Count > 0 Then
            ListBox1.Items.Insert(ListBox1.SelectedIndex, Clipboard.GetText)
            My.Settings.SettingDB.Insert(ListBox1.SelectedIndex - 1, Clipboard.GetText)
            CleanSelect()
            ListBox1.SelectedIndex = ListBox1.SelectedIndex - 1
            LoadArray()
        End If
    End Sub

    Private Sub SplitContainer1_MouseDown(sender As Object, e As MouseEventArgs) Handles SplitContainer1.MouseDown
        If MouseButtons = Windows.Forms.MouseButtons.Right And ListBox1.Items.Count > 0 Then
            'select top or bottom item
            If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then
                ListBox1.SelectedItem = ListBox1.Items.Item(0)
            Else
                ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            End If
        End If
    End Sub

    Private Sub ListBox1_MouseWheel(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseWheel
        'ctrl + mouse wheel adjust listbox font size
        If CBool(GetAsyncKeyState(Keys.LControlKey)) Or CBool(GetAsyncKeyState(Keys.RControlKey)) Then
            If e.Delta > 1 Then
                Me.ListBox1.Font = New System.Drawing.Font(TextBox1.Font.Name, ListBox1.Font.Size + +1)
            End If
            If e.Delta < 1 Then
                If ListBox1.Font.Size <= 0.25 Then ListBox1.Font = New System.Drawing.Font(TextBox1.Font.Name, 8.25) 'reset
                Me.ListBox1.Font = New System.Drawing.Font(TextBox1.Font.Name, ListBox1.Font.Size + -1) '- 
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If CBool(GetAsyncKeyState(Keys.LControlKey)) And CBool(GetAsyncKeyState(Keys.F)) Then
            If ListBox1.SelectedIndex = -1 Then If ListBox1.Items.Count > 0 Then ListBox1.SelectedIndex = 0 Else Exit Sub
            If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then ListBox1.SelectedIndex = 0 : Exit Sub
            If ListBox1.SelectedItem.ToString.Contains(LCase(TextBox1.Text)) Then ListBox1.SelectedIndex += 1
            For i = ListBox1.SelectedIndex To ListBox1.Items.Count - 1
                If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then ListBox1.SelectedIndex = 0 : Exit Sub
                If ListBox1.Items(i).ToString.Contains(LCase(TextBox1.Text)) Then ListBox1.SelectedIndex = i : Exit Sub
                If i = ListBox1.Items.Count - 1 Then ListBox1.SelectedIndex = 0 : Exit Sub
            Next
        End If

        If CBool(GetAsyncKeyState(Keys.F4)) Then TextClear()

        'move cursor home or end
        If CBool(GetAsyncKeyState(Keys.Down)) And TextBox1.Text > "" Then 'end
            Dim getLastLineNumb = TextBox1.GetLineFromCharIndex(TextBox1.SelectionStart + TextBox1.TextLength) + 1
            Dim getLineNumb As Integer = TextBox1.GetLineFromCharIndex(TextBox1.SelectionStart) + 1
            If getLineNumb = getLastLineNumb Then TextBox1.SelectionStart = TextBox1.TextLength : Exit Sub 'Bottom
        End If
        If CBool(GetAsyncKeyState(Keys.Up)) And TextBox1.Text > "" Then 'home
            Dim getLineNumb As Integer = TextBox1.GetLineFromCharIndex(TextBox1.SelectionStart) + 1
            If getLineNumb = 1 Then TextBox1.SelectionStart = 0 : Exit Sub 'Bottom
        End If
    End Sub

    Sub FixedSize()
        If Me.ControlBox = True Then
            Me.FormBorderStyle = FormBorderStyle.None
            Me.ControlBox = False
            Me.Text = ""
        Else
            Me.ControlBox = True
            Me.FormBorderStyle = FormBorderStyle.Sizable
            Me.Text = My.Settings.SettingTitleText
            If My.Settings.SettingIcon > "" Then Me.Icon = New Icon(My.Settings.SettingIcon) Else Me.Icon = Me.Icon
        End If
        If My.Settings.SettingBackgroundImage > "" Then Me.BackgroundImage = Image.FromFile(My.Settings.SettingBackgroundImage)
        If Me.SplitContainer1.Visible = True Then
            If My.Settings.SettingBackgroundImage > "" Then
                Me.BackColor = Color.GhostWhite
                Me.SplitContainer1.Visible = False
            End If
        Else
            If My.Settings.SettingDarkMode = True Then Me.BackColor = Color.Black Else Me.BackColor = Nothing
            Me.SplitContainer1.Visible = True
        End If
    End Sub

    Private Sub PD_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        GetAsyncKeyState(Keys.LControlKey)
        If My.Settings.SettingBackgroundImage > "" Then FixedSize()
        If CBool(GetAsyncKeyState(Keys.LControlKey)) Then Me.CenterToScreen()
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If TextBox1.Text.StartsWith("'") Then 'off/on
            If Timer1.Enabled = True Then Me.Text = My.Settings.SettingTitleText & " > Off" : Timer1.Enabled = False
        Else
            If Timer1.Enabled = False Then ClearAllKeys() : TextBox2.Clear() : Me.Text = My.Settings.SettingTitleText : Timer1.Enabled = True
        End If
    End Sub
End Class