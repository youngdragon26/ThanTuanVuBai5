using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vuxahihi.Models;

namespace Vuxahihi.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(string searchString)
        {
            var data = from p in Data.SeedData.Products
                       join c in Data.SeedData.Categories on p.CategoryId equals c.CategoryId
                       select new Models.ProductViewModel
                       {
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           Price = p.Price,
                           CategoryName = c.CategoryName
                       };
            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(p => p.ProductName.Contains(searchString));
            }

            // giữ lại giá trị search để hiển thị lại trên view
            ViewBag.SearchString = searchString;
            return View(data);
        }
        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            var category = Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            var productViewModel = new Models.ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryName = category.CategoryName
            };
            return View(productViewModel);
        }
        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName");
            return View();
        }
        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                // Simulate adding the new product to the database
                ////Tao id moi cho san pham moi bang cach tu dong tang them 1 vao id lon nhat hien co 
                int newProductId = Data.SeedData.Products.Max(p => p.ProductId) + 1;
                newProduct.ProductId = newProductId;//Gan id moi cho san pham moi
                Data.SeedData.Products.Add(newProduct);//Them san pham moi vao danh sach san pham
                return RedirectToAction(nameof(Index));//Chuyen huong ve trang danh sach san pham sau khi them moi thanh cong
            }
            //Neu du lieu khong hop le, hien thi lai form tao san pham va truyen lai danh sach danh muc de nguoi dung chon lai
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", newProduct.CategoryId);
            return View(newProduct);//Truyen lai doi tuong san pham moi de hien thi lai form va bao loi neu co loi xay ra trong qua trinh tao san pham moi
        }
        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);//Tim san pham can chinh sua theo id
            if (product == null)//Neu khong tim thay san pham, tra ve trang loi 404 Not Found
            {
                return NotFound();
            }
            //Neu tim thay san pham, hien thi form chinh sua san pham va truyen danh sach danh muc de nguoi dung chon lai neu muon thay doi danh muc cua san pham
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product EditProduct)
        {
            if (ModelState.IsValid)
            {
                //Simulate cap nhat san pham trong csdl
                //Tim san pham can cap nhat theo id va cap nhat lai thong tin san pham
                var existingProduct = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == EditProduct.ProductId);
                //Neu tim thay san pham, cap nhat lai thong tin san pham
                if (existingProduct != null)
                {
                    existingProduct.ProductName = EditProduct.ProductName;
                    existingProduct.Price = EditProduct.Price;
                    existingProduct.CategoryId = EditProduct.CategoryId;
                }
                return RedirectToAction(nameof(Index));
            }
            //Neu du lieu khong hop le, hien thi lai form chinh sua san pham va truyen lai danh sach danh muc de nguoi dung chon lai neu muon thay doi danh muc cua san pham
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", EditProduct.CategoryId);
            return View();

        }
        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);//Tim san pham can xoa theo id
            if (product == null)//Neu khong tim thay san pham, tra ve trang loi 404 Not Found
            {
                return NotFound();
            }
            return View(product);
        }
        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);//Tim san pham can xoa theo id
            if (product == null)//Neu ko tim thay san pham, hien thi 404
            {
                return NotFound();
            }
            Data.SeedData.Products.Remove(product);//Xoa san pham khoi danh sach san pham
            return RedirectToAction(nameof(Index));

        }

    }
}

    

