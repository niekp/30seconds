(function ($) {
	var $words = $("[data-id='words']"),
		$timer = $("[data-id='timer']"),
		$rooms = $(".room-container"),
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
		console.log($rooms.length)
		if (!$words.length) {
			if ($rooms.length) {
				$(".room-container").load(location.href + " .room-container .card");
			}

			return;
		}

		var game = getGame(false, function (game) {
			$words.html("<ul class='list-group'></ul>");
			var start = new Date(game.start);
			var timer = (new Date() - new Date(game.start)) / 1000;
			var remaining = 31 - timer;

			if (game.user == getUser() || remaining < -2) {
				$words.show();
				game.words.sort(compareWords);
				game.words.forEach(function (word) {
					$words.find("ul").append("<li class='list-group-item'>" + word.text + "</li>")
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

	function compareWords(a, b) {
		if (a.text < b.text) {
			return -1;
		}
		if (a.text > b.text) {
			return 1;
		}
		return 0;
	}

})(jQuery);