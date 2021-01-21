using System.Collections.Generic;

namespace _30seconds.Models {
	public class LobbyViewModel {
		public LobbyViewModel() {
		}

		public List<Room> Rooms { get; set; }
		public Room NewRoom { get; set; }
		public List<Wordlist> Wordlists { get; set; }
	}
}
