using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weavers.Common.Models.Entities;
public class NewsModel
{
    public ICollection<NewsEntity> Reports { get; set; }
}