<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ShopSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Product
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript" src='<%= ResolveUrl("~/Scripts/Modules/Shop/ProductScript.js")%>' ></script>
<script src="<%= ResolveUrl("~/Scripts/facebookUtil.js") %>" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        InitData();
    });
</script>
   <%--<div style='float:left;width:150px' >
        <div style='height:50px' >
            <input type="button" value='สินค้า' id='btnProduct' />
        </div>
        <div style='height:50px'>
            <input type="button" value='sync facebook' id='btnProductFacbook' />
        </div>
   </div>

   <div>
   </div>
   <div style='clear:both' >
   </div>--%>
   <div>
        <input type="button" value='sync facebook' id='btnProductFacbook' />
   </div>
   <div>
        
   </div>
   <div id='divSysfbDialog' class='popup-1' >
        <div id='divDSMain' style='height:300px'>
            <div style='height:30px' ><input type="checkbox" id='chkDSAll' />Selected All</div>
            <div id='divDSMain1' style='float:left;width:200px' ></div>
            <div id='divDSMain2' style='float:left;width:200px' ></div>
            <div id='divDSMain3' style='float:left;width:200px' ></div>
            <div style='clear:both' ></div>
        </div>
        <div style='text-align:right;' >
            <input type="button" value='sync' id='btnSDSync' />
            <input type="button" value='ยกเลิก' id='btnSDCancel' />
        </div>
   </div>
</asp:Content>
