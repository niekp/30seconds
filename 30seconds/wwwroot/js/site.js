(function ($) {
	var $words = $("[data-id='words']"),
		$timer = $("[data-id='timer']"),
		$rooms = $(".room-container"),
		IdRoom = $("[data-id='room-id']").val();

	$(document).ready(function () {
		$("[data-trigger='new-round']").on('click', newRound);

		getGame(displayGame);
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

	function getGame(callback) {
		$.getJSON("/Game/GetGame/?IdRoom=" + IdRoom, function (game) {
			if (typeof (callback) == "function")
				callback(game);
		});
	}

	function getNewGame(callback) {
		$.getJSON("/Game/CreateGame/?user=" + getUser() + "&IdRoom=" + IdRoom, function (game) {
			if (typeof (callback) == "function")
				callback(game);
		});
	}

	function newRound() {
		getNewGame(displayGame);
	}

	function gameHandler() {
		console.log($rooms.length)
		if (!$words.length) {
			if ($rooms.length) {
				$(".room-container").load(location.href + " .room-container .card");
			}

			return;
		}

		getGame(displayGame);
	}

	function displayGame(game) {
		if (!game) {
			return;
		}

		$words.html("<ul class='list-group'></ul>");
		var remaining = game.remaining.seconds;

		if (game.user == getUser() || (remaining < -2 && remaining > -(60*5))) {
			$words.show();
			game.words.sort(compareWords);
			game.words.forEach(function (word) {
				$words.find("ul").append("<li class='list-group-item'>" + word.text + "</li>")
			})
		} else {
			$words.hide();
		}


		if (remaining >= 0) {
			$timer.html(remaining + " seconden resterend");
		} else {
			$timer.html("<strong>De tijd is voorbij!</strong>");
		}
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