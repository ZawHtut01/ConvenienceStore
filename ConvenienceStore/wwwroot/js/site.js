(function () {
    'use strict'

    // Form validation
    const forms = document.querySelectorAll('.needs-validation')
    Array.prototype.slice.call(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }
            form.classList.add('was-validated')
        }, false)
    })

    // Stock selection handlers
    const itemSelect = document.getElementById('itemId');
    const quantityInput = document.getElementById('quantity');
    const availableStockValue = document.getElementById('availableStockValue');

    if (itemSelect) {
        itemSelect.addEventListener('change', function () {
            const selectedOption = this.options[this.selectedIndex];
            const availableStock = selectedOption.getAttribute('data-stock');

            if (availableStock) {
                availableStockValue.textContent = availableStock;
                quantityInput.max = availableStock;
                quantityInput.placeholder = `Max ${availableStock}`;
            }
        });
    }

    // Quick stock out buttons
    document.querySelectorAll('.quick-stock-out').forEach(button => {
        button.addEventListener('click', function (e) {
            e.preventDefault();
            const itemId = this.getAttribute('data-item-id');
            const itemName = this.getAttribute('data-item-name');

            if (itemSelect) {
                itemSelect.value = itemId;
                availableStockValue.textContent = this.closest('.card').querySelector('.fw-bold').textContent;

                const event = new Event('change');
                itemSelect.dispatchEvent(event);
                quantityInput.focus();

                const toast = new bootstrap.Toast(document.getElementById('quickSelectToast'));
                document.getElementById('selectedItemName').textContent = itemName;
                toast.show();
            }
        });
    });
})();