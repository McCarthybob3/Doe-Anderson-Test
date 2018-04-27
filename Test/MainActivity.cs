using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;
using Org.Apache.Http.Client;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Test
{
    [Activity(Label = "Test", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

          
            SetContentView(Resource.Layout.Main);

            string url = "http://randomuser.me/api";

            var client = new HttpClient();
            string select = "";
            var response = await client.GetAsync(url);
            Button cake = FindViewById<Button>(Resource.Id.cakebut);
            Button contact = FindViewById<Button>(Resource.Id.contactbut);
            Button key = FindViewById<Button>(Resource.Id.keybut);
            Button mail = FindViewById<Button>(Resource.Id.mailbut);
            Button map = FindViewById<Button>(Resource.Id.mapbut);
            Button phone = FindViewById<Button>(Resource.Id.phonebut);

            cake.Click += async (sender, e) =>
            {
                select = "dob";

            };
            contact.Click += async (sender, e) =>
            {
                select = "phone";

            };
            key.Click += async (sender, e) =>
            {
                select = "registered";

            };
            mail.Click += async (sender, e) =>
            {
                select = "email";

            };
            map.Click += async (sender, e) =>
            {
                select = "location";

            };
            phone.Click += async (sender, e) =>
            {
                select = "cell";

            };

            if (response.IsSuccessStatusCode)
            {
                ////    var content = await response.Content.ReadAsStringAsync();
                ////    //var Items = JsonConvert.DeserializeObject<List<string>>(content);

                ////    TextView name = FindViewById<TextView>(Resource.Id.NameText);

                ////    string temp = content;
                ////    name.Text = temp;

                ////}


                JsonValue json = await FetchAsync(url);
                ParseAndDisplay(json, select);
            }
        }

    
            public async Task<JsonValue> FetchAsync(string url)
        {
          
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

           
            using (WebResponse response = await request.GetResponseAsync())
            {
               
                using (Stream stream = response.GetResponseStream())
                { 
                    JsonValue jsonD = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonD.ToString());
                    return jsonD;
                }
            }
        }

        private void ParseAndDisplay(JsonValue json, string select)
        {
            // Get the weather reporting fields from the layout resource: 
            TextView name = FindViewById<TextView>(Resource.Id.NameLabel);
            TextView Detail = FindViewById<TextView>(Resource.Id.DescText);


            JsonValue Results = json["results"];

           
            name.Text = Results["name"];

            if (select != null)
            {
                Detail.Text = Results[select];
            };


        }

      
    }
}







