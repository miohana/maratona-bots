using Newtonsoft.Json;

namespace MaratonaBots.Models
{
    // Usar "Paste Especial" permite que classes sejam criadas direto de conteúdo XML!
    public class Cotacao
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("sigla")]
        public string Sigla { get; set; }

        [JsonProperty("valor")]
        public float Valor { get; set; }
    }

}