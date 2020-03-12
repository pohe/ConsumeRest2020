using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelStandardLibrary;
using Newtonsoft.Json;

namespace ConsumeRest2020
{
    public class Worker
    {

        private const string URI = "http://localhost:53420/api/hotels";
        public void Start()
        {
            //List<Hotel> hoteller = GetHotels();
            //hoteller= GetHotels();
            //foreach (Hotel hotel in hoteller)
            //{
            //    Console.WriteLine("Hotel::" + hotel);
            //}
            //Console.WriteLine("Hent hotel no 3 :"  +  GetOneHotel(3));
            //Console.WriteLine("Delete hotel nr 100");
            //Console.WriteLine("Hotel nr 100 deleted:" + Delete(100));
            //Hotel newHotel = new Hotel(101, "ZoomHotel", "ZoomStreet 123");
            //bool ok = post(newHotel);
            //Console.WriteLine("Har oprettet hotel nr 101 " +  ok);

            Hotel newOpdateretHotel = new Hotel(101, "ZoomOpdateretHotel", "NewStreet 123");
            bool ok = put(101, newOpdateretHotel);
            Console.WriteLine("Har opdateret hotel nr 101 " + ok);

            List<Hotel> hoteller = GetHotels();
            hoteller = GetHotels();
            foreach (Hotel hotel in hoteller)
            {
                Console.WriteLine("Hotel::" + hotel);
            }
        }

        public List<Hotel> GetHotels()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (HttpClient client = new HttpClient())
            {
                Task<string> stringAsync = client.GetStringAsync(URI);
                string jsonString = stringAsync.Result;
                hoteller= JsonConvert.DeserializeObject<List<Hotel>>(jsonString);
            }
            return hoteller;
        }


        public  Hotel GetOneHotel(int id)
        {
            Hotel hotel = new Hotel();
            using (HttpClient client = new HttpClient())
            {
                Task<string> stringAsync = client.GetStringAsync(URI + "/"+ id);
                string jsonString = stringAsync.Result;
                hotel = JsonConvert.DeserializeObject<Hotel>(jsonString);
            }
            return hotel;
        }

        public bool Delete(int id)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);
                HttpResponseMessage resp = deleteAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonStr = resp.Content.ReadAsStringAsync().Result;
                    ok= JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }
            return ok;
        }

        public bool post(Hotel hotel)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString=JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);
                HttpResponseMessage resp = postAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonStr = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }
            return ok;
        }


        public bool put(int id, Hotel hotel)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> putAsync = client.PutAsync(URI+ "/"+ id, content);
                HttpResponseMessage resp = putAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonStr = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }
            return ok;
        }
    }
}