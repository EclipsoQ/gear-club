var cartBtns = Array.from(document.querySelectorAll(".add-to-cart-btn"));
cartBtns.forEach(function (btn) {
    btn.addEventListener('click', function (event) {
        event.preventDefault();
        var index = cartBtns.indexOf(btn);
        var productId = document.querySelectorAll(".product-id")[index].value;
        var data = {
            Quantity: 1,
            ProductId: productId
        }
        console.log(data);

        fetch("/Cart/AddToCart", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })
            .then(function (response) {
                if (response.ok) {
                    alert("Thêm thành công");
                } else {
                    throw new Error("Error sending data to controller")
                }
            })
            .catch(function (error) {
                console.log("An error occurs: ", error)
            })
    });
})


    