var buttonPlus = Array.from(document.querySelectorAll(".btn-plus"));
buttonPlus.forEach(function (button) {
    button.addEventListener('click', function () {                
        var index = buttonPlus.indexOf(button);
        var lineToUpdate = document.querySelectorAll("[data-cart-detail-id]")[index];
        var id = lineToUpdate.getAttribute("data-cart-detail-id");        
        var data = {
            id: id,
            quantity: 1
        };
        
        fetch("/Cart/UpdateCartLine", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })          
            .then(function (response) {
                if (response.ok) {
                    location.reload();
                    return response.json();
                } else {
                    throw new Error("Error sending data to controller")
                }
            })
            .then(function (data) {                
                lineToUpdate.querySelector(".item-subtotal").innerText = data.subtotal.toLocaleString("en-US") + "₫";
            })
            .catch(function (error) {
                console.log("An error occurs: ", error)
            })
    })
});


var buttonMinus = Array.from(document.querySelectorAll(".btn-minus"));
buttonMinus.forEach(function (button) {
    window.onload = function () {
        var value = button.parentElement.nextElementSibling.value;       
        if (value == 1) {
            button.disabled = true;
        }
    }
    button.addEventListener('click', function () {       
        var index = buttonMinus.indexOf(button);         
        var lineToUpdate = document.querySelectorAll("[data-cart-detail-id]")[index];
        var id = lineToUpdate.getAttribute("data-cart-detail-id");
        var data = {
            id: id,
            quantity: -1
        };

        fetch("/Cart/UpdateCartLine", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })
            .then(function (response) {
                if (response.ok) {
                    location.reload();
                    return response.json();
                } else {
                    throw new Error("Error sending data to controller")
                }
            })
            /*.then(function (data) {
                lineToUpdate.querySelector(".item-subtotal").innerText = data.lineSubtotal.toLocaleString("en-US") + "₫";
                document.getElementById("subtotal-all").innerText = data.subtotals.toLocaleString("en-US") + "₫";
                document.getElementById("shipping-fee").innerText = data.shipping.toLocaleString("en-US") + "₫";
                document.getElementById("total").innerText = data.total.toLocaleString("en-US") + "₫";
            })*/
            .catch(function (error) {
                console.log("An error occurs: ", error)
            })
    })
});



var removeBtns = Array.from(document.querySelectorAll(".btn-remove"));
removeBtns.forEach(function (btn) {
    btn.addEventListener("click", function () {
        var lineToRemove = document.querySelectorAll(".item-row");
        var index = removeBtns.indexOf(btn);
        if (lineToRemove.length >= index) {
            var ids = document.querySelectorAll("[data-cart-detail-id]")[index];
            var id = ids.getAttribute("data-cart-detail-id");
            console.log(id);
            var data = {
                id: id
            };            

            fetch("/Cart/RemoveCartLine", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            })
                .then(function (response) {
                    if (response.ok) {
                        return response.text();
                    } else {
                        throw new Error("Error sending data to controller")
                    }
                })
                .catch(function (error) {
                    console.log("An error occurs: ", error)
                })            
            lineToRemove[index].remove();
        }
        else {
            console.log("No lines were remove");
        }
        
    });
});


