document.addEventListener('DOMContentLoaded', function () {
    var tipoPessoaSelect = document.getElementById("TipoPessoa");
    var inscricaoEstadualInput = document.getElementById("InscricaoEstadual");
    var isentoCheck = document.getElementById("IsentoInscricaoEstadual");
    var cpfCnpjInput = document.getElementById("Cpf");
    var fisicaFields = document.getElementById("fisicaFields");
    var form = document.querySelector('form'); 

    function updateFields() {
        var tipoPessoa = tipoPessoaSelect.value;

        if (tipoPessoa === "Juridica") {
            inscricaoEstadualInput.disabled = false;
            isentoCheck.disabled = false;
            fisicaFields.style.display = "none";
            inscricaoEstadualInput.required = !isentoCheck.checked; 
        } else if (tipoPessoa === "Fisica") {
            inscricaoEstadualInput.disabled = true;
            isentoCheck.disabled = true;
            fisicaFields.style.display = "block";
            inscricaoEstadualInput.required = false; 
        } else {
            inscricaoEstadualInput.disabled = true;
            isentoCheck.disabled = true;
            fisicaFields.style.display = "none";
            inscricaoEstadualInput.required = false; 
        }
    }

    tipoPessoaSelect.addEventListener("change", updateFields);
    isentoCheck.addEventListener("change", function () {
        inscricaoEstadualInput.required = !this.checked && tipoPessoaSelect.value === "Juridica"; 
    });

    function formatCpfCnpj(value) {
        value = value.replace(/\D/g, "");

        if (value.length <= 11) { // CPF
            value = value.replace(/(\d{3})(\d{3})/, "$1.$2");
            value = value.replace(/(\d{3})(\d{3})/, "$1.$2.");
            value = value.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
        } else if (value.length <= 14) { // CNPJ
            value = value.replace(/^(\d{2})(\d{3})/, "$1.$2");
            value = value.replace(/\.(\d{3})(\d{3})/, ".$1.$2");
            value = value.replace(/\.(\d{3})(\d{4})/, ".$1/$2");
            value = value.replace(/\/(\d{4})(\d{1,2})$/, "/$1-$2");
        }

        return value;
    }

    function formatInscricaoEstadual(value) {
        value = value.replace(/\D/g, ""); 
        value = value.replace(/(\d{3})(\d{3})(\d{3})(\d{1,3})$/, "$1.$2.$3-$4");
        return value;
    }

    cpfCnpjInput.addEventListener("input", function () {
        this.value = formatCpfCnpj(this.value);
    });

    inscricaoEstadualInput.addEventListener("input", function () {
        this.value = formatInscricaoEstadual(this.value);
    });

    updateFields();


    form.addEventListener("submit", function (event) {
        if (inscricaoEstadualInput.required && !inscricaoEstadualInput.value) {
            event.preventDefault(); 
            alert("A Inscrição Estadual é obrigatória para Pessoas Jurídicas que não são isentas.");
        }
    });
});


document.addEventListener('DOMContentLoaded', function () {
    var senhaInput = document.getElementById("Senha");
    var confirmacaoSenhaInput = document.getElementById("ConfirmacaoSenha");
    var senhaError = document.getElementById("SenhaError");
    var confirmacaoSenhaError = document.getElementById("ConfirmacaoSenhaError");

    function validarSenhas() {
        var senha = senhaInput.value;
        var confirmacaoSenha = confirmacaoSenhaInput.value;

        if (senha !== confirmacaoSenha) {
            confirmacaoSenhaError.textContent = "As senhas não conferem.";
            confirmacaoSenhaInput.setCustomValidity("As senhas não conferem.");
        } else {
            confirmacaoSenhaError.textContent = "";
            confirmacaoSenhaInput.setCustomValidity("");
        }
    }

    senhaInput.addEventListener("input", validarSenhas);
    confirmacaoSenhaInput.addEventListener("input", validarSenhas);
});

//-----//
