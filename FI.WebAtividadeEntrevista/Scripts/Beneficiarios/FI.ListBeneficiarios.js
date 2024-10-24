function SalvarBeneficiario(data, nome, cpf, endpoint) {

        var url = endpoint;
        var vMethod = "";

        vMethod = endpoint.includes("Inserir") ? "POST" : "PUT";

        $.ajax({
            url: url,
            method: vMethod,
            contentType: "application/json",
            data: JSON.stringify({
                Nome: $(nome).val(),
                CPF: $(cpf).val(),
                IdCliente: data.IdCliente
            }),
            success:
                function (r) {
                    if (r.StatusCode == "400") {
                        $('#rowResult').attr("class", "alert alert-warning");
                        $('#rowResult').html("<strong>" + "CPF já cadastrado ou inválido!" + "</strong>")
                    } else if (r.StatusCode == "200") {
                        $('#rowResult').attr("class", "alert alert-success");
                        $('#rowResult').html("<strong>" + "Beneficiário salvo com sucesso!" + "</strong>")
                    }
                    $('#rowResult').fadeIn().delay(3000).fadeOut(); // Mostra e esconde a mensagem após 3 segundos
                    setTimeout(function () {
                        ModalBeneficiarios(data.IdCliente);
                    }, 3000);
                },
            error:
                function (r) {
                    if (r.StatusCode == "500") {
                        $('#rowResult').attr("class", "alert alert-danger");
                    } else if (r.StatusCode == "400") {
                        $('#rowResult').attr("class", "alert alert-warning");
                    }
                    $('#rowResult').html("<strong>" + "Ocorreu um erro no servidor!" + "</strong>")
                     // Mostra e esconde a mensagem após 3 segundos

                    setTimeout(function () {
                        $('#rowResult').fadeIn().delay(3000).fadeOut();
                        ModalBeneficiarios(data.IdCliente);
                    }, 3000);

                }
        });
    }
function AlterarBeneficiario(data, count) {
        var url = "/Beneficiario/Alterar/";
        var div1 = "#nomeBen-" + count;
        var div2 = "#cpfBen-" + count;
        var nome = "#inputNome-" + count;
        var cpf = "#inputCpf-" + count;
        var alterar = "#btnAlterar-" + count;
        $(div1).html("<input id='" + nome.replace("#", "") + "' class='form-control' type='text' maxlength='50' />");
        $(nome).val(data.nome);

        $(div2).html("<input id='" + cpf.replace("#", "") + "' class='form-control' type='text' maxlength='14' />");
        $(cpf).val(data.cpf);

        $(alterar).text("Salvar");

        $(alterar).attr("onclick", "SalvarBeneficiario(" + JSON.stringify(data) + ", '" + nome + "', '" + cpf + "', '" + url + "')");
        $(alterar).attr("class", "btn btn-success");


        MascaraCPF(cpf);
        //alert(count);
    }

function ExcluirBeneficiario(dados, count) {
    var url = "/Beneficiario/Excluir/";
    var div1 = "#nomeBen-" + count;
    var div2 = "#cpfBen-" + count;
    urlExclusao = urlExclusao + dados.id;
    //alert(urlExclusao);
    var excluir = "#divBtnExcluir";
    $("#rowResult").attr("class", "btn btn-success");
    $.ajax({
        url: urlExclusao,
        method: "DELETE",
        success:
            function (r) {
                if (r.StatusCode == "200") {
                    $('#rowResult').attr("class", "alert alert-success");
                    $('#rowResult').html("<strong>" + "Beneficiário excluído com sucesso!" + "</strong>")
                }
                $('#rowResult').fadeIn().delay(3000).fadeOut(); // Mostra e esconde a mensagem após 3 segundos
                setTimeout(function () {
                    ModalBeneficiarios(dados.IdCliente);
                }, 3000);
            },
        error:
            function (r) {
                if (r.StatusCode == "500") {
                    $('#rowResult').attr("class", "alert alert-danger");
                } else if (r.StatusCode == "400") {
                    $('#rowResult').attr("class", "alert alert-warning");
                }
                $('#rowResult').html("<strong>" + "Ocorreu um erro no servidor!" + "</strong>")
                // Mostra e esconde a mensagem após 3 segundos

                setTimeout(function () {
                    $('#rowResult').fadeIn().delay(3000).fadeOut();
                    ModalBeneficiarios(data.IdCliente);
                }, 3000);

            }
    });


    $("#divBtnExcluir").remove();

}

    function InserirBeneficiario() {
        console.log("Inserir Beneficiário");
        var nome = "#benNome";
        var vNome = document.getElementById("benNome").value;
        var cpf = "#benCpf";
        var vCpf = document.getElementById("benCpf").value;
        var id = document.getElementById("benId").value;
        var url = urlBase + "/Beneficiario/Inserir/";
        var data = { nome: vNome, IdCliente: id, cpf: vCpf };
        
        SalvarBeneficiario(data, nome, cpf, url);
}