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
        var startingDecimal = parseFloat($("#StartingAmount").val().replace("$", "")).toFixed(2);

        var startingCents = parseInt(startingAmount.substr(startingAmount.length - 2));
        
        var amounts = startingAmount.split(".");
        var startingDollars = parseInt(amounts[0].replace("$", ""));
        //var startingCents = parseInt(amounts[1]);

        var investCents = 0;
        var investCentString = "";
        var investDecimalString = "";

        var investDollars = 0;

        var keptCents = 0;
        var keptCentString = "";
        var keptDecimalString = "";

        var keptDollars = 0;

        if (!$("#RoundKeptDollars").val()) {
            keptDollars = 0;
            keptDecimalString = "0.";
        }
        else {
            keptDollars = parseInt($("#RoundKeptDollars").val());
            keptDecimalString = $("RoundKeptDollars").val() + ".";
        }

        if (!$("#RoundKeptCents").val()) {
            keptCents = 0;
            keptDecimalString += "00";
        }
        else {
            //keptCents = parseInt($("#RoundKeptCents").val());
            //var keptCentString = $("#RoundKeptCents").val();
            //if(keptCentString.Length() == 1)
            //{
            //    keptDecimalString += ("0" + keptCentString);
                keptCents = parseFloat($("#RoundKeptCents").val()) / 100;
            //}
            //else
            //{
            //    keptDecimalString += keptCentString;
            //    keptCents = parseFloat("." + $("#RoundKeptCents").val())
            //}

        }


        if (!$("#RoundInvestmentDollars").val()) {
            investmentDollars = 0;
            investmentDecimalString = "0.";
        }
        else {
            investmentDollars = parseInt($("#RoundInvestmentDollars").val());
            investmentDecimalString = $("RoundInvestmentDollars").val() + ".";
        }

        if (!$("#RoundInvestmentCents").val()) {
            investmentCents = 0;
            investmentDecimalString += "00";
        }
        else {
            //investmentCents = parseInt($("#RoundInvestmentCents").val());
            //var investmentCentString = $("#RoundInvestmentCents").val();
            //if (investmentCentString.Length() == 1) {
            //    investmentDecimalString += ("0" + $("#RoundInvestmentCents").val());
                investmentCents = parseFloat($("#RoundInvestmentCents").val())/100;

            //}
            //else {
            //    investmentDecimalString += investmentCentString;
            //    investmentCents = parseFloat("." + $("#RoundInvestmentCents").val())
            //}
        }

        var investmentDecimal = parseFloat(investmentDecimalString).toFixed(2);
        var keptDecimal = parseFloat(keptDecimalString).toFixed(2);

        if ((investmentDollars + investmentCents + keptDollars + keptCents) == startingDecimal) {
            $('#submitButton').prop('disabled', false);
            $('#submitButton').prop("class", "btn btn-default btn-success");
        }
        else {
            $('#submitButton').prop('disabled', true);
            $('#submitButton').prop("class", "btn btn-default");
        }

        //if ((investmentDecimal + keptDecimal) == startingDecimal) {
        //    $('#submitButton').prop('disabled', false);
        //    $('#submitButton').prop("class", "btn btn-default btn-success");
        //}
        //else {
        //    $('#submitButton').prop('disabled', true);
        //    $('#submitButton').prop("class", "btn btn-default");
        //}
    });

});