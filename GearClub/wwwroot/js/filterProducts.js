document.getElementById("price-asc").addEventListener("click", function () {
    var productCards = Array.from(document.querySelectorAll(".product-card"));    
    const productList = [];
    productCards.forEach(function (product) {        
        console.log(product);
        var id = product.querySelector(".product-id").value;
        productList.push(id);
    });
    console.log(productList);
});

var checkboxes = document.querySelectorAll("input[type=checkbox]");
document.getElementById("filterBtn").addEventListener("click", function (){
    var priceRange = getSelectedCheckboxes("price-form");
    var brands = getSelectedCheckboxes("brand-form");
    console.log(priceRange, brands);
    var data = {
        PriceRange: priceRange,
        Brand: brands

    }
    fetch("/Product/FilterProduct", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(function (response) {
            if (response.ok) {
                //return response.json();
                return response.text();
            } else {
                throw new Error("Error sending data to controller")
            }
        })
        .then(function (results) {
            //console.log(results);
            $("#product-view").html(results);
        })
        .catch(function (error) {
            console.log("An error occurs: ", error)
        })
})

/*document.getElementById("btnFilter").addEventListener("click", function () {
    var priceRange = getSelectedCheckboxes("price-filter-form");
    var brands = getSelectedCheckboxes("brand-filter-form");
    console.log(priceRange);
    console.log(brands);
    var data = {
        PriceRange: priceRange,
        Brand: brands

    }
    fetch("/Products/GetFilteredProducts", {
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
        .then(function (results) {
            $("#product-view").html(results);
        })
        .catch(function (error) {
            console.log("An error occurs: ", error)
        })
})*/

function getSelectedCheckboxes(formId) {

    var form = document.getElementById(formId);
    var checkboxes = form.querySelectorAll('input[type=checkbox]:checked');
    var values = []
    checkboxes.forEach(function (checkbox) {
        values.push(checkbox.value);
    });
    return values;
}