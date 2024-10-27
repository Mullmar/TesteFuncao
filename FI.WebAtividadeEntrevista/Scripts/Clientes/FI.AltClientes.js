var protocolo = window.location.protocol; // Verifica o protocolo atual (http ou https)
var urlBase = protocolo + "//" + window.location.host; // Constrói a URL base
var urlAlteracao = urlBase + urlAlterar + "/";
var urlExclusao = urlBase + "/Beneficiario/Excluir/"
$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Id').val(obj.Id);
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #CPF').val(obj.CPF);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
    }

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        console.log(urlBase);
        console.log(urlAlteracao);
        var dataJson = {
            "Id": $(this).find("#Id").val(),
            "Nome": $(this).find("#Nome").val(),
            "CEP": $(this).find("#CEP").val(),
            "CPF": $(this).find("#CPF").val(),
            "Email": $(this).find("#Email").val(),
            "Sobrenome": $(this).find("#Sobrenome").val(),
            "Nacionalidade": $(this).find("#Nacionalidade").val(),
            "Estado": $(this).find("#Estado").val(),
            "Cidade": $(this).find("#Cidade").val(),
            "Logradouro": $(this).find("#Logradouro").val(),
            "Telefone": $(this).find("#Telefone").val()
        }
        
        $.ajax({
            url: urlAlteracao,
            method: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(dataJson),
            error:
                function (r) {
                    if (r.status == 400) {
                        document.getElementById("clienteBody").innerHTML = "";
                        document.getElementById("clienteHeader").innerHTML = "";
                        $('#clienteBody').html("CPF inválido, use outro CPF!");
                        $('#clienteHeader').html("Alerta!");
                        $('#clienteModal').modal('show');
                        setTimeout(function () {
                            $('#clienteModal').modal('hide');
                            window.location.href = urlRetorno;
                        }, 3000);
                    }
                    else if (r.status == 500) {
                        document.getElementById("clienteBody").innerHTML = "";
                        document.getElementById("clienteHeader").innerHTML = "";
                        $('#clienteBody').append("Ocorreu um erro no servidor!");
                        $('#clienteHeader').append("Erro!");
                        $('#clienteModal').modal('show');
                        setTimeout(function () {
                            $('#clienteModal').modal('hide');
                            window.location.href = urlRetorno;
                        }, 3000);
                    }

                },
            success:
                function (r) {
                    if (r.status == 400) {
                        document.getElementById("clienteBody").innerHTML = "";
                        document.getElementById("clienteHeader").innerHTML = "";
                        $('#clienteBody').html("CPF já cadastrado, use outro CPF!");
                        $('#clienteHeader').html("Alerta!");
                        $('#clienteModal').modal('show');
                        setTimeout(function () {
                            $('#clienteModal').modal('hide');
                            window.location.href = urlRetorno;
                        }, 3000);
                    } else {
                        document.getElementById("clienteBody").innerHTML = "";
                        document.getElementById("clienteHeader").innerHTML = "";
                        $('#clienteBody').html("Cliente alterado com sucesso!");
                        $('#clienteHeader').html("Sucesso!");
                        $('#clienteModal').modal('show');
                        setTimeout(function () {
                            $('#clienteModal').modal('hide');
                            window.location.href = urlRetorno;
                        }, 3000);

                    }
                }
        });
    });


    
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var id = "#" + random;
    var content = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body" id="modalBody">                                                                           ' +
        '                                                                                               ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';
    $('body').append(content);
    $('#clienteBody').append(texto);
    $('#clienteModal').modal('show');
}

function ModalBeneficiarios(id) {
    //alert(id);
    //alert(urlBase + '/Beneficiario/Index/' + id);

    // Exibe o valor do modal body antes de fazer a requisição
    //alert(document.getElementById("modalBody").value);

    $.ajax({
        url: urlBase + '/Beneficiario/Index/' + id, // URL da requisição
        type: 'GET',
        // Não é necessário usar data aqui para uma requisição GET
        success: function (data) {
            $("#meuModal").modal("show"); // Mostra o modal
            $("#modalBody").html(data); // Insere o conteúdo retornado
            
        },
        error: function () {
            alert("Erro ao chamar o controlador!");
        }
    });
}
