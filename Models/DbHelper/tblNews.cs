using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Weavers.Common.Models.Entities;
using Weavers.Data;
using Weavers.Models.Helpers;
using Weavers.Models.Sql;

namespace Weavers.Models.DbHelper
{
    public class tblNews
    {
        //INSERT INTO tblNews(NewsId, Title, ShortDescription, Image, CreatedBy, CreatedOn, Url) 
        public static ICollection<NewsEntity> Get(string userType)
        {
            var entities = new Collection<NewsEntity>();

            var query = userType=="All" ? UserSqlQueries.AllNewsSelect : string.Format(UserSqlQueries.NewsSelect, userType);
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
                entities.Add(new NewsEntity
                {
                    Id = Convert.ToInt32(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    ShortDescription = reader[2].ToString(),
                    Images = imageCollection,
                    CreatedBy = tblUserBasic.GetUserFullDetail(reader[4].ToString()),
                    CreatedOn = reader[5].ToString(),
                    Url = reader[6].ToString(),
                    IsBookMarked = !reader.IsDBNull(9) && Convert.ToInt32(reader[9]) == 1,
                    Range = reader[10].ToString()
                });
            }

            return entities?.OrderByDescending(s => s.ParsedDateValue).ToList();
        }

        public static ICollection<VideoEntity> GetVideo(string userType)
        {
            var entities = new Collection<VideoEntity>();

            var query = string.Format(UserSqlQueries.VideoSelect, userType);
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                var imageList = reader[8].ToString();
                var imageCollection = new Collection<string>();
                if (!string.IsNullOrEmpty(imageList))
                {
                    var imgList = imageList.Split(';');
                    foreach (var item in imgList)
                    {
                        imageCollection.Add(item);
                    }
                }

                entities.Add(new VideoEntity
                {
                    Id = Convert.ToInt32(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    ShortDescription = reader[2].ToString(),
                    CreatedBy = tblUserBasic.GetUserFullDetail(reader[3].ToString()),
                    CreatedOn = reader[4].ToString(),
                    Url = reader[5].ToString(),
                    Images = imageCollection,
                    IsBookMarked = !reader.IsDBNull(9) && Convert.ToInt32(reader[9]) == 1
                });
            }
            return entities?.OrderByDescending(s => s.ParsedDateValue).ToList();
        }

        public static ReportItemEntity Insert(ReportItemEntity entity, string imgPath)
        {
            var query = "";

            switch (entity.ReportType)
            {
                case ReportType.news:

                    query = string.Format(UserSqlQueries.NewsInsert, entity.ShortDescription, entity.DetailDescription, imgPath,
                                        entity.UserId, entity.ReportedOn, entity.Url, entity.Range);
                    break;
                case ReportType.alert:
                    query = string.Format(UserSqlQueries.AlertInsert, entity.ShortDescription, entity.DetailDescription, imgPath,
                                       entity.UserId, entity.ReportedOn, entity.Url, entity.Range);
                    break;
                case ReportType.videos:
                    query = string.Format(UserSqlQueries.VideoInsert, entity.ShortDescription, entity.DetailDescription,
                                      entity.UserId, entity.ReportedOn, entity.Url, imgPath);
                    break;
                default:

                    break;
            }
            if (!string.IsNullOrEmpty(query))
            {
                try
                {
                    entity.ID = SaveData.SaveAndGetId(query);
                }
                catch (Exception ex)
                {

                }
            }

            return entity;
        }


        public static void DeleteNews(int newsId)
        {
            var query = string.Format(UserSqlQueries.NewsDelete, newsId);

            SaveData.Save(query);

        }

        public static void BookMarkItem(BookMarkItemEntity bookMarkItemEntity)
        {
            var query = "";
            var bookMarkType = bookMarkItemEntity.Type.ToString();

            switch (bookMarkItemEntity.Type)
            {
                case ReportType.news:
                    query = string.Format(UserSqlQueries.BookMarkNews, bookMarkItemEntity.BookMarked ? 1 : 0, bookMarkItemEntity.ItemId);
                    break;
                case ReportType.alert:
                    query = string.Format(UserSqlQueries.BookMarkAlert, bookMarkItemEntity.BookMarked ? 1 : 0, bookMarkItemEntity.ItemId);
                    break;
                case ReportType.videos:
                    query = string.Format(UserSqlQueries.BookMarkVideo, bookMarkItemEntity.BookMarked ? 1 : 0, bookMarkItemEntity.ItemId);
                    break;
                case ReportType.shops:
                case ReportType.member:
                    query = string.Format(UserSqlQueries.UserBookmarkUpdate, bookMarkItemEntity.BookMarked ? 1 : 0, bookMarkItemEntity.ItemId);
                    break;
                default:
                    bookMarkType = "report";
                    query = string.Format(UserSqlQueries.BookMarReportItem, bookMarkItemEntity.BookMarked ? 1 : 0, bookMarkItemEntity.ItemId);
                    break;
            }

            query = string.Format(UserSqlQueries.UserItemBookMarkInsert, bookMarkType, bookMarkItemEntity.ItemId, bookMarkItemEntity.UserId, bookMarkItemEntity.BookMarked ? 1 : 0);

            if (!string.IsNullOrEmpty(query))
            {
                try
                {
                    SaveData.Save(query);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static BookMarkList GetBookMarkItems(string userId)
        {
            var entities = new BookMarkList { BookMarkItems = new Collection<BookMarkItem>() };
            var bookMarkCollection = new Collection<BookMarkItem>();
            var query = string.Format(UserSqlQueries.UserItemBookMarkSelect, userId);
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();
            // BookMarkId  BookMarkType, BookMarkItemId, UserId, IsBookMarked
            while (reader.Read())
            {
                var parsedType = (ReportType)Enum.Parse(typeof(ReportType), reader[1].ToString());
                var existingEntity = bookMarkCollection.FirstOrDefault(s => s.Type == parsedType);
                if (existingEntity == null)
                {
                    var bookmark = new BookMarkItem
                    {
                        Type = parsedType,
                        BookMarkItems = new List<CodeValueEntity>() {
                            new CodeValueEntity {
                                Id=Convert.ToInt32(reader[2].ToString()),
                                Name = reader[1].ToString()
                            } 
                        }
                    };
                    bookMarkCollection.Add(bookmark);
                }
                else
                {
                    existingEntity.BookMarkItems.Add(new CodeValueEntity
                    {
                        Id = Convert.ToInt32(reader[2].ToString()),
                        Name = reader[1].ToString()
                    });
                }

            }

            entities.BookMarkItems = bookMarkCollection;

            return entities;
        }


        public static Dictionary<string, string> BookMarkItemsCount(string userId)
        {
            var entities = new Dictionary<string, string>();

            var query = string.Format(UserSqlQueries.BookMarkCount, userId);
            
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();
            
            while (reader.Read())
            {
                var parsedType = (ReportType)Enum.Parse(typeof(ReportType), reader[0].ToString());

                entities.Add(parsedType.ToString(), reader[1].ToString());

            }

            

            return entities;
        }

    }
}