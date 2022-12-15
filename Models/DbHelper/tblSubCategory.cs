using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Weavers.Common.Models.Entities;
using Weavers.Data;

using Weavers.Models.Sql;

namespace Weavers.Models.DbHelper
{
    public static class tblSubCategory
    {
        public static ICollection<SubCategoryEntity> Get(int categoryID)
        {
            var entities = new Collection<SubCategoryEntity>();

            var query = categoryID==0? UserSqlQueries.SubCategoryRetrieve : string.Format(UserSqlQueries.SubCategoryRetrieveById, categoryID);
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                entities.Add(new SubCategoryEntity
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Category =new CodeValueEntity { Id = Convert.ToInt32(reader[1].ToString()), Name=reader[4].ToString() },
                    Name = reader[2].ToString(),
                    Status = Convert.ToInt32(reader[3].ToString()),
                    Telugu=reader[5].ToString(),
                    Tamil=reader[6].ToString(),
                    Hindi=reader[7].ToString(),
                    kannad=reader[8].ToString()
                });
            }

            return entities;
        }
    }
}