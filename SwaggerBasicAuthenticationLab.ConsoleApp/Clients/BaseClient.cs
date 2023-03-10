using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwaggerBasicAuthenticationLab.ConsoleApp.Clients
{
    public class BaseClient
    {
        public BusyControl Busy { get; set; }
        public BaseClient(ClientConfiguration configuration)
        {

        }
        protected void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings)
        {
            // settings.Converters.Add(new DateOnlyJsonConverter());
        }

        protected Task PrepareRequestAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url, CancellationToken token)
        {
            Console.WriteLine("PrepareRequestAsync");
            return Task.CompletedTask;
        }
        protected Task PrepareRequestAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder, CancellationToken token)
        {
            return Task.CompletedTask;
        }
        protected Task ProcessResponseAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response, CancellationToken token)
        {
            Console.WriteLine("ProcessResponseAsync");
            return Task.CompletedTask;
        }



        /// <summary>
        /// 建立Request
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<MyHttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            //string accessToken = !string.IsNullOrWhiteSpace(AccessToken) ? AccessToken : UgozConfig.AccessToken;
            Busy.IsBusy = true;
            var request = new MyHttpRequestMessage(Busy);
            //request.Headers.Add("Authorization", $"Bearer {accessToken}");
            return await Task.FromResult(request);
        }

    }

    public class MyHttpRequestMessage : HttpRequestMessage
    {
        private BusyControl Busy { get; set; }
        public MyHttpRequestMessage(BusyControl busy)
        {
            Busy = busy;
        }

        protected override void Dispose(bool disposing)
        {
            Busy.IsBusy= false;
            base.Dispose(disposing);
        }

    }

    public class DateOnlyJsonConverter : JsonConverter<System.DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override System.DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return System.DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, System.DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
