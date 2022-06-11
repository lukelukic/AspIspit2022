using System;
using System.Collections.Generic;

#nullable disable

namespace AspIspit2022.DataAccess.Models
{
    public partial class UserUseCase
    {
        public int UserId { get; set; }
        public int UseCaseId { get; set; }

        public virtual User User { get; set; }
    }
}
