$('#BrandID').focus();
 
$('#purchaseMaster_TaxableAmt').val($("#PurchaseTot").val());
$('#SalesMaster_TaxableAmt').val($("#SalesTot").val());
$('#QuotationMaster_TaxableAmt').val($("#QuotationTot").val());
$('#quotationMaster_TaxableAmt').val($("#IQuotationTot").val());
$('#IsalesMaster_TaxableAmt').val($("#ISalesTot").val());

$('#quotationMaster_IQuotationNo').val($("#IQuoIdd").val());
 
cal();
Grand2();

    


$('.Rate').on("change", function () {
    ItemRate();
});
function ItemRate() {
    var A = $('.Rate').val();
    var B = $('#SCGST').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SCGSTRate').val(C);
    var A = $('.Rate').val();
    var B = $('#SSGST').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SSGSTRate').val(C);
    var A = $('.Rate').val();
    var B = $('#SIGST').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SIGSTRate').val(C);
};

$('#SCGST').on("change", function () {

    var A = $('.Rate').val();
    var B = $('#SCGST').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SCGSTRate').val(C);
    ItemRate();

});

$('#SSGST').on("change", function () {
    var A = $('.Rate').val();
    var B = $('#SSGST').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SSGSTRate').val(C);
    ItemRate();
});

$('#SIGST').on("change", function () {
    var A = $('.Rate').val();
    var B = $('#SIGST').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SIGSTRate').val(C);
    ItemRate();
});


$('#SizeW').on("change", function () {
    var SizeW = $('#SizeW').val();
    var SizeH = $('#SizeH').val();
    if (SizeW != "" && SizeH != "") {
        var Tot = Number(SizeW) * Number(SizeH);
        $('#TotSize').val(Tot);
    }
    var Qty = $('#Qty').val();
    var Rate = $('#Rate').val();
    if (Qty != "" && Rate != "") {
        var Tot = Number(Qty) * Number(Rate);
        $('#Amount').val(Tot);
    }
    var TotSize = $('#TotSize').val();
    if (TotSize != "" && Qty != "" && Rate != "") {
        var Tot2 = Number(TotSize) * Number(Qty) * Number(Rate);
        var Totsqft = Number(TotSize) * Number(Qty);

        $('#Amount').val(Tot2);

        $('#TotSqFt').val(Totsqft);
    }
});

$('#SizeH').on("change", function () {
    var SizeW = $('#SizeW').val();
    var SizeH = $('#SizeH').val();
    if (SizeW != "" && SizeH != "") {
        var Tot = Number(SizeW) * Number(SizeH);
        $('#TotSize').val(Tot);
    }
    var Qty = $('#Qty').val();
    var Rate = $('#Rate').val();
    if (Qty != "" && Rate != "") {
        var Tot = Number(Qty) * Number(Rate);
        $('#Amount').val(Tot);
    }
    var TotSize = $('#TotSize').val();
    if (TotSize != "" && Qty != "" && Rate != "") {
        var Tot2 = Number(TotSize) * Number(Qty) * Number(Rate);
        var Totsqft = Number(TotSize) * Number(Qty);

        $('#Amount').val(Tot2);

        $('#TotSqFt').val(Totsqft);
    }
});






$('#Qty').on("change", function () {
    var Qty = $('#Qty').val();
    var Rate = $('#Rate').val();
    if (Qty != "" && Rate != "") {
        var Tot = Number(Qty) * Number(Rate);
        $('#Amount').val(Tot);
    }
    var TotSize = $('#TotSize').val();
    if (TotSize != "" && Qty != "" && Rate!="")
    {
        var Tot2 = Number(TotSize) * Number(Qty) * Number(Rate);
        var Totsqft = Number(TotSize) * Number(Qty);

        $('#Amount').val(Tot2);

        $('#TotSqFt').val(Totsqft);
    }
});



$('#Rate').on("change", function () {
        var Qty = $('#Qty').val();
        var Rate = $('#Rate').val();
        if (Qty != "" && Rate != "")
        {
            var Tot = Number(Qty) * Number(Rate);
            $('#Amount').val(Tot);
        }
        var TotSize = $('#TotSize').val();
        if (TotSize != "" && Qty != "" && Rate != "") {
            var Tot2 = Number(TotSize) * Number(Qty) * Number(Rate);
            var Totsqft = Number(TotSize) * Number(Qty);

            $('#Amount').val(Tot2);
           
            $('#TotSqFt').val(Totsqft);
        }
    });

$("#BrandID").on("change", function () {


$.ajax({
    type: "GET",
    url: "/CasCading/getCategory",
    data: { BrandID: $("#BrandID").val()},
    success: function (data) {
        var s = '<option value="-1">--SELECT--</option>';
        for (var i = 0; i < data.length; i++) {
            s += '<option value="' + data[i].CategoryID + '">' + data[i].CategoryName + '</option>';
        }
        $("#CategoryID").html(s);
    }
    });  


});

$("#CategoryID").on("change", function () {


    $.ajax({
        type: "GET",
        url: "/CasCading/getItem",
        data: { BrandID: $("#BrandID").val(), CategoryID: $("#CategoryID").val() },
        success: function (data) {
            var s = '<option value="-1">--SELECT--</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ItemId + '">' + data[i].Description + '</option>';
            }
            $("#ItemID").html(s);
        }
    });


});
$("#ItemID").on("change", function () { 
    $.ajax({
        type: "GET",
        url: "/CasCading/GetItemRate",
        data: {  ID: $("#ItemID").val() },
        success: function (data) {
           
            $("#Rate").val(data);
        }
    }); 
});





//Purchase
$('#purchaseMaster_CGSTAmt').on("change", function () {
     
    var A = $('#purchaseMaster_TaxableAmt').val();
    var B = $('#purchaseMaster_CGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#purchaseMaster_CGSTRate').val(C);
    Grand();
     
});

$('#purchaseMaster_SGSTAmt').on("change", function () {
    var A = $('#purchaseMaster_TaxableAmt').val();
    var B = $('#purchaseMaster_SGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#purchaseMaster_SGSTRate').val(C);
    Grand();
});

$('#purchaseMaster_IGSTAmt').on("change", function () {
    var A = $('#purchaseMaster_TaxableAmt').val();
    var B = $('#purchaseMaster_IGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#purchaseMaster_IGSTRate').val(C);
    Grand();
});

function Grand() {
    var a = $('#purchaseMaster_TaxableAmt').val();
    var b = $('#purchaseMaster_CGSTRate').val();
    var c = $('#purchaseMaster_SGSTRate').val();
    var d = $('#purchaseMaster_IGSTRate').val();
    if (b == "") { b = 0; }
    if (c == "") { c = 0; }
    if (d == "") { d = 0; }
    var e = Number(b) + Number(c) + Number(d);
    var f = Number(a) + Number(e); 
    $('#purchaseMaster_TotalGST').val(e);
    $('#purchaseMaster_GrandTotal').val(f); 
};

//SalesSalesMaster_CGSTAmt
$('#SalesMaster_CGSTAmt').on("change", function () {

    var A = $('#SalesMaster_TaxableAmt').val();
    var B = $('#SalesMaster_CGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SalesMaster_CGSTRate').val(C);
    Grand1();

});

$('#SalesMaster_SGSTAmt').on("change", function () {
    var A = $('#SalesMaster_TaxableAmt').val();
    var B = $('#SalesMaster_SGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SalesMaster_SGSTRate').val(C);
    Grand1();
});

$('#SalesMaster_IGSTAmt').on("change", function () {
    var A = $('#SalesMaster_TaxableAmt').val();
    var B = $('#SalesMaster_IGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#SalesMaster_IGSTRate').val(C);
    Grand1();
});

function Grand1() {
    var a = $('#SalesMaster_TaxableAmt').val();
    var b = $('#SalesMaster_CGSTRate').val();
    var c = $('#SalesMaster_SGSTRate').val();
    var d = $('#SalesMaster_IGSTRate').val();
    if (b == "") { b = 0; }
    if (c == "") { c = 0; }
    if (d == "") { d = 0; }
    var e = Number(b) + Number(c) + Number(d);
    var f = Number(a) + Number(e);
    $('#SalesMaster_TotalGST').val(e);
    $('#SalesMaster_GrandTotal').val(f);
};



//QuotationMaster_CGSTAmt
$('#QuotationMaster_CGSTAmt').on("change", function () {

    var A = $('#QuotationMaster_TaxableAmt').val();
    var B = $('#QuotationMaster_CGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#QuotationMaster_CGSTRate').val(C);
    Grand1();

});

$('#QuotationMaster_SGSTAmt').on("change", function () {
    var A = $('#QuotationMaster_TaxableAmt').val();
    var B = $('#QuotationMaster_SGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#QuotationMaster_SGSTRate').val(C);
    Grand1();
});

$('#QuotationMaster_IGSTAmt').on("change", function () {
    var A = $('#QuotationMaster_TaxableAmt').val();
    var B = $('#QuotationMaster_IGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#QuotationMaster_IGSTRate').val(C);
    Grand1();
});

function Grand1() {
    var a = $('#QuotationMaster_TaxableAmt').val();
    var b = $('#QuotationMaster_CGSTRate').val();
    var c = $('#QuotationMaster_SGSTRate').val();
    var d = $('#QuotationMaster_IGSTRate').val();
    if (b == "") { b = 0; }
    if (c == "") { c = 0; }
    if (d == "") { d = 0; }
    var e = Number(b) + Number(c) + Number(d);
    var f = Number(a) + Number(e);
    $('#QuotationMaster_TotalGST').val(e);
    $('#QuotationMaster_GrandTotal').val(f);
};
//quotationMaster_TaxableAmt
//IQuotationMaster_CGSTAmt
$('#quotationMaster_CGSTAmt').on("change", function () {

    var A = $('#quotationMaster_TaxableAmt').val();
    var B = $('#quotationMaster_CGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#quotationMaster_CGSTRate').val(C);
    Grand2();

});

$('#quotationMaster_SGSTAmt').on("change", function () {
    var A = $('#quotationMaster_TaxableAmt').val();
    var B = $('#quotationMaster_SGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#quotationMaster_SGSTRate').val(C);
    Grand2();
});

$('#quotationMaster_IGSTAmt').on("change", function () {
    var A = $('#quotationMaster_TaxableAmt').val();
    var B = $('#quotationMaster_IGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#quotationMaster_IGSTRate').val(C);
    Grand2();
});

function Grand2() {
    var a = $('#quotationMaster_TaxableAmt').val();
    var b = $('#quotationMaster_CGSTRate').val();
    var c = $('#quotationMaster_SGSTRate').val();
    var d = $('#quotationMaster_IGSTRate').val();
    if (b == "") { b = 0; }
    if (c == "") { c = 0; }
    if (d == "") { d = 0; }
    var e = Number(b) + Number(c) + Number(d);
    var f = Number(a) + Number(e);
    $('#quotationMaster_TotalGST').val(e);
    $('#quotationMaster_GrandTotal').val(f);
};


//SizeW SizeH TotSize


function cal()
{
    var A = $('#quotationMaster_TaxableAmt').val();
    var B = $('#quotationMaster_CGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#quotationMaster_CGSTRate').val(C);
    Grand2();
    var A = $('#quotationMaster_TaxableAmt').val();
    var B = $('#quotationMaster_SGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#quotationMaster_SGSTRate').val(C);
    Grand2();
    var A = $('#quotationMaster_TaxableAmt').val();
    var B = $('#quotationMaster_IGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#quotationMaster_IGSTRate').val(C);
    Grand2();
};




//IQuotationMaster_CGSTAmt
$('#IsalesMaster_CGSTAmt').on("change", function () {

    var A = $('#IsalesMaster_TaxableAmt').val();
    var B = $('#IsalesMaster_CGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#IsalesMaster_CGSTRate').val(C);
    Grand3();

});

$('#IsalesMaster_SGSTAmt').on("change", function () {
    var A = $('#IsalesMaster_TaxableAmt').val();
    var B = $('#IsalesMaster_SGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#IsalesMaster_SGSTRate').val(C);
    Grand3();
});

$('#IsalesMaster_IGSTAmt').on("change", function () {
    var A = $('#IsalesMaster_TaxableAmt').val();
    var B = $('#IsalesMaster_IGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#IsalesMaster_IGSTRate').val(C);
    Grand3();
});

function Grand3() {
    var a = $('#IsalesMaster_TaxableAmt').val();
    var b = $('#IsalesMaster_CGSTRate').val();
    var c = $('#IsalesMaster_SGSTRate').val();
    var d = $('#IsalesMaster_IGSTRate').val();
    if (b == "") { b = 0; }
    if (c == "") { c = 0; }
    if (d == "") { d = 0; }
    var e = Number(b) + Number(c) + Number(d);
    var f = Number(a) + Number(e);
    $('#IsalesMaster_TotalGST').val(e);
    $('#IsalesMaster_GrandTotal').val(f);
};


//SizeW SizeH TotSize


function cal3() {
    var A = $('#IsalesMaster_TaxableAmt').val();
    var B = $('#IsalesMaster_CGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#IsalesMaster_CGSTRate').val(C);
    Grand2();
    var A = $('#IsalesMaster_TaxableAmt').val();
    var B = $('#IsalesMaster_SGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#IsalesMaster_SGSTRate').val(C);
    Grand2();
    var A = $('#IsalesMaster_TaxableAmt').val();
    var B = $('#IsalesMaster_IGSTAmt').val();
    var C = (Number(A) * Number(B)) / 100;
    $('#IsalesMaster_IGSTRate').val(C);
    Grand2();
    var a = $('#IsalesMaster_TaxableAmt').val();
    var b = $('#IsalesMaster_CGSTRate').val();
    var c = $('#IsalesMaster_SGSTRate').val();
    var d = $('#IsalesMaster_IGSTRate').val();
    if (b == "") { b = 0; }
    if (c == "") { c = 0; }
    if (d == "") { d = 0; }
    var e = Number(b) + Number(c) + Number(d);
    var f = Number(a) + Number(e);
    $('#IsalesMaster_TotalGST').val(e);
    $('#IsalesMaster_GrandTotal').val(f);
};

$('.CashReceived').on("change", function () {

    alert();
});