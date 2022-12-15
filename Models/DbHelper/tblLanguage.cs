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
    public static class tblLanguage
    {
                
        public static ICollection<LanguageEntity> Get()
        {
            var entities = new Collection<LanguageEntity>();

            var query = UserSqlQueries.LanguageRetrieve;
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                entities.Add(new LanguageEntity
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Code = reader[1].ToString(),
                    Name = reader[2].ToString(),
                    AnsiChar=reader[3].ToString(),
                    LangCode=reader[5].ToString(),
                    Status = Convert.ToInt32(reader[4].ToString())
                });
            }

            return entities;
        }
    }
}