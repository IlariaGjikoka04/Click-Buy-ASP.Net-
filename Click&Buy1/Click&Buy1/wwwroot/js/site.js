function loadCartItems() {
    fetch("/Home/GetCartItems")
        .then((response) => response.json())
        .then((cartItems) => {
            const cartContainer = document.getElementById("cartItems");
            cartContainer.innerHTML = "";

            if (cartItems.length === 0) {
                cartContainer.innerHTML = `<p class="text-center">Your basket is still empty, discover everything we've got for you</p>`;
            } else {
                cartItems.forEach((item) => {
                    cartContainer.innerHTML += `
                        <div class="d-flex align-items-center justify-content-between mb-3">
                            <div class="d-flex align-items-center">
                                <img src="${item.imageUrl}" alt="${item.name}" style="width: 60px; height: 60px; object-fit: cover;">
                                <div class="ms-3 text-start">
                                    <h6>${item.name}</h6>
                                    <p class="mb-0">$${item.price} x ${item.quantity}</p>
                                </div>
                            </div>
                        </div>
                    `;
                });
                attachRemoveEventListeners();
            }
        })
        .catch((error) => console.error("Error loading cart items:", error));
}

document.addEventListener("DOMContentLoaded", function () {
    const heartIcons = document.querySelectorAll(".heart-icon");

    heartIcons.forEach((icon) => {
        icon.addEventListener("click", function () {
            const productName = this.getAttribute("data-name");
            const productPrice = this.getAttribute("data-price");
            const productImage = this.getAttribute("data-image");

            fetch("/Home/AddToCart", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    Name: productName,
                    Price: parseFloat(productPrice),
                    ImageUrl: productImage,
                    CategoryId: 1
                }),
            })
                .then((response) => response.json())
                .then((data) => {
                    if (data.success) {
                        alert(data.message);
                        loadCartItems();
                    } else {
                        alert(data.message);
                    }
                })
                .catch((error) => {
                    console.error("Error adding to cart:", error);
                });
        });
    });

    document.getElementById("cartOffcanvas").addEventListener("shown.bs.offcanvas", loadCartItems);
});



$(document).ready(function () {
    $('#payment-link').on('click', function (e) {
        e.preventDefault(); 

        $.ajax({
            url: '/Home/Payment', // URL-ja e faqes që dëshiron të hapësh
            type: 'GET',
            success: function (response) {
                // Krijon një dritare të re dhe vendos përmbajtjen brenda saj
                var newWindow = window.open('', '_blank');
                newWindow.document.write(response);
                newWindow.document.close();
            },
            error: function () {
                alert('An error occurred while loading the page.');
            }
        });
    });
});

function submitOrder() {
    var email = document.getElementById("emailInput").value;
    if (!email) {
        alert("Please enter your email.");
        return;
    }

    fetch('/Checkout/CompleteOrder', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email: email })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert("Order placed successfully!");
                window.location.href = "/";
            } else {
                alert("Error: " + data.message);
            }
        })
        .catch(error => console.error("Error:", error));
}
