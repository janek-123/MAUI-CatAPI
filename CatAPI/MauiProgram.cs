﻿using CatAPI.Services;
using CatAPI.ViewModels;
using Microsoft.Extensions.Logging;

namespace CatAPI;
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

        builder.Services.AddSingleton<ICatApiService, CatApiService>();

        builder.Services.AddTransient<CatsPage>();
        builder.Services.AddTransient<CatsViewModel>();

        builder.Services.AddTransient<CatDetailPage>();
        builder.Services.AddTransient<CatDetailViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
