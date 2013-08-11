<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>


<%

Dim myConnection As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Public\brogramming\Ass\WebApplication2\WebApplication2\Database\Database1.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True")

Dim insertCommand As New SqlDataAdapter("INSERT INTO Persons (PersonID, LastName, FirstName, Address, City) VALUES (7331, 'jack', 'johnson', 'kfc', 'added at runtime')", myConnection)
insertCommand.Fill(New DataSet())

Dim myCommand As New SqlDataAdapter("select * from Persons", myConnection)
Dim ds As New DataSet()
myCommand.Fill(ds)

For j = 0 To ds.Tables(0).Rows.Count-1
    For i = 0 To ds.Tables(0).Columns.Count-1
       Response.Write(ds.tables(0).rows(j)(i).ToString() + "   ")
    Next i
    Response.Write("       New line        ")
Next j

%>

<%-- Delete
Dim deleteCommand As New SqlDataAdapter("DELETE FROM Persons", myConnection)
deleteCommand.Fill(New DataSet())
--%>