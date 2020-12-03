﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string [] Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
