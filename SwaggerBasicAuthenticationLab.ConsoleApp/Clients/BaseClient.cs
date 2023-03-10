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
        public BaseClient(ClientConfiguration configuration)
        {

        }
        protected void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings)
        {
           // settings.Converters.Add(new DateOnlyJsonConverter());
        }

        protected Task PrepareRequestAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url, CancellationToken token)
        {
            return Task.CompletedTask;
        }
        protected Task PrepareRequestAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder, CancellationToken token)
        {
            return Task.CompletedTask;
        }
        protected Task ProcessResponseAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response, CancellationToken token)
        {
            return Task.CompletedTask;
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
