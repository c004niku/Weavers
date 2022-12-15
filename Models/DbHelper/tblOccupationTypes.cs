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
    public static class tblOccupationTypes
    {
        public static ICollection<OccupationTypes> Get()
        {
            var entities = new Collection<OccupationTypes>();

            //Status
            var query = UserSqlQueries.OccupationTypesRetrieve;
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                entities.Add(new OccupationTypes
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Name = reader[1].ToString(),
                    Status = Convert.ToInt32(reader[2].ToString()),
                    Hindi = reader[3].ToString(),
                    kannad = reader[4].ToString(),
                    Tamil = reader[5].ToString(),
                    Telugu = reader[6].ToString()
                });
            }

            return entities;
        }
    }
}