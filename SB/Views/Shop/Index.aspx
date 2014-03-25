<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= ResolveUrl("~/Scripts/Modules/Shop/ShopScript.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/facebookUtil.js") %>" type="text/javascript"></script>
    
    <div>
        <fb:login-button show-faces="true" width="200" max-rows="1" scope="manage_pages, user_photos, friends_photos"></fb:login-button>
    </div>
    <div>
        <a href="#" id="linkLogout" style="font-size: small; display: none" onclick="javascript:SetLogout()">
            [logout] </a>
    </div>
    <br />
    <br />
    <div id='divPageList' >
    </div>
</asp:Content>
