using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace DomainModel.Concrete
{
    public class PostRepository
    {
        EFContext context;
        public PostRepository(EFContext context)
        {
            this.context = context;
        }
        public IQueryable<Product> GetAllProduct()
        {
            return from data in context.Products orderby data.Id descending select data;
        }
        public bool AddProduct(string title,string description, HttpPostedFileBase[] Images, int quantity,bool isIn,int categoryId,double price,string path,string miniImages)
        {
            Product product = new Product() { Title = title, Description = description, Quantity = quantity, IsIn = isIn, CategoryId = categoryId, AddTime = DateTime.Now,Price=price};
            context.Products.Add(product);
            context.SaveChanges();
            SaveImages(Images, product.Id, path,1080,1920, miniImages, 400, 600);
            context.SaveChanges();
            return true;
        }
        public void EditProduct(int id, string title, string description, int quantity, bool isIn, HttpPostedFileBase[] Image, int categoryId,double price, string path, string miniImages)
        {
            Product product = GetProductById(id);
            product.Title = title;
            product.Description = description;
            product.Quantity = quantity;
            product.IsIn = isIn;
            product.CategoryId = categoryId;
            product.Price = price;
            SaveImages(Image, id, path,1080,1920, miniImages, 400, 600);
            context.SaveChanges();
        }
        public Images SaveImage(string name,string miniName, int productID)
        {
            Images img = new Images() { Name = name,ProductID=productID,MiniImage=miniName };
            context.Images.Add(img);
            context.SaveChanges();
            return img;
        }
        public Product GetProductById(int id)
        {
            return context.Products.Include(p=>p.Image).SingleOrDefault(p => p.Id == id);
        }

        public void DeleteImages(int postId,string path,string miniPath)
        {
            Product product = GetProductById(postId);
            var delete = product.Image.ToList();
            foreach (var e in delete)
            {
                DeleteImage(path + e.Name);
                DeleteImage(miniPath + e.MiniImage);
                DeleteImageById(e);
            }
        }
        public void DeleteImage(string path)
        {
            System.IO.File.Delete(path);
        }

        private void SaveImages(HttpPostedFileBase[] Images,int postId,string path,int height,int width,string miniPath,int miniHeight,int miniWidth)
        {
            foreach (var img in Images)
            {
                string name = Path.GetFileName(img.FileName);
                string miniName = Path.GetFileName(img.FileName);
                Bitmap imgs = WorkImage.CreateImage(img, width, height);
                Bitmap miniImg = WorkImage.CreateImage(img, miniHeight, miniWidth);
                int i = 1;
                if (imgs != null)
                {                   
                    string[] dirs = Directory.GetFiles(path, name);
                    while (dirs.Length != 0)
                    {
                        name = "copy-" + i + "-" + Path.GetFileName(img.FileName);
                        i++;
                        dirs = Directory.GetFiles(path, name);
                    }                         
                    string filename = path + name;
                    imgs.Save(filename, ImageFormat.Jpeg);
                }
                int h = 1;
                if (miniImg!=null)
                {
                    string[] miniDirs = Directory.GetFiles(miniPath, miniName);
                    while (miniDirs.Length != 0)
                    {
                        miniName = "copy-" + h + "-" + Path.GetFileName(img.FileName);
                        h++;
                        miniDirs = Directory.GetFiles(miniPath, miniName);
                    }
                    string filename = miniPath + miniName;
                    miniImg.Save(filename, ImageFormat.Jpeg);
                }
                SaveImage(name, miniName, postId);
            }
        }
        public void DeleteImageById(Images img)
        {
            context.Images.Remove(img);
        }
        public void RemoveProduct(Product product,string path,string miniPath)
        {
            DeleteImages(product.Id, path, miniPath);
            context.Products.Remove(product);
            context.SaveChanges();
        }
        public Basket AddBasketToUser(User user)
        {
            Basket basket = new Basket();
            basket.User = user;
            context.Basket.Add(basket);
            context.SaveChanges();
            return basket;
        }

        public ICollection<ProductInBusket> GetAllProductInBasket(string emailUser)
        {
            var p = context.Basket.Include(d => d.Products).SingleOrDefault(u => u.User.email == emailUser);
            return p.Products;
        }

        public bool AddProductToCart(int productId,int quantity,User user)
        {
            Basket basket = user.Basket;
            if (user.Basket==null)
            {
                basket = AddBasketToUser(user);
            }
            var p = context.Basket.Include(d => d.Products).SingleOrDefault(u => u.User.id == user.id);
            foreach(var prod in p.Products)
            {
                if (prod.ProductId==productId)
                {
                    prod.Quantity += quantity;
                    context.SaveChanges();
                    return true;
                }
            }
            ProductInBusket product = new ProductInBusket() { Product = GetProductById(productId), Quantity = quantity,Basket=basket };
            context.ProductsInBasket.Add(product);
            context.SaveChanges();
            user.Basket.Products.Add(product);
            context.SaveChanges();
            return true;

        }
        public bool DeleteProductFromBasket(int id,User user)
        {
            Basket basket = context.Basket.Include(pr=>pr.Products).SingleOrDefault(u => u.User.id == user.id);
            context.ProductsInBasket.Remove(basket.Products.SingleOrDefault(p => p.ProductId == id));
            context.SaveChanges();
            return true;
        }
        public int GetCountOfBasket(User user)
        {
            return GetAllProductInBasket(user.email).Count;
        }
        public double TotalBasketPrice(User user)
        {
            var allProduct = GetAllProductInBasket(user.email);
            double price=0;
            foreach(var p in allProduct)
            {
                price = price + GetProductById(p.ProductId).Price * p.Quantity;
            }
            return price;
        }
    }
}
