var _regexGlobalEncontraNaoDigitos = RegExp("\\D");

/// <summary>Variável helper que executa algumas regex configuradas para procurar em toda expressão.</summary>
var GlobalRegex = new function () { this.PossuiCaractereNaoNumerico = function (value) { return _regexGlobalEncontraNaoDigitos.test(value); } };

function TempoExtenso() {
    TempoAtual = new Date();
    HoraAtual = TempoAtual.getHours();
    MinutoAtual = TempoAtual.getMinutes();
    SegundoAtual = TempoAtual.getSeconds();

    TempoAuxiliar = (HoraAtual > 24) ? HoraAtual - 24 : HoraAtual;
    if (TempoAuxiliar == "0") MomentoAuxiliar = 24;
    TempoAuxiliar += ((MinutoAtual < 10) ? ":0" : ":") + MinutoAtual;
    TempoAuxiliar += ((SegundoAtual < 10) ? ":0" : ":") + SegundoAtual;

    return TempoAuxiliar;
}

function DataExtenso() {
    ArrayDia = new Array("Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado");
    ArrayMes = new Array("Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro");

    DataAtual = new Date();
    AnoAtual = DataAtual.getYear();
    DiaSemana = DataAtual.getDay();
    MesAtual = DataAtual.getMonth();
    DiaAtual = DataAtual.getDate();

    DataAuxiliar = ArrayDia[DiaSemana] + ", ";
    DataAuxiliar += DiaAtual + " de ";
    DataAuxiliar += ArrayMes[MesAtual] + " de ";

    if (AnoAtual < 2000) AnoAtual = AnoAtual + 1900;
    DataAuxiliar += AnoAtual;

    return DataAuxiliar;
}

function validarData(textBox, value) {
    if (textBox != null) {
        var valor = textBox.value;
    }
    else {
        var valor = value;
    }

    if (valor == '') {
        return true;
    }

    var date = valor;
    var ardt = new Array;
    var ExpReg = new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
    ardt = date.split("/");
    erro = false;
    if (date.search(ExpReg) == -1) {
        erro = true;
    }
    else if (((ardt[1] == 4) || (ardt[1] == 6) || (ardt[1] == 9) || (ardt[1] == 11)) && (ardt[0] > 30))
        erro = true;
    else if (ardt[1] == 2) {
        if ((ardt[0] > 28) && ((ardt[2] % 4) != 0))
            erro = true;
        if ((ardt[0] > 29) && ((ardt[2] % 4) == 0))
            erro = true;
    }
    if (erro) {
        return false;
    }
    return true;
}

function DataHoraExtenso(IdObjeto) {
    if (document.getElementById(IdObjeto))
        document.getElementById(IdObjeto).innerHTML = DataExtenso() + " - " + TempoExtenso();
}

function formatarCPFCNPJ(textbox) {
    var tamanho = extrairValor(textbox.value).length;

    if (tamanho < 12) {
        mascarar(textbox, '###.###.###-##');
    } else {
        mascarar(textbox, '##.###.###/####-##');
    }
}

function isNumeric(str) {
    var er = /^[0-9]+$/;
    return (er.test(str));
}

function extrairValor(texto) {

    var textoResultante = "";
    var i;
    var caractere;
    var zeroAEsquerda = false;

    if (texto != null && texto != 'undefined') {
        for (i = 0; i < texto.length; i++) {

            caractere = texto.charAt(i);

            //Tratar apenas números
            if (caractere >= '0' && caractere <= '9') {
                textoResultante += caractere;
            }
        }
    }

    return textoResultante;
}

function mascarar(textbox, mascara) {

    //Tratar o valor da texbox sem os caracteres não numéricos
    var valor = extrairValor(textbox.value);
    var valorFormatado = "";
    var i = 0;
    var j = 0;

    //Enquanto não acabar a máscara ou o valor
    while (i < valor.length && j < mascara.length) {

        //Se o caractere da máscara indicar a presença de um caractere numérico
        if (mascara.charAt(j) == '#') {
            valorFormatado = valorFormatado + valor.charAt(i++);

            //Se o caractere da máscara indicar a presença de um caractere pré definido
        } else {
            valorFormatado = valorFormatado + mascara.charAt(j);
        }

        //Incrementar o índice da máscara               
        j++;
    }
    //Mostrar o valor formatado
    textbox.value = valorFormatado;
}

function MaxlengthTextArea(textbox, thelimit) {
    if (textbox.value.length >= thelimit) {
        textbox.value = textbox.value.substr(0, thelimit);
    }
}

function replaceAll(value, token, newToken) {
    while (value.indexOf(token) != -1) {
        value = value.replace(token, newToken);
    }
    return value;
}

function SomenteNumeros() {
    var caracterDigitado = String.fromCharCode(window.event.keyCode);
    validChars = '0123456789';
    return validChars.indexOf(caracterDigitado) > -1;
}

function VerificarTipoArquivoPDF(source, arguments) {
    var sFile = arguments.Value.toString().toLowerCase();
    arguments.IsValid = sFile.endsWith('.pdf');
}

function VerificarTipoArquivo(source, arguments) {
    var sFile = arguments.Value.toString().toLowerCase();
    arguments.IsValid =
            ((sFile.endsWith('.xls')) ||
            (sFile.endsWith('.xlsx')) ||
            (sFile.endsWith('.pdf')) ||
            (sFile.endsWith('.docx')) ||
            (sFile.endsWith('.doc')));
}

function ShowCurrentDateOnCalendarExtender(sender, args) {
    var selectedDate = new Date();
    if (sender._textbox.get_element().value != "") {
        selectedDate = Date.parseLocale(sender._textbox.get_element().value,
                                        sender._textbox.get_element().CalendarBehavior._format);
    }
    sender._selectedDate = selectedDate;
}

function SomenteNumerosInteiros(textBox, teclapress) {
    var charCode = (teclapress.which) ? teclapress.which : teclapress.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
}

function LimparTextoColadoNoCampo(textBox) {
    var valor = textBox.value;
    if (GlobalRegex.PossuiCaractereNaoNumerico(valor)) {
        textBox.value = "";
    } else {
        return valor;
    }
}

function MascaraNumeroCom3Decimais(objTextBox, e) {
    var sep = 0;
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var SeparadorDecimal = ",";
    var SeparadorMilesimo = "."
    len = objTextBox.value.length;
    for (i = 0; i < len; i++)
        if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
    aux = '';
    for (; i < len; i++)
        if (strCheck.indexOf(objTextBox.value.charAt(i)) != -1) aux += objTextBox.value.charAt(i);
    len = aux.length;
    if (len == 0) objTextBox.value = '';
    if (len == 1) objTextBox.value = '0' + SeparadorDecimal + '00' + aux;
    if (len == 2) objTextBox.value = '0' + SeparadorDecimal + '0' + aux;
    if (len == 3) objTextBox.value = '0' + SeparadorDecimal + aux;
    if (len > 3) {
        aux2 = '';
        for (j = 0, i = len - 4; i >= 0; i--) {
            if (j == 3) {
                aux2 += SeparadorMilesimo;
                j = 0;
            }
            aux2 += aux.charAt(i);
            j++;
        }
        objTextBox.value = '';
        len2 = aux2.length;
        for (i = len2 - 1; i >= 0; i--)
            objTextBox.value += aux2.charAt(i);
        objTextBox.value += SeparadorDecimal + aux.substr(len - 3, len);
    }
}

function formatarCampoInteiro(input) {
    var output = input
    if (parseFloat(input)) {
        input = new String(input);
        var parts = input.split(".");
        parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
        output = parts.join(".");
    }

    return output;
}