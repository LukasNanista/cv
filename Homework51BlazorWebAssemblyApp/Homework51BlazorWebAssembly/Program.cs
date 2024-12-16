using Homework51BlazorWebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

//task: Build a Blazor WebAssembly application that has a simple data-entry page with First and Last name fields. Add the name to a list below the form when submitted.

/* task overview:
 * 1. create blazor web assembly app - DONE
 * 2. create person model (FN, LN) - DONE
 * 3. create razor component for person form (input FN, LN, submit button) - DONE
 * 4. link page in navbar - DONE
 * 
 * all tasks complete
 */

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
