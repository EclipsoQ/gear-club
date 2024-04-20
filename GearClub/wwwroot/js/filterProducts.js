document.getElementById("btnFilter").addEventListener("click", function () {
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
})

function getSelectedCheckboxes(formId) {

    var form = document.getElementById(formId);
    var checkboxes = form.querySelectorAll('input[type=checkbox]:checked');
    var values = []
    checkboxes.forEach(function (checkbox) {
        values.push(checkbox.value);
    });
    return values;
}