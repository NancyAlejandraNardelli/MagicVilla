﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_api.Modelos.Dto
{
    public class NumeroVillaCreateDto
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }

        public string DetalleEspecial { get; set; }
    }
}
