$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resultado = confirm("Tem certeza que deseja excluir esta Imagem?");

        if (resultado == false) {
            e.preventDefault();
        }
    });
    AjaxUploadImagemDocCliente();
});

function AjaxUploadImagemDocCliente() {
    $(".imgDoc-upload").click(function () {
        $(this).parent().parent().find(".input-fileDoc").click();
    });
    $(".btn-imagemDoc-excluir").click(function () {
        var CampoHidden = $(this).parent().find("input[name=imagemDoc]");
        var Imagem = $(this).parent().find(".imgDoc-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagemDoc-excluir");
        var InputFile = $(this).parent().find(".input-fileDoc");
        $.ajax({
            type: "GET",
            url: "/Colaborador/ImagemDocCliente/Deletar?caminho=" + CampoHidden.val(),
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

    $(".input-fileDoc").change(function () {
        //Formulário de dados via JavaScript
        var Binario = $(this)[0].files[0];
        var Formulario = new FormData();
        Formulario.append("fileDoc", Binario);

        var CampoHidden = $(this).parent().find("input[name=imagemDoc]");
        var ImagemDoc = $(this).parent().find(".imgDoc-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagemDoc-excluir");

        //Apresenta imagem loading
        ImagemDoc.attr("src", "/img/loading.gif");
        ImagemDoc.addClass("thumb");

        //TODO - Requisição Ajax enviando o Formulario
        $.ajax({
            type: "POST",
            url: "/Colaborador/ImagemDocCliente/Armazenar",
            data: Formulario,
            contentType: false,
            processData: false,
            error: function () {
                alert("Erro no envio do arquivo!");
                ImagemDoc.attr("src", "/img/padrao.png");
                ImagemDoc.removeClass("thumb");
            },
            success: function (data) {
                var caminho = data.caminho;
                ImagemDoc.attr("src", caminho);
                ImagemDoc.removeClass("thumb");
                CampoHidden.val(caminho);
                BtnExcluir.removeClass("btn-ocultar");
            }
        });
    });
}