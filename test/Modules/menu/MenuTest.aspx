<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuTest.aspx.cs" Inherits="test.Modules.menu.MenuTest" MasterPageFile="~/Site.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <link href="../../Styles/menu/style.css" rel="stylesheet" type="text/css" />
     
    <%-- <script src='<%= ResolveUrl("~/Scripts/menu/modernizr.custom.79639.js")%>' type="text/javascript"></script>--%>
     
     <script type="text/javascript">

         function DropDown(el) {
             this.dd = el;
             this.placeholder = this.dd.children('a');
             this.opts = this.dd.find('ul.dropdown > li');
             this.val = '';
             this.index = -1;
             this.initEvents();
         }

         DropDown.prototype = {
             initEvents: function () {
                 var obj = this;

                 obj.dd.on('click', function (event) {
                     $(this).toggleClass('active');
                     return false;
                 });

                 obj.opts.on('click', function () {
                     var opt = $(this);
                     obj.val = opt.html();
                     obj.index = opt.index();
                     obj.placeholder.html(obj.val);
                 });
             },
             getValue: function () {
                 return this.val;
             },
             getIndex: function () {
                 return this.index;
             }
         }

         $(function () {

             var dd = new DropDown($('#dd'));

             $(document).click(function () {
                 // all dropdowns
                 $('.wrapper-dropdown-5').removeClass('active');
             });
         });

		</script>
        <br />
					<div id="dd" class="wrapper-dropdown-5">
                        <a href="#"><span class='shop_icon'></span>รายการสั่งซื้อ</a>
						<ul class="dropdown">
                            <li><a href="#"><span class='shop_icon'></span>รายการสั่งซื้อ</a></li>
							<li><a href="#"><span class='bank_icon'></span>บัญชีธนคาร</a></li>
							<li><a href="#"><span class='delivery_icon'></span>ส่งสินค้า</a></li>
							<li><a href="#"><span class='product_icon'></span>สินค้า</a></li>
                            <li><a href="#"><span class='setting_icon'></span>ตั้งค่า</a></li>
						</ul>
					</div>
        
     
</asp:Content>
