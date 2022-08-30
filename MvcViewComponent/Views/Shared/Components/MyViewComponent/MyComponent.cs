using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MvcViewComponent.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;

namespace MvcViewComponent.Views.Shared.Components.MyViewComponent
{
    public class MyComponent : ViewComponent
    {
        // Define o endereço de base para a api
        RestClient client = new RestClient("https://dummyjson.com");

        public async Task<IViewComponentResult> InvokeAsync(int limit, int skip)
        {
            // Define o endpoint da api
            string endPoint = $"/products?limit={limit}&skip={skip}";

            var request = new RestRequest(endPoint, Method.Get);

            try
            {
                var response = await client.GetAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var myDeserializedClass = JsonConvert.DeserializeObject<BaseResponseProduct>(response.Content);

                    var items = new ObservableCollection<Product>(myDeserializedClass.products);

                    return View(items);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
                return null;
            }

            return null;
        }
    }
}
