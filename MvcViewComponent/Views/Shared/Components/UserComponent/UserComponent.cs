using Microsoft.AspNetCore.Mvc;
using MvcViewComponent.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;

namespace MvcViewComponent.Views.Shared.Components.MyComponent
{
    public class UserComponent : ViewComponent
    {
        RestClient client = new RestClient("https://dummyjson.com");

        public async Task<IViewComponentResult> InvokeAsync(int limit, int skip)
        {
            // Define o endpoint da api
            string endPoint = $"/users?limit={limit}&skip={skip}";

            var request = new RestRequest(endPoint, Method.Get);

            try
            {
                var response = await client.GetAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var myDeserializedClass = JsonConvert.DeserializeObject<BaseResponseUser>(response.Content);

                    var items = new ObservableCollection<User>(myDeserializedClass.users);

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
