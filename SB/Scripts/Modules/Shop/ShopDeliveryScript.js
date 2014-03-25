function InitData() {

    var dd = new DropDown($('#menu'));
    dd.setValue("menuDelivery");
    dd.initEvents();

}

function btnDelete_onclick(deliveryID, moreQty, img) {
    if (confirm("คุณต้องการลบข้อมูลนี้ ?")) {
        $.post('Shop/DeleteShopDelivery', { deliveryID: deliveryID, moreQty: moreQty }, function (data) {
            window.location = data
        });
    }
}

