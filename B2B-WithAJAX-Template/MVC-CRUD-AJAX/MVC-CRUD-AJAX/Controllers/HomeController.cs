using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using MVC_CRUD_AJAX.DAL;
using MVC_CRUD_AJAX.Models;
using MVC_CRUD_AJAX.ViewModels;
using Excel = Microsoft.Office.Interop.Excel;

namespace MVC_CRUD_AJAX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MyContext db = new MyContext();
            var productList = db.Products.ToList();
            return View(productList);
        }


        [HttpPost]
        public JsonResult InsertProduct(ImgViewModel model)
        {
            List<int> imagesId = new List<int>();
            int ImgsId = 0;
            var files = model.ImgFile;
            byte[] imageByte = null;
            Product pro = null;

            using (MyContext db = new MyContext())
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        //file.SaveAs(Server.MapPath("/Images/" + file.FileName));
                        BinaryReader reader = new BinaryReader(file.InputStream);
                        imageByte = reader.ReadBytes(file.ContentLength);

                        Images img = new Images();

                        img.ImgTitle = file.FileName;
                        img.ImgByte = imageByte;
                        //img.ImgPath = "/Images/" + file.FileName;
                        db.Images.Add(img);
                        db.SaveChanges();

                        imagesId.Add(img.Id);
                        ImgsId = img.Id;

                    }

                    var product = new Product();
                    product.Name = model.ProductName;
                    product.Unit = model.ProductUnit;
                    product.Price = model.ProductPrice;
                    product.ImgsId = ImgsId;
                    db.Products.Add(product);
                    db.SaveChanges();

                    pro = product;

                }
            }

            return Json(pro);
        }

        [HttpPost]
        //public ActionResult UpdateProduct(ImgViewModel model)
        public int UpdateProduct(ImgViewModel model)
        {
            int ImgsId = 0;
            var files = model.ImgFile;
            byte[] imageByte = null;
            int ProId = Int32.Parse(model.ProductId);
            int ImId = Int32.Parse(model.ImageId);

            using (MyContext db = new MyContext())
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        file.SaveAs(Server.MapPath("/Images/" + file.FileName));
                        BinaryReader reader = new BinaryReader(file.InputStream);
                        imageByte = reader.ReadBytes(file.ContentLength);

                        Images newImg = new Images();

                        newImg.ImgTitle = file.FileName;
                        newImg.ImgByte = imageByte;
                        newImg.ImgPath = "/Images/" + file.FileName;
                        db.Images.Add(newImg);
                        db.SaveChanges();

                        ImgsId = newImg.Id;

                        Product updatedProduct = (from p in db.Products
                                                  where p.Id == ProId
                                                  select p).FirstOrDefault();
                        updatedProduct.Name = model.ProductName;
                        updatedProduct.Unit = model.ProductUnit;
                        updatedProduct.Price = model.ProductPrice;
                        updatedProduct.ImgsId = ImgsId;

                        Images oldImg = (from i in db.Images where i.Id == ImId select i).FirstOrDefault();
                        db.Images.Remove(oldImg);

                        db.SaveChanges();

                    }
                }

                else
                {
                    Product updatedProduct = (from p in db.Products
                                              where p.Id == ProId
                                              select p).FirstOrDefault();
                    updatedProduct.Name = model.ProductName;
                    updatedProduct.Unit = model.ProductUnit;
                    updatedProduct.Price = model.ProductPrice;

                    db.SaveChanges();
                    db.SaveChanges();

                    Images oldImg = (from i in db.Images where i.Id == ImId select i).FirstOrDefault();
                    ImgsId = oldImg.Id;

                }

            }

            //return new EmptyResult();
            return ImgsId;
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            using (MyContext db = new MyContext())
            {
                Product product = (from p in db.Products
                                   where p.Id == productId
                                   select p).FirstOrDefault();

                Images img = (from i in db.Images where i.Id == product.ImgsId select i).FirstOrDefault();


                db.Products.Remove(product);
                db.Images.Remove(img);

                string fullPath = Request.MapPath("/Images/" + img.ImgTitle);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                db.SaveChanges();
            }
            return new EmptyResult();
        }


        [HttpPost]
        public ActionResult DeleteProducts(List<int> arr)
        {

            using (MyContext db = new MyContext())
            {
                if (arr != null)
                    foreach (var a in arr)
                    {
                        Product product = (from p in db.Products
                                           where p.Id == a
                                           select p).FirstOrDefault();

                        Images img = (from i in db.Images where i.Id == product.ImgsId select i).FirstOrDefault();


                        db.Products.Remove(product);
                        db.Images.Remove(img);

                        db.SaveChanges();
                    }
            }
            return new EmptyResult();
        }


        public ActionResult DisplayingImage(int id)
        {
            Images img = null;
            if (id != null)
            {
                MyContext db = new MyContext();

                img = db.Images.SingleOrDefault(x => x.Id == id);
                return File(img.ImgByte, "image/jpg");
            }

            else
            {
                return new EmptyResult();
            }


        }

        public JsonResult Export()
        {
            MyContext db = new MyContext();


            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            //var workSheet = workbook.Worksheets.Add("Sheet1");

            var export = db.Products.ToList();

            worksheet.Cells[1, 1] = "Id";
            worksheet.Cells[1, 2] = "Name";
            worksheet.Cells[1, 3] = "Unit";
            worksheet.Cells[1, 4] = "Price";

            var row = 2;
            foreach (var e in export)
            {
                worksheet.Cells[row, 1] = e.Id;
                worksheet.Cells[row, 2] = e.Name;
                worksheet.Cells[row, 3] = e.Unit;
                worksheet.Cells[row, 4] = e.Price;

                row++;
            }

            string fileName = "Order_" + DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss") + ".xlsx";
            //var path = Server.MapPath("/Content/Files/"+fileName);
            string fullPath = Path.Combine(Server.MapPath("/Content/Files"), fileName);

            workbook.SaveAs(fullPath);
            //workbook.SaveAs(Server.MapPath("/Content/Files/"+name+".xlsx"));

            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            application.Quit();
            Marshal.FinalReleaseComObject(application);

            byte[] filebite = System.IO.File.ReadAllBytes(fullPath);
            var error = "Something went wrong";
            var tempFile = new { fName = fileName, fPath = fullPath, errorMessage = error, fB = filebite };
            return Json(tempFile, JsonRequestBehavior.AllowGet);


        }

        public FileResult Download(string filePath, string fileName)
        {

            //https://stackoverflow.com/questions/30704078/how-to-download-a-file-through-ajax-request-in-asp-net-mvc-4

            string contentType = string.Empty;
            var sDocument = filePath;


            //if (sDocument == null)
            //{
            //    return HttpNotFound();
            //}

            //if (mode == "action")
            //    return Json(new { fileName = filePath }, JsonRequestBehavior.AllowGet);

            if (sDocument.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }
            else if (sDocument.Contains(".docx"))
            {
                contentType = "application/docx";
            }
            else if (sDocument.Contains(".xls"))
            {
                contentType = "application/xlsx";
            }

            return File(filePath, contentType, fileName);
            //return File(filePath, "application/xlsx", fileName);

        }

        public JsonResult ExportByte()
        {
            MyContext db = new MyContext();


            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            //var workSheet = workbook.Worksheets.Add("Sheet1");

            var export = db.Products.ToList();

            worksheet.Cells[1, 1] = "Id";
            worksheet.Cells[1, 2] = "Name";
            worksheet.Cells[1, 3] = "Unit";
            worksheet.Cells[1, 4] = "Price";

            var row = 2;
            foreach (var e in export)
            {
                worksheet.Cells[row, 1] = e.Id;
                worksheet.Cells[row, 2] = e.Name;
                worksheet.Cells[row, 3] = e.Unit;
                worksheet.Cells[row, 4] = e.Price;

                row++;
            }

            string fileName = "Order_" + DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss") + ".xlsx";
            //var path = Server.MapPath("/Content/Files/"+fileName);
            string fullPath = Path.Combine(Server.MapPath("/Content/Files"), fileName);

            workbook.SaveAs(fullPath);
            //workbook.SaveAs(Server.MapPath("/Content/Files/"+name+".xlsx"));

            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            application.Quit();
            Marshal.FinalReleaseComObject(application);

            byte[] filebite = System.IO.File.ReadAllBytes(fullPath);

            CreatedFiles file = new CreatedFiles();

            file.FileTitle = fileName;
            file.FilePath = fullPath;
            file.FileByte = filebite;
            db.Files.Add(file);
            db.SaveChanges();

            System.IO.File.Delete(fullPath);

            var error = "Something went wrong";
            //var tempFile = new { fName = fileName, fPath = fullPath, errorMessage = error, fB = filebite };
            return Json(file.Id, JsonRequestBehavior.AllowGet);


        }

        public FileResult DownloadFileBite(int id)
        {

            CreatedFiles file = null;

            MyContext db = new MyContext();

            file = db.Files.SingleOrDefault(x => x.Id == id);
            return File(file.FileByte, "applcation/xlsx", file.FileTitle);

            //return File(filePath, "application/xlsx", fileName);

        }


        [HttpPost]
        public JsonResult UploadFile(FileViewModel model)
        {

            List<int> imagesId = new List<int>();

            //var a = model.FileName;
            //var b = model.FileSize;
            //var c = model.FileType;
            //var d = model.UploadFile;



            var files = model.UploadFile;
            //  byte[] fileByte = null;
            Product pro = null;

            using (MyContext db = new MyContext())
            {
                if (model != null)
                {
                    foreach (var file in files)
                    {


                        file.SaveAs(Server.MapPath("/Content/FilesUploaded/" + file.FileName));
                        BinaryReader reader = new BinaryReader(file.InputStream);
                        byte[] fileByte = reader.ReadBytes(file.ContentLength);

                        UploadedFile uFile = new UploadedFile();

                        uFile.FileName = file.FileName;
                        uFile.FileSize = file.ContentLength;
                        uFile.FileType = file.ContentType;
                        uFile.FileByte = fileByte;
                        uFile.FilePath = "Content/FilesUploaded/" + file.FileName;


                        db.uFiles.Add(uFile);
                        db.SaveChanges();

                        imagesId.Add(uFile.Id);

                    }

                    db.SaveChanges();

                }
            }


            return Json(imagesId);
        }

        public FileResult DownloadUploadedFileBite(int id)
        {





            UploadedFile file = null;

            MyContext db = new MyContext();



            file = db.uFiles.SingleOrDefault(x => x.Id == id);

            string contentType = string.Empty;
            var sDocument = file.FilePath;


            //if (sDocument == null)
            //{
            //    return HttpNotFound();
            //}

            //if (mode == "action")
            //    return Json(new { fileName = filePath }, JsonRequestBehavior.AllowGet);

            if (sDocument.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }
            else if (sDocument.Contains(".docx"))
            {
                contentType = "application/docx";
            }
            else if (sDocument.Contains(".xls"))
            {
                contentType = "application/xlsx";
            }

            else if (sDocument.Contains(".jpg"))
            {
                contentType = "image/jpg";
            }


            return File(file.FileByte, contentType, file.FileName);


            //return File(filePath, "application/xlsx", fileName);

        }



        [HttpPost]
        public JsonResult AddImgToExcelWorking(FileViewModel model)
        {

            int excelId = 0;
            string imgName = "";
            var CountedUsedRange = "";
            var CountedFilledRange = "";
            var AffectedRows = 0;
            //var response = "Range: Filled:ImagesInserted +






            var files = model.UploadFile;
            //  byte[] fileByte = null;


            using (MyContext db = new MyContext())
            {
                if (model != null)
                {
                    string exelFile = Server.MapPath("/Content/FilesUploaded/Test7.xlsx");
                    //Server.MapPath("/Content/FilesUploaded/"
                    Excel.Application excel = new Excel.Application();
                    //Excel.Workbook workbook = excel.Workbooks.Open(@"C:\Users\Martijn\Documents\Test.xlsx", ReadOnly: false, Editable: true);
                    Excel.Workbook workbook = excel.Workbooks.Open(exelFile, ReadOnly: false, Editable: true);
                    Excel.Worksheet worksheet = workbook.Worksheets.Item[1] as Excel.Worksheet;
                    if (exelFile == null)
                        return Json("Error");

                    //Range row1 = worksheet.Rows.Cells[1, 1];
                    //Range row2 = worksheet.Rows.Cells[2, 1];
                    //row1.Value = "Test100";
                    //row2.Value = "Test200";

                    //worksheet.Cells[1, 1] = "IMG"; // Works
                    //worksheet.Cells[1, 1].Style.Font.Bold = true; //Does not work!


                    //excel.Application.ActiveWorkbook.Save();
                    //excel.Application.Quit();
                    //excel.Quit();


                    int row = 3;

                    //Excel.Range UsedRange = worksheet.UsedRange;
                    //object[] UsedRange = worksheet.Range["A"].Value2;
                    //UsedRange.Select(a => a == null ? null : a.ToString()).ToList();
                    //int lastUsedRow = UsedRange.Row + UsedRange.Rows.Count - 1;

                    //// Returns array of strings of used range --- Start // Works
                    Excel.Range firstColumn = worksheet.UsedRange.Columns[1];
                    System.Array myvalues = (System.Array)firstColumn.Cells.Value;
                    string[] usedRange = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();
                    CountedFilledRange = usedRange.Length.ToString();
                    //// Returns array of strings of used range --- End //


                    foreach (var file in files)
                    {

                        ////Read and Save File -- Start
                        file.SaveAs(Server.MapPath("/Content/FilesUploaded/" + file.FileName));
                        //BinaryReader reader = new BinaryReader(file.InputStream);
                        //byte[] fileByte = reader.ReadBytes(file.ContentLength);

                        //UploadedFile uFile = new UploadedFile();

                        //uFile.FileName = file.FileName;
                        //uFile.FileSize = file.ContentLength;
                        //uFile.FileType = file.ContentType;
                        //uFile.FileByte = fileByte;
                        //uFile.FilePath = "Content/FilesUploaded/" + file.FileName;

                        //db.uFiles.Add(uFile);
                        //db.SaveChanges();
                        ////Read and Save File --End

                        ////--- Modify Strings --- Start
                        //Remove everything after "."
                        var imgNameTemp = file.FileName.Remove(file.FileName.IndexOf(".") + 1);
                        ////--- Remove "."
                        imgName = imgNameTemp.Replace(@".", string.Empty).ToUpper();
                        //Modify Strings --- End


                        string fName = Server.MapPath("/Content/FilesUploaded/" + file.FileName);


                        // Works
                        //worksheet.Shapes.AddPicture(fName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45);

                        // worksheet.Pictures.Add(1, 1, );
                        // worksheet.Pictures.Add(1, 1, fName);

                        // works
                        // Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[row, 2];
                        // float Left = (float)((double)oRange.Left);
                        // float Top = (float)((double)oRange.Top);
                        // const float ImageSize = 50;

                        // Excel.Range UsedRange = worksheet.UsedRange;
                        // int lastUsedRow = UsedRange.Row + UsedRange.Rows.Count - 1;

                        Excel.Range UsedRange = worksheet.UsedRange;
                        CountedUsedRange = UsedRange.Rows.Count.ToString();
                        //int lastUsedRow = UsedRange.Row + UsedRange.Rows.Count - 1;

                        for (var i = 1; i < UsedRange.Count; i++)
                        {
                            Microsoft.Office.Interop.Excel.Range oRange =
                                (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i, 2];
                            //var cellValue = (string)oRange.Value2;

                            var cellValue = worksheet.Cells[i, 1].Text.ToString();
                            if (cellValue != null)
                            {
                                if (cellValue.ToLower() == imgName.ToLower())
                                {

                                    //Microsoft.Office.Interop.Excel.Range oRange =
                                    //    (Microsoft.Office.Interop.Excel.Range) worksheet.Cells[i, 2];
                                    float Left = (float)((double)oRange.Left + 2);
                                    float Top = (float)((double)oRange.Top + 2);
                                    //const float ImageSize = 50;


                                    //if (string.Equals(worksheet.Cells[row, 1].Value.ToLower(), imgName.ToLower()))
                                    //if (string.Equals(worksheet.Cells[row, 1].Value.i.ToString().ToLower(), imgName.ToLower()))

                                    //works
                                    //worksheet.Shapes.AddPicture(fName, Microsoft.Office.Core.MsoTriState.msoFalse,
                                    //    Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);

                                    //Works
                                    //oRange.RowHeight = ImageSize;
                                    //oRange.ColumnWidth = ImageSize;

                                    //--- Get and print Origial Image Sizes --Start
                                    System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                                    var originalWidth = img.Width;
                                    var originalHeight = img.Height;

                                    //worksheet.Cells[i, 6] = "Width: " + originalWidth + " px";
                                    //worksheet.Cells[i, 7] = "Height: " + originalHeight + "px";
                                    //--- Get and print Origial Image Sizes --End

                                    //--- Add a picture and set its sizes  // It lowers quality
                                    //var MyPic2 = worksheet.Shapes.AddPicture(fName,
                                    //    Microsoft.Office.Core.MsoTriState.msoFalse,
                                    //    Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, 50, 50);

                                    //--- Add a picture with original sizes
                                    var MyPic = worksheet.Shapes.AddPicture(fName,
                                        Microsoft.Office.Core.MsoTriState.msoFalse,
                                        Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, -1, -1);
                                    // Now set the sizes
                                    var height = 60;
                                    // var width = 100;
                                    MyPic.Height = height; //Points
                                    //MyPic.Width = width;

                                    //Set the row height or column width
                                    oRange.RowHeight = MyPic.Height + 2;
                                    //oRange.ColumnWidth = MyPic.Height;

                                    //Calculate column width for added picture

                                    //// Height
                                    //// 1 point equals: approximately  0.035 cm
                                    //// 1cm = 1point / 0.035cm = 28.57 points

                                    //// Width
                                    //// 1 point equals: approximately  0.187 cm
                                    //// 1 cm equals: 1point / 0.187cm = 5.35 points

                                    double ratio = (double)originalWidth / (double)originalHeight;
                                    var myPicHeightInCM = height * 0.035;
                                    var myPicWidthInCM = myPicHeightInCM * ratio;
                                    var myPicWidthInPoints = myPicWidthInCM * 5.35;

                                    oRange.ColumnWidth = myPicWidthInPoints + 0.30;

                                    //worksheet.Cells[row, 3] = imgName.ToUpper();
                                    //row++;

                                    AffectedRows++;
                                    //excelId = uFile.Id;
                                }
                            }
                        }
                    }

                    ////--- Set Range
                    //Excel.Range newRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[10, 10]];

                    ////-- set named Style
                    //Excel.Style style = workbook.Styles.Add("NewStyle");

                    //style.Font.Name = "Verdana";
                    //style.Font.Size = 20;
                    //style.Font.Bold = true;
                    //style.Font.Italic = true;
                    //style.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    //style.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                    //style.Interior.Pattern = Excel.XlPattern.xlPatternSolid;
                    //style.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    //style.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    ////--- Apply named Style to Range
                    //newRange.Style = "NewStyle";

                    ////--- Autofit Column
                    //newRange.Columns.AutoFit();


                    excel.Application.ActiveWorkbook.Save();
                    excel.Application.Quit();
                    excel.Quit();

                    db.SaveChanges();

                }
            }

            return Json(new
            {
                Range = CountedUsedRange,
                Filled = CountedFilledRange,
                ImagesInserted = AffectedRows
            }, JsonRequestBehavior.AllowGet);
        }


        //

        [HttpPost]
        public JsonResult AddImgToExcel(FileViewModel model)
        {

            int excelId = 0;
            string imgName = "";
            var CountedUsedRange = "";
            var CountedFilledRange = "";
            var AffectedRows = 0;


            var files = model.UploadFile;

            if (model != null)
            {
                string exelFile = Server.MapPath("/Content/FilesUploaded/Test7.xlsx");

                Excel.Application excel = new Excel.Application();
                excel.DisplayAlerts = false;
                excel.Visible = false;
                excel.ScreenUpdating = false;
                Excel.Workbook workbook = excel.Workbooks.Open(exelFile, ReadOnly: false, Editable: true);
                Excel.Worksheet worksheet = workbook.Worksheets.Item[1] as Excel.Worksheet;
                if (exelFile == null)
                    return Json("Error");

                int row = 2;


                Excel.Range firstColumn = worksheet.UsedRange.Columns[1];
                System.Array myvalues = (System.Array)firstColumn.Cells.Value;
                string[] usedRange = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();
                CountedFilledRange = usedRange.Length.ToString();



                foreach (var file in files)
                {


                    file.SaveAs(Server.MapPath("/Content/FilesUploaded/" + file.FileName));

                    var imgNameTemp = file.FileName.Remove(file.FileName.IndexOf(".") + 1);

                    imgName = imgNameTemp.Replace(@".", string.Empty).ToUpper();

                    string fName = Server.MapPath("/Content/FilesUploaded/" + file.FileName);



                    Excel.Range UsedRange = worksheet.UsedRange;
                    CountedUsedRange = UsedRange.Rows.Count.ToString();

                    Microsoft.Office.Interop.Excel.Range oRange =
                        (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[row, 7];




                    float Left = (float)((double)oRange.Left + 2);
                    float Top = (float)((double)oRange.Top + 2);

                    System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                    var originalWidth = img.Width;
                    var originalHeight = img.Height;


                    var MyPic = worksheet.Shapes.AddPicture(fName,
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, -1, -1);

                    var height = 60;

                    MyPic.Height = height;

                    oRange.RowHeight = MyPic.Height + 2;

                    double ratio = (double)originalWidth / (double)originalHeight;
                    var myPicHeightInCM = height * 0.035;
                    var myPicWidthInCM = myPicHeightInCM * ratio;
                    var myPicWidthInPoints = myPicWidthInCM * 5.35;

                    oRange.ColumnWidth = myPicWidthInPoints + 0.30;

                    worksheet.Cells[row, 6] = imgName.ToUpper();

                    row++;

                }




                excel.Application.ActiveWorkbook.Save();
                excel.Application.Quit();
                excel.Quit();




            }

            return Json(new
            {
                Range = CountedUsedRange,
                Filled = CountedFilledRange,
                ImagesInserted = AffectedRows
            }, JsonRequestBehavior.AllowGet);
        }






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}