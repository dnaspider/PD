﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.6.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<ArrayOfString xmlns:xsi=""http://www.w3."& _ 
            "org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" />")>  _
        Public Property SettingDB() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("SettingDB"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("SettingDB") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("3")>  _
        Public Property SettingCodeLength() As Integer
            Get
                Return CType(Me("SettingCodeLength"),Integer)
            End Get
            Set
                Me("SettingCodeLength") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("163")>  _
        Public Property SettingSpecialKey() As Integer
            Get
                Return CType(Me("SettingSpecialKey"),Integer)
            End Get
            Set
                Me("SettingSpecialKey") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property SettingTitleTip() As Boolean
            Get
                Return CType(Me("SettingTitleTip"),Boolean)
            End Get
            Set
                Me("SettingTitleTip") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SettingBracketModeOnlyScan() As Boolean
            Get
                Return CType(Me("SettingBracketModeOnlyScan"),Boolean)
            End Get
            Set
                Me("SettingBracketModeOnlyScan") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("150")>  _
        Public Property SettingInterval() As Integer
            Get
                Return CType(Me("SettingInterval"),Integer)
            End Get
            Set
                Me("SettingInterval") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property SettingDarkMode() As Boolean
            Get
                Return CType(Me("SettingDarkMode"),Boolean)
            End Get
            Set
                Me("SettingDarkMode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Lime")>  _
        Public Property SettingDarkModeText() As Global.System.Drawing.Color
            Get
                Return CType(Me("SettingDarkModeText"),Global.System.Drawing.Color)
            End Get
            Set
                Me("SettingDarkModeText") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0.9")>  _
        Public Property SettingOpacity() As Double
            Get
                Return CType(Me("SettingOpacity"),Double)
            End Get
            Set
                Me("SettingOpacity") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SettingTopMost() As Boolean
            Get
                Return CType(Me("SettingTopMost"),Boolean)
            End Get
            Set
                Me("SettingTopMost") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("268")>  _
        Public Property SettingHeight() As Integer
            Get
                Return CType(Me("SettingHeight"),Integer)
            End Get
            Set
                Me("SettingHeight") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("300")>  _
        Public Property SettingWidth() As Integer
            Get
                Return CType(Me("SettingWidth"),Integer)
            End Get
            Set
                Me("SettingWidth") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("118")>  _
        Public Property SettingLocationTop() As Integer
            Get
                Return CType(Me("SettingLocationTop"),Integer)
            End Get
            Set
                Me("SettingLocationTop") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("78")>  _
        Public Property SettingLocationLeft() As Integer
            Get
                Return CType(Me("SettingLocationLeft"),Integer)
            End Get
            Set
                Me("SettingLocationLeft") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SettingTextBox() As String
            Get
                Return CType(Me("SettingTextBox"),String)
            End Get
            Set
                Me("SettingTextBox") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1")>  _
        Public Property SettingListboxSelectedIndex() As Integer
            Get
                Return CType(Me("SettingListboxSelectedIndex"),Integer)
            End Get
            Set
                Me("SettingListboxSelectedIndex") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property SettingTabIndex() As Integer
            Get
                Return CType(Me("SettingTabIndex"),Integer)
            End Get
            Set
                Me("SettingTabIndex") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property SettingSelectionStart() As Integer
            Get
                Return CType(Me("SettingSelectionStart"),Integer)
            End Get
            Set
                Me("SettingSelectionStart") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property SettingSelectionLength() As Integer
            Get
                Return CType(Me("SettingSelectionLength"),Integer)
            End Get
            Set
                Me("SettingSelectionLength") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property SettingTextBoxZoomFactor() As Single
            Get
                Return CType(Me("SettingTextBoxZoomFactor"),Single)
            End Get
            Set
                Me("SettingTextBoxZoomFactor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8.25")>  _
        Public Property SettingListBoxFontSize() As Single
            Get
                Return CType(Me("SettingListBoxFontSize"),Single)
            End Get
            Set
                Me("SettingListBoxFontSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SettingSendkeysOnlyMode() As Boolean
            Get
                Return CType(Me("SettingSendkeysOnlyMode"),Boolean)
            End Get
            Set
                Me("SettingSendkeysOnlyMode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("«")>  _
        Public Property SettingBracketOpen() As String
            Get
                Return CType(Me("SettingBracketOpen"),String)
            End Get
            Set
                Me("SettingBracketOpen") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("»")>  _
        Public Property SettingBracketClose() As String
            Get
                Return CType(Me("SettingBracketClose"),String)
            End Get
            Set
                Me("SettingBracketClose") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("95")>  _
        Public Property SettingSplitterDistance() As Integer
            Get
                Return CType(Me("SettingSplitterDistance"),Integer)
            End Get
            Set
                Me("SettingSplitterDistance") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("15")>  _
        Public Property SettingSplitterWidth() As Integer
            Get
                Return CType(Me("SettingSplitterWidth"),Integer)
            End Get
            Set
                Me("SettingSplitterWidth") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SettingBackgroundImage() As String
            Get
                Return CType(Me("SettingBackgroundImage"),String)
            End Get
            Set
                Me("SettingBackgroundImage") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property SettingFirstLoad() As Integer
            Get
                Return CType(Me("SettingFirstLoad"),Integer)
            End Get
            Set
                Me("SettingFirstLoad") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SettingInfiniteLoop() As Boolean
            Get
                Return CType(Me("SettingInfiniteLoop"),Boolean)
            End Get
            Set
                Me("SettingInfiniteLoop") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SettingIcon() As String
            Get
                Return CType(Me("SettingIcon"),String)
            End Get
            Set
                Me("SettingIcon") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SettingWordWrap() As Boolean
            Get
                Return CType(Me("SettingWordWrap"),Boolean)
            End Get
            Set
                Me("SettingWordWrap") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("PD")>  _
        Public Property SettingTitleText() As String
            Get
                Return CType(Me("SettingTitleText"),String)
            End Get
            Set
                Me("SettingTitleText") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.pd.My.MySettings
            Get
                Return Global.pd.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
