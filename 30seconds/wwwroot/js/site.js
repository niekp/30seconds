(function ($) {
    var $words = $("[data-id='words']"),
        $timer = $("[data-id='timer']"),
        IdRoom = $("[data-id='room-id']").val();

    $(document).ready(function () {
        $("[data-trigger='new-round']").on('click', newRound);

        setInterval(gameHandler, 1000);
    })

    function makeid(length) {
        var result = '';
        var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var charactersLength = characters.length;
        for (var i = 0; i < length; i++) {
            result += characters.charAt(Math.floor(Math.random() * charactersLength));
        }
        return result;
    }


    function getUser() {
        if (!localStorage.getItem("user")) {
            localStorage.setItem("user", makeid(20))
        }

        return localStorage.getItem("user");
    }

    function newRound() {
        getGame(true);
    }

    function getGame(forceNew = false, callback) {
        $.getJSON("/Game/GetGame/?user=" + getUser() + "&IdRoom=" + IdRoom + "&forceNew=" + forceNew, function (game) {
            if (typeof (callback) == "function")
                callback(game);
        });
    }

    function gameHandler() {

        var game = getGame(false, function (game) {
            $words.html("");
            var start = new Date(game.start);
            var timer = (new Date() - new Date(game.start)) / 1000;
            var remaining = 31 - timer;

            if (game.user == getUser() || remaining < -2) {
                $words.show();
                game.words.forEach(function (word) {
                    $words.append(word + "<br />")
                })
            } else {
                $words.hide();
            }

            
            if (remaining >= 0) {
                $timer.html(Math.round(31 - timer) + " seconden resterend");
            } else {
                $timer.html("De tijd is voorbij!");
            }
        });
    }

})(jQuery);