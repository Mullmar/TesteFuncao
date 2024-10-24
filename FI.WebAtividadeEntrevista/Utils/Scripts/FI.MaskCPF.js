function MascaraCPF(input) {
    $(document).ready(function () {
        var $seuCampoCpf = $(input);
        $seuCampoCpf.mask('000.000.000-00', { reverse: true });
    });
}

