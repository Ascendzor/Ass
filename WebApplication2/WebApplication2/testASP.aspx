<%@ Page Language="C#" %>
<%

string connectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\Public\\brogramming\\Ass\\WebApplication2\\WebApplication2\\Database\\Database1.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";


string q;
q = Request.QueryString["q"];
Response.Write(q);
%>