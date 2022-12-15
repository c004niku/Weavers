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
    public static class tblCategory
    {
        public static ICollection<CategoryEntity> Get(int languageId)
        {
            var entities = new Collection<CategoryEntity>();
            
            var query = UserSqlQueries.CategoryRetrieve;
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                var name = reader[1].ToString();
                switch (languageId)
                {
                    case 2:
                        name = reader[5].ToString();
                        break;
                    case 3:
                        name = reader[3].ToString();
                        break;
                    case 4:
                        name = reader[6].ToString();
                        break;
                    case 5:
                        name = reader[4].ToString();
                        break;
                    default:
                        name = reader[1].ToString();
                        break;
                }

                entities.Add(new CategoryEntity
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Name = name,
                    Status = Convert.ToInt32(reader[2].ToString()),
                   
                });
            }

            return entities;
        }
    }
}