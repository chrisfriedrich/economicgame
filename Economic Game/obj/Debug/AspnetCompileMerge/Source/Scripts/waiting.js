$(document).ready(function () {

            $('#submitButton').prop('disabled', true);

            var $pG = $('#progressbar').progressbar();

            var step1 = 10000 + Math.floor(Math.random() * 5000);
            var period1 = step1;
            var speed1 = step1 / 50;

            var step2 = 3000 + Math.floor(Math.random() * 3000);
            var period2 = step2 + period1;
            var speed2 = step2 / 50;

            $pG.progressbar({
                value: 1
            });

            setTimeout(function () {
                $('#progressMessage1').fadeIn(200);
                var pGress = setInterval(function () {
                    var pVal = $pG.progressbar('option', 'value');
                    var pCnt = !isNaN(pVal) ? (pVal + 1) : 1;
                    if (pCnt > ((period1 / period2) * 100)) {
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
                    if (pCnt > ((period2 / period2) * 100)) {
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
                $('#messageForm').submit();
            }, period2 + 300);
        });