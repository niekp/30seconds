using _30seconds.Data;
using _30seconds.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace _30seconds.Controllers {
	public class MaintenanceController : Controller {
		private readonly GameContext db;

		public MaintenanceController(GameContext db) {
			this.db = db;
		}
		public IActionResult Index() {
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Wordlist() {
			var wordlist = db.Wordlist.Include(l => l.Words).FirstOrDefault();
			if (!(wordlist is Wordlist)) {
				wordlist = new Wordlist() {
					Title = "Standaard"
				};
				db.Add(wordlist);
				await db.SaveChangesAsync();
			} else {
				foreach (var word in wordlist.Words) {
					db.Remove(word);
				}
				await db.SaveChangesAsync();
			}

			var words = System.IO.File.ReadAllLines("woordenlijst.txt");
			foreach (var w in words) {
				var word = new Word(w, wordlist.Id);
				db.Add(word);
			}
			await db.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}
