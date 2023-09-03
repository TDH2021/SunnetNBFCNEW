Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module SQLScriptNetModule
    Class ConnectionData
        Public ServerName As String
        Public DatabaseName As String
        Public UserID As String
        Public Password As String
        Public sProd As String = My.Application.Info.ProductName

        Public Function CheckConnection() As Boolean
            Dim oConn As New SqlConnection
            strConn = "UID=" & UserID & ";Password=" & Password & ";Database=" & DatabaseName & ";Server=" & ServerName & ";"
            Try
                oConn.ConnectionString = strConn
                oConn.Open()
                oConn.Close()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Sub SaveToRegistry()
            SaveSetting(sProd, "Setup", "Server", ServerName)
            SaveSetting(sProd, "Setup", "Database", DatabaseName)
            SaveSetting(sProd, "Setup", "UserID", UserID)
            SaveSetting(sProd, "Setup", "Password", Password)
        End Sub

        Public Sub GetFromRegistry()
            ServerName = GetSetting(sProd, "Setup", "Server", "(local)")
            DatabaseName = GetSetting(sProd, "Setup", "Database", "UNKNOWN")
            UserID = GetSetting(sProd, "Setup", "UserID", "sa")
            Password = GetSetting(sProd, "Setup", "Password", "")
        End Sub
    End Class

    Public strConn As String
    Public cnnCom As SqlConnection
    Public cdConn As ConnectionData

    Public Function OpenConnection() As Boolean
        cnnCom = New SqlConnection
        cnnCom.ConnectionString = strConn
        Try
            cnnCom.Open()
            Return True
        Catch ex As Exception
            MsgBox("Connection Failed!", MsgBoxStyle.Critical)
            Return False
        End Try
    End Function

    Public Sub CloseConnection()
        If cnnCom Is Nothing AndAlso cnnCom.State = ConnectionState.Open Then
            cnnCom.Close()
            cnnCom = Nothing
        End If
    End Sub

    Public Function GetTableList() As ArrayList
        If Not OpenConnection() Then Return Nothing
        Dim arrReturn As New ArrayList
        Dim oDA As New SqlDataAdapter("SELECT * FROM sysobjects WHERE (xtype = 'U') AND ([name]<>'dtproperties') ORDER BY [name]", cnnCom)
        Dim oDS As New DataSet
        oDA.Fill(oDS)
        CloseConnection()
        Dim oDR As DataRow
        If oDS.Tables(0).Rows.Count > 0 Then
            For Each oDR In oDS.Tables(0).Rows
                arrReturn.Add(oDR("name"))
            Next
            Return arrReturn
        Else
            Return Nothing
        End If
    End Function

    Public Function GetFieldList(ByVal sTable As String) As ArrayList
        If Not OpenConnection() Then Return Nothing
        Dim arrReturn As New ArrayList
        Dim oDA As New SqlDataAdapter("SELECT * FROM " & sTable, cnnCom)
        Dim oDS As New DataSet
        oDA.Fill(oDS)
        CloseConnection()
        Dim oDC As DataColumn
        For Each oDC In oDS.Tables(0).Columns
            arrReturn.Add(oDC.ColumnName)
        Next
        Return arrReturn
    End Function

    Public Function GetSelectTag(ByVal sTable As String, Optional ByVal bFieldOnly As Boolean = False) As String
        If Not OpenConnection() Then Return Nothing
        Dim oDA As New SqlDataAdapter("SELECT * FROM " & sTable, cnnCom)
        Dim oDS As New DataSet
        oDA.Fill(oDS, sTable)
        CloseConnection()
        Dim oDCC As DataColumnCollection
        oDCC = oDS.Tables(sTable).Columns
        Dim sResult As String = ""
        Dim oDC As DataColumn
        If Not bFieldOnly Then sResult = "SELECT "
        For Each oDC In oDCC
            sResult = sResult & oDC.ColumnName & ", "
        Next
        sResult = Left(sResult, Len(sResult) - 2)
        If Not bFieldOnly Then sResult = sResult & " FROM " & sTable
        Return sResult
    End Function

    Public Function GetInsertTag(ByVal sTable As String) As String
        If Not OpenConnection() Then Return Nothing
        Dim oDA As New SqlDataAdapter("SELECT * FROM " & sTable, cnnCom)
        Dim oDS As New DataSet
        oDA.Fill(oDS, sTable)
        Dim oDCC As DataColumnCollection
        oDCC = oDS.Tables(sTable).Columns
        Dim sResult As String = "", sValue As String = ""
        Dim oDC As DataColumn
        Dim sColName As String
        sResult = "INSERT INTO " & sTable & "("
        sValue = " VALUES ("
        For Each oDC In oDCC
            sColName = oDC.ColumnName
            If Not oDC.AutoIncrement() Then
                If UCase(sColName) <> "ID" Then
                    sResult = sResult & sColName & ", "
                    sValue = sValue & "@" & sColName & ", "
                End If
            End If
        Next
        sResult = Left(sResult, Len(sResult) - 2) & ")"
        sValue = Left(sValue, Len(sValue) - 2) & ")"
        sResult = sResult & sValue
        CloseConnection()
        Return sResult
    End Function

    Public Function GetUpdateTag(ByVal sTable As String, Optional ByVal nKey As Integer = 1) As String
        If Not OpenConnection() Then Return Nothing
        Dim oDA As New SqlDataAdapter("SELECT * FROM " & sTable, cnnCom)
        Dim oDS As New DataSet
        Dim sColName As String
        oDA.Fill(oDS, sTable)
        Dim oDCC As DataColumnCollection
        oDCC = oDS.Tables(sTable).Columns
        Dim sResult As String = "", sWhere As String = ""
        Dim oDC As DataColumn
        sResult = "UPDATE " & sTable & " SET "
        For Each oDC In oDCC
            sColName = oDC.ColumnName
            If oDC.Ordinal >= nKey Then
                If UCase(sColName) <> "ID" Then
                    sResult = sResult & sColName & " = @" & sColName & ", "
                End If
            Else
                sWhere = sWhere & sColName & " = @" & sColName & " " & IIf(oDC.Ordinal < nKey - 1, "AND ", "")
            End If
        Next
        sResult = Left(sResult, Len(sResult) - 2) & IIf(Len(sWhere) > 0, " WHERE " & sWhere, "")
        CloseConnection()
        Return sResult
    End Function

    Public Function GetDeleteTag(ByVal sTable As String) As String
        If Not OpenConnection() Then Return Nothing
        Dim oDA As New SqlDataAdapter("SELECT * FROM " & sTable, cnnCom)
        Dim oDS As New DataSet
        oDA.Fill(oDS, sTable)
        Dim oDCC As DataColumnCollection
        oDCC = oDS.Tables(sTable).Columns
        Dim sResult As String = "", sWhere As String = ""
        Dim oDC As DataColumn
        sResult = "DELETE FROM " & sTable & " "
        oDC = oDCC(0)
        sWhere = sWhere & oDC.ColumnName & " = @" & oDC.ColumnName
        sResult &= " WHERE " & sWhere
        CloseConnection()
        Return sResult
    End Function

End Module


