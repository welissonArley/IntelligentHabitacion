﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligentHabitacion.Api.Repository.Model
{
    [Table("HomeAssociation")]
    public class HomeAssociation : ModelBase
    {
        [ForeignKey("HomeId")]
        public Home Home { get; set; }
        public long HomeId { get; set; }
        public DateTime JoinedOn { get; set; }
        public ICollection<User> Users { get; set; }

        public override void Decrypt()
        {
            Home.Decrypt();
        }

        public override void Encrypt()
        {
            Home.Encrypt();
        }
    }
}