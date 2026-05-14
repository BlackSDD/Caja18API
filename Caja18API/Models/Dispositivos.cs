using System.ComponentModel.DataAnnotations;

namespace Caja18API.Models
{
    public class Dispositivos
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object>? Data { get; set; }
    }

    public class DispositivoPost
    {
        [Required]
        public string Name { get; set; }
        public Dictionary<string, object>? Data { get; set; }
    }
}
