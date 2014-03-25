function InitData() {

    var dd = new DropDown($('#menu'));
    dd.setValue("menuBank");
    dd.initEvents();

    $('#demo-htmlselect').ddslick({ width: 200,
        onSelected: function (data) {
            $('#hidBankID').val(data.selectedData.value);
        }
    });

}

function btnDelete_onclick(bankID, img) {
    if (confirm("คุณต้องการลบข้อมูลนี้ ?")) {
        $.post('Shop/DeleteShopBank', { bankID: bankID }, function (data) {
            window.location = data
        });
    }
}

