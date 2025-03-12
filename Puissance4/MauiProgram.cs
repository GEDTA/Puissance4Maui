using Microsoft.Extensions.Logging;
using Puissance4.Models;
using Puissance4.ViewModels;

namespace Puissance4;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton<GameModel>();
		builder.Services.AddSingleton<GameViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
      
        
        return builder.Build();
	}
}
