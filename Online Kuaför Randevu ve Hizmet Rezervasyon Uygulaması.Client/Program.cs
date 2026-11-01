using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Online_Kuaför_Randevu_ve_Hizmet_Rezervasyon_Uygulamasý.Client.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<SalonDataService>();
builder.Services.AddScoped<AppointmentSessionService>();
builder.Services.AddScoped<AppointmentBusinessService>();
builder.Services.AddScoped<LocalStorageService>();

await builder.Build().RunAsync();
