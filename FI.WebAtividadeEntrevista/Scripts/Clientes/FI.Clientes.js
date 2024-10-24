
$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: url,
            method: "POST",
            data: {
                "Id":0,
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "CPF": $(this).find("#CPF").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val()
            },
            error:
                function (r) {
                    if (r.status == 400) {
                        document.getElementById("clienteBody").innerHTML = "";
                        document.getElementById("clienteHeader").innerHTML = "";
                        $('#clienteBody').html("CPF já cadastrado, use outro CPF!");
                        $('#clienteHeader').html("Alerta!");
                        $('#clienteModal').modal('show');
                    }
                    else if (r.status == 500) {
                        document.getElementById("clienteBody").innerHTML = "";
                        document.getElementById("clienteHeader").innerHTML = "";
                        $('#clienteBody').html("Ocorreu um erro no servidor");
                        $('#clienteHeader').html("Erro!");
                        $('#clienteModal').modal('show');
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
                    } else {
                        document.getElementById("clienteBody").innerHTML = "";
                        document.getElementById("clienteHeader").innerHTML = "";
                        $('#clienteBody').html("Cliente cadastrado com sucesso!");
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

    function ModalDialog(titulo, texto) {

        var random = Math.random().toString().replace('.', '');
        var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
            '        <div class="modal-dialog">                                                                                 ' +
            '            <div class="modal-content">                                                                            ' +
            '                <div class="modal-header">                                                                         ' +
            '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
            '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
            '                </div>                                                                                             ' +
            '                <div class="modal-body">                                                                           ' +
            '                    <p>' + texto + '</p>                                                                           ' +
            '                </div>                                                                                             ' +
            '                <div class="modal-footer">                                                                         ' +
            '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
            '                                                                                                                   ' +
            '                </div>                                                                                             ' +
            '            </div><!-- /.modal-content -->                                                                         ' +
            '  </div><!-- /.modal-dialog -->                                                                                    ' +
            '</div> <!-- /.modal -->                                                                                        ';

        $('body').append(texto);
        $('#' + random).modal('show');
    }
});
