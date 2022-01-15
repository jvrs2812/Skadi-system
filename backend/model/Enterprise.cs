using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace backend.model
{

    public class Enterprise
    {
        [Required]
        public string id { get; set; }
        [Required]
        public NameEnterprise settings { get; set; }
        [Required]
        public string cnpj { get; set; }
        [Required]
        public string stateregistration { get; set; }
        [Required]
        public string telephone { get; set; }
        [Required]
        public string email { get; set; }

    }

    public class NameEnterprise
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string slug { get; set; }
        [Required]
        public string namefantasy { get; set; }
    }
}