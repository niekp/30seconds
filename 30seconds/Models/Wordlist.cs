using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _30seconds.Models {
	public class Wordlist {
		public Wordlist() {
		}

		[Key]
		public int Id { get; set; }

		public string Title { get; set; }

		public ICollection<Word> Words { get; set; }
	}
}
