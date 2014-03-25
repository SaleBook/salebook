<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ShopSite.Master" Inherits="System.Web.Mvc.ViewPage<SBBL.Dto.Modules.Shop.EShopDelivery>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delivery
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript" src='<%= ResolveUrl("~/Scripts/Modules/Shop/ShopDeliveryScript.js") %>' ></script>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        InitData();
    });
</script>

<% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       { %>
    <div style='float: left; width: 250px'>
        <table>
            <tr>
                <td>
                    ประเภทการส่งสินค้า
                </td>
            </tr>
            <tr>
                <td>
                   <%: Html.DropDownListFor(x => x.deliveryID, new SelectList(Model.DeliveryList, "deliveryID", "deliveryName"))%>
                     <div>
                        <%: Html.ValidationMessageFor(x => x.deliveryID)%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    จำนวน(>,=)
                </td>
            </tr>
            <tr>
                <td>
                    <%: Html.TextBoxFor(x => x.moreQty)%>
                     <div>
                        <%: Html.ValidationMessageFor(x => x.moreQty)%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    ราคา
                </td>
            </tr>
            <tr>
                <td>
                    <%: Html.TextBoxFor(x => x.price) %>
                    <div>
                        <%: Html.ValidationMessageFor(x => x.price)%>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan='3'>
                    <input type="submit" value='เพิ่มข้อมูล' />
                </td>
            </tr>
        </table>
    </div>
        <% } %>
    <div style='float: left; width: 700px; padding-left: 10px'>
        <table class="tb-simple">
            <thead>
                <tr>
                    <th>
                        ประเภทการส่งสินค้า
                    </th>
                    <th>
                        จำนวน(>,=)
                    </th>
                    <th>
                        ราคา
                    </th>
                    <th width='30px'>
                    </th>
                </tr>
            </thead>
            <% foreach (SBBL.Dto.Modules.Shop.EShopDelivery obj in Model.ShopDeliveryList)
               { %>
            <tr>
                <td>
                    <%: obj.deliveryName %>
                </td>
                <td align="right" >
                    <%: obj.moreQty.Value.ToString("N0") %>
                </td>
                <td align="right" >
                   <%: obj.price.Value.ToString("N2") %>
                </td>
                <td>
                    <input type="image" id='btnDelete' src='../Content/image/delete.png' onclick='btnDelete_onclick("<%: obj.deliveryID %>", "<%: obj.moreQty %>", this)' />
                </td>
            </tr>
            <% } %>
        </table>
    </div>
    <div style='clear: both'>
    </div>


</asp:Content>
