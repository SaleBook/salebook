<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ShopSite.Master" Inherits="System.Web.Mvc.ViewPage<SBBL.Dto.Modules.Shop.EShopBank>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bank
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript" src='<%= ResolveUrl("~/Scripts/Modules/Shop/ShopBankScript.js")%>' ></script>

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
                    ธนคาร
                </td>
            </tr>
            <tr>
                <td>
                    <select id="demo-htmlselect">
                        <% foreach (SBBL.Dto.Modules.Master.EBank obj in Model.bankList)
                           { %>
                        <option value="<%: obj.bankID %>" data-imagesrc="<%: ResolveUrl(obj.imageUrl) %>"
                            data-description=" ">
                            <%: obj.bankName%></option>
                        <% } %>
                    </select>
                    <input type="hidden" name="hidBankID" id="hidBankID" value="" />
                </td>
            </tr>
            <tr>
                <td>
                    สาขา
                </td>
            </tr>
            <tr>
                <td>
                    <%: Html.TextBoxFor(x => x.branch)%>
                </td>
            </tr>
            <tr>
                <td>
                    ชื่อ-นามสกุล
                </td>
            </tr>
            <tr>
                <td>
                    <%: Html.TextBoxFor(x => x.bookName) %>
                    <div>
                        <%: Html.ValidationMessageFor(x => x.bookName)%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Book No.
                </td>
            </tr>
            <tr>
                <td>
                    <%: Html.TextBoxFor(x => x.bookNo) %>
                    <div>
                        <%: Html.ValidationMessageFor(x => x.bookNo)%>
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
                        ธนาคาร
                    </th>
                    <th>
                        สาขา
                    </th>
                    <th>
                        ชื่อ - นามสกุล
                    </th>
                    <th>
                        Book No.
                    </th>
                    <th width='30px'>
                    </th>
                </tr>
            </thead>
            <% foreach (SBBL.Dto.Modules.Shop.EShopBank obj in Model.shopBankList)
               { %>
            <tr>
                <td>
                    <img src="<%: ResolveUrl(obj.imageUrl) %>" style='margin-right: 5px;vertical-align: middle;' />
                    <label style='display:inline-block' ><%: obj.bankName %></label>
                </td>
                <td>
                    <%: obj.branch %>
                </td>
                <td>
                    <%: obj.bookName %>
                </td>
                <td>
                    <%: obj.bookNo %>
                </td>
                <td>
                    <input type="image" id='btnDelete' src='../Content/image/delete.png' onclick='btnDelete_onclick("<%: obj.bankID %>", this)' />
                </td>
            </tr>
            <% } %>
        </table>
    </div>
    <div style='clear: both'>
    </div>
</asp:Content>
