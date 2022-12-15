using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Weavers.Data;
using Weavers.Common.Models.Entities;
using Weavers.Models.Sql;
using Weavers.Common.Models;

namespace Weavers.Models.DbHelper
{

    //SELECT `UserId`, `ReportedOn`, `ShortDescription`, `Status`, `Action`, `DetailDescription` FROM `tblReportItem` WHERE 1
    public class tblReportItem
    {
        public static ICollection<ReportItemEntity> Get(int userId)
        {
            var entities = new Collection<ReportItemEntity>();

            var query = userId == 0 ? UserSqlQueries.ReportItemSelect : string.Format(UserSqlQueries.ReportItemSelectByUser, userId);
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                Enum.TryParse(reader[7].ToString(), out ReportType report);

                Enum.TryParse(reader[4].ToString(), out ReportStatusType reportStatus);

                entities.Add(new ReportItemEntity
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    UserId = Convert.ToInt32(reader[1].ToString()),
                    ReportedOn = reader[2].ToString(),
                    ShortDescription = reader[3].ToString(),
                    Status = reportStatus,
                    Action = "Completed",
                    DetailDescription = reader[6].ToString(),
                    ReportType = report,
                    Images = GlobalConstants.GetReportItemImagePath(reader[8].ToString()),// reader[8].ToString(),
                    Url = reader[9].ToString(),
                    IsBookMarked = !reader.IsDBNull(10) && Convert.ToInt32(reader[10]) == 1
                }); ;
            }

            return entities?.OrderByDescending(s=>s.ParsedReportedValue).ToList();
        }


        public static ICollection<NewsEntity> SearchItem(string searchText, ReportType type, string userType)
        {
            var entities = new Collection<NewsEntity>();

            if (type == ReportType.shops)
            {
                //var shopLists= tblUserBasic.GetUsersByCategory(1, searchText);
                //foreach (var item in shopLists)
                //{
                //    entities.Add(new NewsEntity
                //    {
                //        Id = item.ID,
                //        Title = string.IsNullOrEmpty(item.ShopName)?item.User.Name:item.ShopName,
                //        ShortDescription = item.ProfileDescription + item.SubCategory.Name,
                //        Images = item.ShopImages,
                //        CreatedBy = item.User,
                //        CreatedOn = item.MemberSince.ToShortDateString(),
                //    });
                //}
            }
            else
            {

                var query = string.Format(UserSqlQueries.SearchReportQuery, searchText,userType);

                switch (type)
                {
                    case ReportType.news:

                        query = string.Format(UserSqlQueries.SearchNewsQuery, searchText, userType);
                        break;
                    case ReportType.alert:
                        query = string.Format(UserSqlQueries.SearchAlertQuery, searchText, userType);
                        break;
                    case ReportType.videos:
                        query = string.Format(UserSqlQueries.SearchVideoQuery, searchText, userType);
                        break;
                    default:
                        query = string.Format(UserSqlQueries.SearchReportQuery, searchText, userType);
                        break;
                }

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
                            imageCollection.Add(GlobalConstants.GetReportItemImagePath(item));
                        }
                    }
                    entities.Add(new NewsEntity
                    {
                        Id = Convert.ToInt32(reader[0].ToString()),
                        Title = reader[1].ToString(),
                        ShortDescription = reader[2].ToString(),
                        Images = imageCollection,
                        CreatedBy = tblUserBasic.GetUserFullDetail(reader[4].ToString()),
                        CreatedOn = reader[5].ToString(),
                        Url = reader[6].ToString(),
                        IsBookMarked=!reader.IsDBNull(9)&& Convert.ToInt32(reader[9])==1
                    });
                }
            }

            return entities;
        }


        public static ReportItemEntity InserReport(ReportItemEntity entity, string imgPath)
        {
            var query = string.Format(UserSqlQueries.ReportItemInsert, entity.UserId, entity.ReportedOn, entity.ShortDescription,
                                      entity.Status.ToString(), entity.Action, entity.DetailDescription, entity.ReportType, imgPath, entity.Url);
            try
            {
                entity.ID = SaveData.SaveAndGetId(query);
            }
            catch (Exception ex)
            {

            }
           

            return entity;
        }

        public static bool DeleteReport(int id)
        {
            var itemDeleted = false;
            var query = string.Format(UserSqlQueries.DeleteReportItem, id,ReportStatusType.PENDING);
            try
            {
              var deleteRecord= SaveData.Save(query);
                itemDeleted = deleteRecord == 1;
            }
            catch (Exception ex)
            {

            }

            return itemDeleted;
        }
    }
}