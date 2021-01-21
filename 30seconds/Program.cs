using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace _30seconds {
	public class Program {
		public static void Main(string[] args) {
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) {
			return Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(webBuilder => {
	webBuilder.UseUrls(urls: "http://localhost:5007");
	webBuilder.UseStartup<Startup>();
});
		}
	}
}
