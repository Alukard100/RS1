﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.UserType
{
    public class GetUserTypeResponse
    {
        public int TypeId { get; set; }
        public string? Type { get; set; }
    }
}
