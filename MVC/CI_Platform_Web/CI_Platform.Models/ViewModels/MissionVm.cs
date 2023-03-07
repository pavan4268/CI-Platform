﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.ViewModels
{
    public class MissionVm
    {
        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; }

        public string OrganizationName { get; set; }

        public int Rating { get; set; }

        public string Img { get; set; }

        public string MissionThemes { get; set; }

        public string CityName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; } 

        public int NumberOfSeats { get; set; }

        public string Deadline { get; set; }
    }
}
