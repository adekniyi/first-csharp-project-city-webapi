﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Models
{
    public class PointOfInterestForUpdatingDto
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]
        public string Name { get; set; }


        [MaxLength(200)]
        public string Description { get; set; }
    }
}
