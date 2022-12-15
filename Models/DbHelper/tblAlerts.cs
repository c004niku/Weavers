using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Weavers.Data;
using Weavers.Common.Models.Entities;
using Weavers.Models.Sql;

namespace Weavers.Models.DbHelper
{
    public class tblAlerts
    {

        //INSERT INTO tblAlerts(AlertId, Title, ShortDescription, Image, CreatedBy, CreatedOn, Url) VALUES ([value-1],[value-2],[value-3],[value-4],[value-5],[value-6],[value-7])
        public static ICollection<AlertEntity> Get(string userType)
        {
            var entities = new Collection<AlertEntity>();

            var query = string.Format(UserSqlQueries.AlertSelect,userType);
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                var imageList = reader[3].ToString();
                var imageCollection = new Collection<string>();
                if (!string.IsNullOrEmpty(imageList))
                {
                    var imgList = imageList.Split(';');
                    foreach (var item in imgList)
                    {
                        imageCollection.Add(item);
                    }
                }

                entities.Add(new AlertEntity
                {
                    Id = Convert.ToInt32(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    ShortDescription = reader[2].ToString(),
                    Images = imageCollection,
                    CreatedBy = tblUserBasic.GetUserFullDetail(reader[4].ToString()),
                    CreatedOn = reader[5].ToString(),
                    Url = reader[6].ToString(),
                    IsBookMarked = !reader.IsDBNull(9) && Convert.ToInt32(reader[9]) == 1,
                    Range = reader[10].ToString(),
                });
            }

            return entities?.OrderByDescending(s => s.ParsedDateValue).ToList(); ;
        }
        
    }
}