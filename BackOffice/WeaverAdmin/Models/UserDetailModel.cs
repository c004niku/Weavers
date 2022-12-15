using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weavers.Common.Models.Entities;

public class UserDetailModel
{
    public ICollection<UserEntity> UserEntities { get; set; }
}