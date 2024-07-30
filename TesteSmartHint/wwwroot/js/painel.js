document.getElementById('selectAll').addEventListener('change', function () {
    const checkboxes = document.querySelectorAll('.selectBloqueio');
    checkboxes.forEach(checkbox => {
        checkbox.checked = this.checked;
    });
});

document.getElementById('deleteSelected').addEventListener('click', function () {
    const selectedIds = Array.from(document.querySelectorAll('.selectBloqueio'))
        .map(checkbox => ({
            id: parseInt(checkbox.getAttribute('data-id')),
            isBlocked: checkbox.checked
        }));

    if (selectedIds.length > 0) {
        if (confirm('Tem certeza de que deseja aplicar as alterações de bloqueio/desbloqueio nos clientes selecionados?')) {
            const form = document.getElementById('updateForm');
            const token = form.querySelector('input[name="__RequestVerificationToken"]').value;

            const formData = new FormData();
            selectedIds.forEach(item => {
                formData.append('ids', item.id);
                formData.append('isBlocked', item.isBlocked);
            });

            fetch('@Url.Action("AplicarAlteracoesBloqueio")', {
                method: 'POST',
                headers: {
                    'RequestVerificationToken': token
                },
                body: formData
            }).then(response => {
                if (response.ok) {
                    window.location.reload();
                } else {
                    alert('Ocorreu um erro ao tentar aplicar as alterações nos clientes selecionados.');
                }
            });
        }
    } else {
        alert('Nenhum cliente selecionado.');
    }
});