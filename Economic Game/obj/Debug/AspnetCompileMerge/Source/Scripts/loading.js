$(document).ready(function () {

    var step1 = 20000 + Math.floor(Math.random() * 10000);
    var period1 = step1;
    var speed1 = step1 / 30;

    var step2 = 7000 + Math.floor(Math.random() * 3000);
    var period2 = step2 + period1;
    var speed2 = step2 / 30;

    var step3 = 8000 + Math.floor(Math.random() * 2000);
    var period3 = step3 + period2;
    var speed3 = step3 / 30;

    var step4 = 3000 + Math.floor(Math.random() * 2000);
    var period4 = step4 + period3;
    var speed4 = step4 / 30;

    var step5 = 3000 + Math.floor(Math.random() * 2000);
    var period5 = step5 + period4;
    var speed5 = step5 / 30;


    $('#submitButton').prop('disabled', true);

    var $pG = $('#progressbar').progressbar();

    $pG.progressbar({
        value: 1
    });

    setTimeout(function () {
        $('#progressMessage1').fadeIn(200);
        var pGress = setInterval(function () {
            var pVal = $pG.progressbar('option', 'value');
            var pCnt = !isNaN(pVal) ? (pVal + 1) : 1;
            if (pCnt > ((period1 / period5) * 100)) {
                clearInterval(pGress);
            } else {
                $pG.progressbar({
                    value: pCnt
                });
            }
        }, speed1);
    }, 0);

    setTimeout(function () {
        $('#progressMessage1').fadeOut(200);
    }, period1);

    setTimeout(function () {
        $('#progressMessage2').fadeIn(200);
        var pGress = setInterval(function () {
            var pVal = $pG.progressbar('option', 'value');
            var pCnt = !isNaN(pVal) ? (pVal + 1) : 1;
            if (pCnt > ((period2 / period5) * 100)) {
                clearInterval(pGress);
            } else {
                $pG.progressbar({
                    value: pCnt
                });
            }
        }, speed2);
    }, period1);

    setTimeout(function () {
        $('#progressMessage2').fadeOut(200);
    }, period2);

    setTimeout(function () {
        $('#progressMessage3').fadeIn(200);
        var pGress = setInterval(function () {
            var pVal = $pG.progressbar('option', 'value');
            var pCnt = !isNaN(pVal) ? (pVal + 1) : 1;
            if (pCnt > ((period3 / period5) * 100)) {
                clearInterval(pGress);
            } else {
                $pG.progressbar({
                    value: pCnt
                });
            }
        }, speed3);
    }, period2);

    setTimeout(function () {
        $('#progressMessage3').fadeOut(200);
    }, period3);

    setTimeout(function () {
        $('#progressMessage4').fadeIn(200);
        var pGress = setInterval(function () {
            var pVal = $pG.progressbar('option', 'value');
            var pCnt = !isNaN(pVal) ? (pVal + 1) : 1;
            if (pCnt > ((period4 / period5) * 100)) {
                clearInterval(pGress);
            } else {
                $pG.progressbar({
                    value: pCnt
                });
            }
        }, speed4);
    }, period3)

    setTimeout(function () {
        $('#progressMessage4').fadeOut(500);
    }, period4);

    setTimeout(function () {
        $('#progressMessage5').show();
        var pGress = setInterval(function () {
            var pVal = $pG.progressbar('option', 'value');
            var pCnt = !isNaN(pVal) ? (pVal + 1) : 1;
            if (pCnt > 100) {
                clearInterval(pGress);
            } else {
                $pG.progressbar({
                    value: pCnt
                });
            }
        }, speed5);
    }, period4)

    setTimeout(function () {
        $('#submitButton').prop("disabled", false);
    }, period4 + 300);

    setTimeout(function () {
        if ($('#submitButton').text.length > 0) {
            $('#submitButton').prop("class", "btn btn-default btn-success");
        }
    }, period4 + 400);
});