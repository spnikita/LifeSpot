﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Text;

namespace LifeSpot
{
    public static class EndpointMapper
    {
        /// <summary>
        /// Маппинг css-файлов
        /// </summary>
        /// <param name="builder">Объект, реализующий EndpointRouteBuilder</param>
        public static void MapCss(this IEndpointRouteBuilder builder)
        {
            var cssFiles = new[] { "index.css" };

            foreach (var filename in cssFiles)
            {
                builder.MapGet($"/Static/CSS/{filename}", async context =>
                {
                    var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "CSS", filename);
                    var css = await File.ReadAllTextAsync(cssPath);
                    await context.Response.WriteAsync(css);
                });
            }
        }

        /// <summary>
        /// Маппинг js-файлов
        /// </summary>
        /// <param name="builder">Объект, реализующий EndpointRouteBuilder</param>
        public static void MapJs(this IEndpointRouteBuilder builder)
        {
            var jsFiles = new[] { "index.js", "testing.js", "about.js" };

            foreach (var filename in jsFiles)
            {
                builder.MapGet($"/Static/JS/{filename}", async context =>
                {
                    var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", filename);
                    var js = await File.ReadAllTextAsync(jsPath);
                    await context.Response.WriteAsync(js);
                });
            }
        }

        /// <summary>
        /// Маппинг изображений
        /// </summary>
        /// <param name="builder">Объект, реализующий EndpointRouteBuilder</param>
        public static void MapImage(this IEndpointRouteBuilder builder)
        {
            var images = new[] { "london.jpg", "ny.jpg", "spb.jpg"};

            foreach (var imageName in images)
            {
                builder.MapGet($"/Static/Images/{imageName}", async context =>
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "Images", imageName);                   
                    context.Response.ContentType = "image/jpg";
                    await using var stream = File.OpenRead(imagePath);
                    await stream.CopyToAsync(context.Response.Body);
                });
            }           
        }

        /// <summary>
        /// Маппинг html-файлов
        /// </summary>
        /// <param name="builder">Объект, реализующий EndpointRouteBuilder</param>
        public static void MapHtml(this IEndpointRouteBuilder builder)
        {
            string footerHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "footer.html"));
            string sideBarHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "sideBar.html"));
            string sliderHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "slider.html"));

            builder.MapGet("/", async context =>
            {
                var manePagePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "index.html");
                // Загружаем шаблон страницы, вставляя в него элементы
                var htmlContent = new StringBuilder(await File.ReadAllTextAsync(manePagePath))    
                .Replace("<!--SIDEBAR-->", sideBarHtml)
                    .Replace("<!--FOOTER-->", footerHtml);

                await context.Response.WriteAsync(htmlContent.ToString());
            });

            builder.MapGet("/testing", async context =>
            {
                var testingPagePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "testing.html");

                // Загружаем шаблон страницы, вставляя в него элементы
                var htmlContent = new StringBuilder(await File.ReadAllTextAsync(testingPagePath))
                    .Replace("<!--SIDEBAR-->", sideBarHtml)
                    .Replace("<!--FOOTER-->", footerHtml);

                await context.Response.WriteAsync(htmlContent.ToString());
            });

            builder.MapGet("/about", async context =>
            {
                var aboutPagePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "about.html");

                // Загружаем шаблон страницы, вставляя в него элементы
                var htmlContent = new StringBuilder(await File.ReadAllTextAsync(aboutPagePath))
                    .Replace("<!--SLIDER-->", sliderHtml)
                    .Replace("<!--SIDEBAR-->", sideBarHtml)
                    .Replace("<!--FOOTER-->", footerHtml);

                await context.Response.WriteAsync(htmlContent.ToString());
            });
        }
    }
}
