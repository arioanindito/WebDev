using WebDev.Models;
using WebDev.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebDev.Repository
{
    public class PostRepository : IPost
    {
        private DBContext db;
        private IConfiguration _configuration;
        public PostRepository(DBContext _db, IConfiguration configuration)
        {
            db = _db;
            _configuration = configuration;
        }

        public IEnumerable<Post> GetPosts => db.Posts;

        public void Add(Post Post, IFormFile photo)
        {
            if (Post.PostId == 0)
            {
                //Post post = new Post();
                //post.UserName = user.UserName;
                db.Posts.Add(Post);
                db.SaveChanges();

                if (photo != null)
                {
                    string imagesPath = _configuration.GetValue<string>("PostPhotoLocation");

                    int newImageIndex = 0;

                    string directoryPath = Path.Combine(imagesPath, Post.PostId.ToString());
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    string fileName = string.Format("{0}.jpg", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
                    string filePath = Path.Combine(directoryPath, fileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }

                    Image image = new Image();
                    image.PostId = Post.PostId;
                    image.Index = newImageIndex;
                    image.FileName = fileName;
                    //image.Name = Post.PostName;
                    db.Images.Add(image);
                    
                    db.SaveChanges();
                }
            }
            else
            {
                var dbEntity = db.Posts.Find(Post.PostId);
                //dbEntity.PostName = Post.PostName;
                dbEntity.Comment = Post.Comment;
                //dbEntity.Images = Post.Images;
                db.SaveChanges();

                //if (photo != null)
                //{
                    //string imagesPath = _configuration.GetValue<string>("PostPhotoLocation");

                    //int newImageIndex = 0;

                    //string directoryPath = Path.Combine(imagesPath, Post.PostId.ToString());
                    //if (!Directory.Exists(directoryPath))
                    //{
                    //    Directory.CreateDirectory(directoryPath);
                    //}

                    //string fileName = string.Format("{0}.jpg", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
                    //string filePath = Path.Combine(directoryPath, fileName);

                    //using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    photo.CopyTo(fileStream);
                    //}

                    //Image image = new Image();
                    //image.PostId = Post.PostId;
                    //image.Index = newImageIndex;
                    //image.FileName = fileName;
                    //image.Name = Post.PostName;
                    //db.Images.Add(image);

                    //db.SaveChanges();
                //}
            }
        }
        public Post GetPost(int? ID)
        {
            return db.Posts
                .Include(s => s.Images)
                .SingleOrDefault(a => a.PostId == ID);
        }

        public void Remove(int? ID)
        {
            Post dbEntity = db.Posts.Find(ID);
            db.Posts.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
