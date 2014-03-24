using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Transactions;
using FileStreams.Data.ServiceModel;
using FileStreams.Model;

namespace FileStreams.Data.Services
{
    public class PhotoService : IRepository<Photo>
    {
        private const string RowDataStatement = @"SELECT Data.PathName() AS 'Path', GET_FILESTREAM_TRANSACTION_CONTEXT() AS 'Transaction' FROM {0} WHERE Id = @id";

        public IList<Photo> GetAll()
        {
            using (var context = new FileStreamContext())
            {
                return context.Photos.ToList();
            }
        }

        public Photo GetById(int id)
        {
            using (var context = new FileStreamContext())
            {
                var photo = context.Photos.FirstOrDefault(p => p.Id == id);
                if (photo == null)
                {
                    return null;
                }

                using (var tx = new TransactionScope())
                {

                    var selectStatement = String.Format(RowDataStatement, context.GetTableName<Photo>());

                    var rowData =
                        context.Database.SqlQuery<FileStreamRowData>(selectStatement, new SqlParameter("id", id))
                            .First();

                    using (var source = new SqlFileStream(rowData.Path, rowData.Transaction, FileAccess.Read))
                    {
                        var buffer = new byte[16*1024];
                        using (var ms = new MemoryStream())
                        {
                            int bytesRead;
                            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, bytesRead);
                            }
                            photo.Data = ms.ToArray();
                        }
                    }

                    tx.Complete();
                }

                return photo;
            }
        }

        public void Update(Photo entity)
        {
            using (var context = new FileStreamContext())
            {
                using (var tx = new TransactionScope())
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();

                    SavePhotoData(context, entity);

                    tx.Complete();
                }
            }
        }

        public void Insert(Photo entity)
        {
            using (var context = new FileStreamContext())
            {
                using (var tx = new TransactionScope())
                {
                    context.Photos.Add(entity);
                    context.SaveChanges();

                    SavePhotoData(context, entity);

                    tx.Complete();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new FileStreamContext())
            {
                context.Entry(new Photo { Id = id}).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private static void SavePhotoData(FileStreamContext context, Photo entity)
        {
            var selectStatement = String.Format(RowDataStatement, context.GetTableName<Photo>());

            var rowData =
                context.Database.SqlQuery<FileStreamRowData>(selectStatement, new SqlParameter("id", entity.Id))
                    .First();

            using (var destination = new SqlFileStream(rowData.Path, rowData.Transaction, FileAccess.Write))
            {
                var buffer = new byte[16 * 1024];
                using (var ms = new MemoryStream(entity.Data))
                {
                    int bytesRead;
                    while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destination.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}
