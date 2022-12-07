#Region "Using"
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Web.SessionState
Imports System.ComponentModel
#End Region
Partial Public Class BatchUpdate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        'Populate grid with data on the first load
        If Not IsPostBack AndAlso Not IsCallback Then
            Dim provider As New InvoiceItemsProvider()
            provider.Populate()
            provider.Populate()
        End If
    End Sub
    Protected Sub grid_CustomDataCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs)
        Dim res() As String = e.Parameters.Split(":"c)
        If res.Length <> 2 Then
            Return
        End If
        Dim id As Integer = -1
        Dim value As Boolean = False
        If Not Integer.TryParse(res(0), id) Then
            Return
        End If
        If Not Boolean.TryParse(res(1), value) Then
            Return
        End If
        UpdateItem(id, value)
    End Sub
    Private Sub UpdateItem(ByVal id As Integer, ByVal value As Boolean)
        'Update your data here
        Dim provider As New InvoiceItemsProvider()
        Dim item As InvoiceItem = provider.GetItemById(id)
        If item IsNot Nothing Then
            item.IsPaid = value
        End If
    End Sub
    Protected Function GetCheckBoxChecked(ByVal value As Object) As String
        Return If(DirectCast(value, Boolean), "checked", "")
    End Function
End Class

