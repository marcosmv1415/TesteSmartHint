$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resultado = confirm("Tem certeza que deseja excluir este registro?");

        if (resultado == false) {
            e.preventDefault();
        }
    });
    AjaxUploadImagemTemplate();
});

function AjaxUploadImagemTemplate() {
    $(".img-upload").click(function () {
        $(this).parent().parent().find(".input-file").click();
    });
    $(".btn-imagem-excluir").click(function () {
        var CampoHidden = $(this).parent().find("input[name=imagem]");
        var Imagem = $(this).parent().find(".img-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagem-excluir");
        var InputFile = $(this).parent().find(".input-file");
        $.ajax({
            type: "GET",
            url: "/Colaborador/ImagemTemplate/Deletar?caminho=" + CampoHidden.val(),
            error: function () {

            },
            success: function () {
                Imagem.attr("src", "/img/padrao.png");
                BtnExcluir.addClass("btn-ocultar");
                CampoHidden.val("");
                InputFile.val("");

            }
        });
    });


    $(".input-file").change(function () {
        //Formulário de dados via JavaScript
        var Binario = $(this)[0].files[0];
        var Formulario = new FormData();
        Formulario.append("file", Binario);

        var CampoHidden = $(this).parent().find("input[name=imagem]");
        var Imagem = $(this).parent().find(".img-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagem-excluir");

        //Apresenta imagem loading
        Imagem.attr("src", "/img/loading.gif");
        Imagem.addClass("thumb");

        //TODO - Requisição Ajax enviando o Formulario
        $.ajax({
            type: "POST",
            url: "/Colaborador/ImagemTemplate/Armazenar",
            data: Formulario,
            contentType: false,
            processData: false,
            error: function () {
                alert("Erro no envio do arquivo!");
                Imagem.attr("src", "/img/padrao.png");
                Imagem.removeClass("thumb");
            },
            success: function (data) {
                var caminho = data.caminho;
                Imagem.attr("src", caminho);
                Imagem.removeClass("thumb");
                CampoHidden.val(caminho);
                BtnExcluir.removeClass("btn-ocultar");
            }
        });
    });
}


