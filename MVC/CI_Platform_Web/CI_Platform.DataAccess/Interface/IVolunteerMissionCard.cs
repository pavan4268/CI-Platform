﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Repository.Interface
{
    public interface IVolunteerMissionCard
    {
        public VolunteerMissionVm GetMission(long? id);
    }
}
