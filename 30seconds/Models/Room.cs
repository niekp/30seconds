using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _30seconds.Models {
	public class Room {
		public Room() {
			Created = DateTime.Now;
			AmountOfSeconds = 30;
		}

		[Key]
		public int Id { get; set; }

		[Display(Name = "Naam")]
		public string Name { get; set; }

		[JsonIgnore]
		public virtual ICollection<Game> Games { get; set; }

		[Display(Name = "Woordenlijst")]
		public int IdWordlist { get; set; }

		[JsonIgnore]
		[ForeignKey(nameof(IdWordlist))]
		public virtual Wordlist Wordlist { get; set; }

		[Display(Name = "Aantal seconden")]
		public int AmountOfSeconds { get; set; }

		public DateTime Created { get; set; }
	}
}
