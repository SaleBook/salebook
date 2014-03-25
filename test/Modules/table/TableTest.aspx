<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableTest.aspx.cs" Inherits="test.Modules.table.TableTest" MasterPageFile="~/Site.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <link href="../../Styles/table/simple.css" rel="stylesheet" type="text/css" />
     
    <%-- <script src='<%= ResolveUrl("~/Scripts/menu/modernizr.custom.79639.js")%>' type="text/javascript"></script>--%>
     
     
        <br />
					<div>
                        <table class="bordered">
    <thead>

    <tr>
        <th>#</th>        
        <th>IMDB Top 10 Movies</th>
        <th>Year</th>
    </tr>
    </thead>
    <tr>
        <td>1</td>        
        <td>The Shawshank Redemption</td>

        <td>1994</td>
    </tr>        
    <tr>
        <td>2</td>         
        <td>The Godfather</td>
        <td>1972</td>
    </tr>
    <tr>

        <td>3</td>         
        <td>The Godfather: Part II</td>
        <td>1974</td>
    </tr>    
    <tr>
        <td>4</td> 
        <td>The Good, the Bad and the Ugly</td>
        <td>1966</td>

    </tr>
    <tr>
        <td>5</td> 
        <td>Pulp Fiction</td>
        <td>1994</td>
    </tr>
    <tr>
        <td>6</td> 
        <td>12 Angry Men</td>

        <td>1957</td>
    </tr>
    <tr>
        <td>7</td> 
        <td>Schindler's List</td>
        <td>1993</td>
    </tr>    
    <tr>

        <td>8</td> 
        <td>One Flew Over the Cuckoo's Nest</td>
        <td>1975</td>
    </tr>
    <tr>
        <td>9</td> 
        <td>The Dark Knight</td>

        <td>2008</td>
    </tr>
    <tr>
        <td>10</td> 
        <td>The Lord of the Rings: The Return of the King</td>
        <td>2003</td>
    </tr> 

</table>
					</div>
        
     
</asp:Content>
