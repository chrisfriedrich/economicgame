$(document).ready(function () {

    var timeout = 75000 + Math.floor(Math.random() * 25000);

    setTimeout(function () {
        $('#sellerTyping').fadeIn(500);
    }, 7000);

    setTimeout(function () {
        $('#sellerTyping').hide(300);
    }, 13000);

    setTimeout(function () {
        $('#sellerMessage1').show(200);
    }, 13400);

    setTimeout(function () {
        $('#sellerTyping').fadeIn(500);
    }, 18000);

    setTimeout(function () {
        $('#sellerTyping').hide();
    }, 23000);

    setTimeout(function () {
        $('#sellerMessage2').show(200);
    }, 23500);

    setTimeout(function () {
        $('#sellerTyping').fadeIn(500);
    }, 28000);

    setTimeout(function () {
        $('#sellerTyping').hide(300);
    }, 33000);

    setTimeout(function () {
        $('#sellerMessage3').show(200);
    }, 33400);

    setTimeout(function () {
        $('#sellerTyping').hide(300);
    }, timeout);

    setTimeout(function () {
        $('#sellerMessage4').show(200);
    }, timeout);

    $(".toggleFAQ").click(function () {
        $('#FAQtable').toggle();
        $('.FAQ').toggle(500);
    });

    $('#submitButton').prop('disabled', true);
    $('input[type="text"]').keyup(function () {
        var startingAmount = $("#StartingAmount").val();
        var startingCents = parseInt(startingAmount.substr(startingAmount.length - 2));

        var investCents = 0;
        var keptCents = 0;

        if (!$("#RoundKeptCents").val()) {
            keptCents = 0;
        }
        else {
            keptCents = parseInt($("#RoundKeptCents").val());
        }

        if (!$("#RoundInvestmentCents").val()) {
            investCents = 0;
        }
        else {
            investCents = parseInt($("#RoundInvestmentCents").val());
        }

        if ((keptCents + investCents) == startingCents) {
            $('#submitButton').prop('disabled', false);
            $('#submitButton').prop("class", "btn btn-default btn-success");
        }
        else {
            $('#submitButton').prop('disabled', true);
            $('#submitButton').prop("class", "btn btn-default");
        }
    });

});