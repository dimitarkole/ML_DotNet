using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarPriceAI
{
    public class DataGatherer
    {
        public async Task<IEnumerable<RowData>> GatherData()
        {
            var results = new List<RowData>();
            var parser = new HtmlParser();
            var client = new HttpClient();
            var url = $"https://www.autoscout24.bg/objava/renault-megane-iii-grandtour-authentique-1-2-tce-115-rdc-klima-te---ac5ccd47-7daf-4e19-a53d-e7043a79f92b?cldtidx=14&cldtsrc=listPage";

            string htmlContent = null;
            for (var i = 0; i < 10; i++)
            {
                try
                {
                    var response = await client.GetAsync(url);
                    htmlContent = await response.Content.ReadAsStringAsync();
                    break;
                }
                catch
                {
                    Console.Write('!');
                    Thread.Sleep(500);
                }
            }

            /* if (string.IsNullOrWhiteSpace(htmlContent))
             {
                 break;
             }*/
            var document = await parser.ParseDocumentAsync(htmlContent);

            // Console.WriteLine(document.Body);

            var price = document.GetElementsByClassName("cldt-price")[0];
            var km = document.GetElementsByClassName("cldt-stage-primary-keyfact")[0];
            var registration = document.GetElementById("basicDataFirstRegistrationValue");
            var horse = document.GetElementsByClassName("sc-font-l cldt-stage-primary-keyfact")[2];
            var brand = document.GetElementsByClassName("cldt-stealth-link")[1];
            var model = document.GetElementsByClassName("cldt-stealth-link")[2];
            Console.WriteLine("price =" + price.TextContent.Trim());
            Console.WriteLine("km =" + km.TextContent.Trim());
            Console.WriteLine("registration =" + registration.TextContent.Trim());
            Console.WriteLine("horse =" + horse.TextContent.Trim());
            Console.WriteLine("model =" + model.TextContent.Trim());
            Console.WriteLine("brand =" + brand.TextContent.Trim());

            var informations = document.GetElementsByClassName("cldt-stealth-link");
            foreach (var information in informations)
            {
                Console.WriteLine("characteristic =" + information.TextContent);

            }

            /*var characteristic = document.GetElementsByClassName("cld-categorized-data")[0];
            Console.WriteLine("characteristic =" + characteristic.TextContent.Trim());

            /*var dateSections = document.GetElementsByClassName("tir_title");
             var dateAndEditionString = dateSections[0].TextContent.Trim();
             var dateString = dateAndEditionString.Substring(Math.Max(0, dateAndEditionString.Length - 10), 10);
             var date = DateTime.Parse(dateString);
            */


            return results;
        }

    }
}
