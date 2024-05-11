// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(loadAddressCard());
function loadAddressCard() {
    $('#addressSelector').change(function () {
        var addressId = $(this).val();
        if (addressId != 0) {
            $.ajax({
                url: '/Address/GetAddressCard/' + addressId,
                type: 'GET',
                success: function (result) {
                    $('#partialViewCont').html(result);
                }
            });
        }
        else {
            $('#partialViewCont').html("");
        }
    });
}    

document.getElementById("btnSubmit").addEventListener("click", function () {
    let selectedAddress = document.getElementById("addressSelector");    
    let selectedPayment = document.querySelector("input[type=radio]:checked");    
    if (selectedAddress.value == 0) {
        document.getElementById("validateAddress").innerText = "Vui lòng chọn địa chỉ nhận hàng";
    }
    else if (selectedPayment == null) {
        document.getElementById("validatePayment").innerText = "Vui lòng chọn phương thức thanh toán";
    }
    else {
        let orderDate = new Date();

        let data = {
            addressId: selectedAddress.value,
            payment: selectedPayment.value,
            orderDate: orderDate
        };
        console.log(data);
        fetch("/Order/ProcessOrder", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })
            .then(function (response) {
                if (response.ok) {
                    alert("Đặt thành công");                    
                    window.location.href = "/Order/Index";
                } else {                    
                    throw new Error("Error sending data to controller")
                }
            })
            .catch(function (error) {
                alert("Có lỗi khi thực hiện đặt đơn hàng");
                console.log("An error occurs: ", error)
            })
        
    }
})